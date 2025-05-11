using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace TestFileGenerator.Core
{
    public class LargeFileGenerator : ILargeFileGenerator
    {
        private readonly List<string> _myPhrases = new List<string>() {
            "Apple", "Banana is yellow", "Cherry is the best", "Dogs love to play", "Elephants are huge", "Frogs jump high",
            "Milk is white", "Rabbits eat carrots", "Water is life", "Zebras have stripes"};


        public async Task GenerateLargeFileAsync(string folderPath,
            long fileSizeInMegabytes,
            Action<bool> onFileCreated,
            Action<long> onBytesWritten)
        {
            var random = new Random();

            long bytesWritten = 0;
            const int maxIndexCount = 100000; //{index}.{somePhrase}
            int phrasesCount = _myPhrases.Count;
            long fileSizeInBytes = fileSizeInMegabytes * 1024 * 1024;
            string fullFilepath = $"{folderPath}\\MySuperFile.txt";
            long lastReportedBytes = 0;
            const int progressBarIndicatorInMb = 20 * 1024 * 1024;  //20 mb

            var sb = new StringBuilder();
            const int batchLineSize = 250000;
            const int fileStreamBufferSize = 50 * 1024 * 1024;

            File.Delete(fullFilepath);

            try
            {
                using (var fs = new FileStream(fullFilepath, FileMode.Create, FileAccess.Write, FileShare.None, fileStreamBufferSize))
                using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                {
                    while (bytesWritten < fileSizeInBytes)
                    {
                        sb.Clear();

                        for (int i = 0; i < batchLineSize && bytesWritten < fileSizeInBytes; i++)
                        {
                            int randomNumberIndex = random.Next(maxIndexCount);
                            int randomStringIndex = random.Next(phrasesCount);

                            string tmpLine = $"{randomNumberIndex}.{_myPhrases[randomStringIndex]}{Environment.NewLine}";
                            sb.Append(tmpLine);

                            bytesWritten += Encoding.UTF8.GetByteCount(tmpLine);
                        }

                        await writer.WriteLineAsync(sb.ToString());

                        if (bytesWritten - lastReportedBytes >= progressBarIndicatorInMb)
                        {
                            onBytesWritten(bytesWritten);
                            lastReportedBytes = bytesWritten;
                        }
                    }
                }

                onFileCreated(true);
            }
            catch (Exception ex) 
            {
                onFileCreated(false);
            }
        }
    }
}
