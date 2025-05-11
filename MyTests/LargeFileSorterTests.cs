using System.Text;
using TestFileSorter.Core;

namespace MyTests
{
    public class LargeFileSorterTests
    {
        private readonly string _testFolderPath;

        public LargeFileSorterTests()
        {
            _testFolderPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_testFolderPath);
        }

        [Fact]
        public async Task GenerateSortedFileAsync_SortsFileCorrectly()
        {
            // Arrange
            var sorter = new LargeFileSorter();
            string inputFilePath = Path.Combine(_testFolderPath, "input.txt");
            string outputFilePath = Path.Combine(_testFolderPath, "FinalResult_SortedFile.txt");

            var inputLines = new[]
            {
                "2.Banana is yellow",
                "1.Apple",
                "3.Cherry is the best",
            };

            await File.WriteAllLinesAsync(inputFilePath, inputLines, Encoding.UTF8);

            string completionMessage = null;
            string statusMessage = null;

            void OnComplete(string message) => completionMessage = message;
            void NotifyCurrentStatus(string message) => statusMessage = message;

            // Act
            await sorter.GenerateSortedFileAsync(inputFilePath, OnComplete, NotifyCurrentStatus);

            // Assert
            Assert.Equal("Merge is done. Check FinalResult_SortedFile.txt", completionMessage);
            Assert.Contains("Chunking is completed. Starting Merging...", statusMessage);

            Assert.True(File.Exists(outputFilePath), "Sorted output file should exist");

            var outputLines = await File.ReadAllLinesAsync(outputFilePath, Encoding.UTF8);
            var expectedLines = new[]
            {
                "1.Apple",
                "2.Banana is yellow",
                "3.Cherry is the best",
            };

            Assert.Equal(expectedLines, outputLines);

            if (Directory.Exists(_testFolderPath))
            {
                Directory.Delete(_testFolderPath, true);
            }
        }        
    }
}
