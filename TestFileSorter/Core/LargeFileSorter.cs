using System.Linq;
using System.Text;

namespace TestFileSorter.Core
{
    public struct MyLine : IComparable<MyLine>
    {
        public MyLine(int lineIndex, string line)
        {
            LineIndex = lineIndex;
            Line = line;
        }

        public MyLine(string line)
        {
            ReadOnlySpan<char> lineSpan = line.AsSpan();

            int dotIndex = lineSpan.IndexOf('.');

            if (dotIndex != -1) 
            {
                LineIndex = int.Parse(lineSpan[..dotIndex]);
                Line = lineSpan[(dotIndex + 1)..].ToString();
            }
        }

        public int LineIndex { get; set; }
        public string Line { get; set; }

        public int CompareTo(MyLine other)
        {
            int cmp = string.Compare(Line, other.Line, StringComparison.Ordinal);

            return cmp != 0 ? cmp : LineIndex.CompareTo(other.LineIndex);
        }

        public override string ToString() => $"{LineIndex}.{Line}";
    }

    public class LargeFileSorter : ILargeFileSorter
    {
        const int StreamBufferSize = 8 * 1024 * 1024; //mb

        const int StringBuilderFlushSize = 50000;

        public async Task GenerateSortedFileAsync(string filePath, Action<string> onComplete, Action<string> notifyCurrentStatus)
        {

            if (!File.Exists(filePath))
            {
                onComplete("File does not exists");
                return;
            }

            const int chunkSizeMb = 300 * 1024 * 1024; // 300mb

            List<string> sortedFiles = await GetSortedFileChunksAsync(filePath, chunkSizeMb, onComplete, notifyCurrentStatus);

            notifyCurrentStatus("Chunking is completed. Starting Merging...");

            await MergeChunksAsync(sortedFiles, filePath, onComplete, notifyCurrentStatus);
        }

        private async Task MergeChunksAsync(List<string> sortedFilePaths,
                                        string inputFilePath,
                                        Action<string> onComplete,
                                        Action<string> notifyCurrentStatus)
        {
            List<StreamReader> readers = sortedFilePaths
                .Select(path => new StreamReader(path, Encoding.UTF8, false, StreamBufferSize))
                .ToList();

            var pq = new PriorityQueue<(MyLine line, int fileIndex), MyLine>();

            for (int i = 0; i < readers.Count; i++)
            {
                if (!readers[i].EndOfStream)
                {
                    string tmpLine = await readers[i].ReadLineAsync();

                    if (!string.IsNullOrWhiteSpace(tmpLine))
                    {
                        var l = new MyLine(tmpLine);
                        pq.Enqueue((l, i), l);
                    }
                }
            }

            //var parallelOptions = new ParallelOptions
            //{
            //    MaxDegreeOfParallelism = Environment.ProcessorCount
            //};

            //await Parallel.ForEachAsync(readers.Select((reader, index)
            //    => (reader, index)), parallelOptions, async (item, ct) =>
            //{
            //    var (reader, fileIndex) = item;
            //    if (!reader.EndOfStream)
            //    {
            //        string tmpLine = await reader.ReadLineAsync();

            //        if (!string.IsNullOrWhiteSpace(tmpLine))
            //        {
            //            var l = new MyLine(tmpLine);

            //            pq.Enqueue((l, fileIndex), l);
            //        }
            //    }
            //});

            string folderPath = Path.GetDirectoryName(inputFilePath);
            string outputFile = Path.Combine(folderPath, $"FinalResult_SortedFile.txt");

            using (var fs = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.None, StreamBufferSize))
            using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8, StreamBufferSize))
            {
                var sb = new StringBuilder();

                int lineWritten = 0;

                while (pq.Count > 0)
                {
                    var (myLine, fileIndex) = pq.Dequeue();

                    string tmpLine = myLine.ToString();

                    if (!string.IsNullOrWhiteSpace(tmpLine))
                    {
                        sb.AppendLine(tmpLine);
                    }

                    lineWritten++;

                    if (lineWritten >= StringBuilderFlushSize)
                    {
                        await writer.WriteAsync(sb.ToString());

                        sb.Clear();

                        lineWritten = 0;
                    }

                    if (!readers[fileIndex].EndOfStream)
                    {
                        var nextLine = await readers[fileIndex].ReadLineAsync();
                        var newEntry = new MyLine(nextLine);
                        pq.Enqueue((newEntry, fileIndex), newEntry);
                    }
                }

                if (lineWritten > 0)
                {
                    await writer.WriteAsync(sb.ToString());
                }

            }

            foreach (var reader in readers)
            {
                reader.Dispose();
            }

            onComplete("Merge is done. Check FinalResult_SortedFile.txt");
        }

        public async Task<List<string>> GetSortedFileChunksAsync
            (string inputFilePath, 
            int chunkSizeInBytes, 
            Action<string> onComplete, 
            Action<string> notifyCurrentStatus)
        {
            var tempFiles = new List<string>();

            string folderPath = Path.GetDirectoryName(inputFilePath);

            int chunkIndex = 0;

            var processingTasks = new List<Task>();

            using (var reader = new StreamReader(inputFilePath, 
                Encoding.UTF8,
                true, StreamBufferSize)) 
            {
                notifyCurrentStatus($"Chunking is started. Please wait ...");

                var chunk = new List<MyLine>();

                long bytesRead = 0;

                while (!reader.EndOfStream)
                {
                    string? line = await reader.ReadLineAsync();

                    if (!string.IsNullOrWhiteSpace(line)) 
                    {
                        bytesRead += Encoding.UTF8.GetByteCount(line) + Environment.NewLine.Length;

                        var myLine = new MyLine(line);

                        if (!string.IsNullOrWhiteSpace(myLine.Line))
                        {
                            chunk.Add(myLine);
                        }
                    };

                    if (bytesRead >= chunkSizeInBytes || reader.EndOfStream)
                    {
                        chunkIndex++;

                        string tempFile = Path.Combine(folderPath, $"chunk_{chunkIndex}.txt");

                        var copyChunk = chunk.ToList();

                        processingTasks.Add(SortAndSaveChunkAsync(copyChunk, tempFile));

                        tempFiles.Add(tempFile);

                        chunk.Clear();

                        bytesRead = 0;
                    }
                }
            }

            await Task.WhenAll(processingTasks);

            return tempFiles;
        }

        private async Task SortAndSaveChunkAsync(List<MyLine> chunk, string filePath)
        {
            int lineWritten = 0;

            chunk = chunk.AsParallel().OrderBy(x => x, Comparer<MyLine>.Default).ToList();
            //chunk.Sort();

            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, StreamBufferSize))
            using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8, StreamBufferSize))
            {
                var sb = new StringBuilder();

                foreach (MyLine line in chunk)
                {
                    string tmpLine = line.ToString();

                    lineWritten ++;

                    sb.AppendLine(tmpLine);

                    if (lineWritten >= StringBuilderFlushSize)
                    {
                        await writer.WriteLineAsync(sb.ToString());

                        sb.Clear();

                        lineWritten = 0;
                    }
                }

                if (lineWritten > 0)
                {
                    await writer.WriteLineAsync(sb.ToString());
                }
            }
        }
    }
}
