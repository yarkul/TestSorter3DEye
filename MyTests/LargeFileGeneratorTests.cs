using System;
using System.IO;
using System.Threading.Tasks;
using TestFileGenerator.Core;
using Xunit;

namespace MyTests
{
        public class LargeFileGeneratorTests
        {
            private readonly string _testFolderPath;

            public LargeFileGeneratorTests()
            {
                _testFolderPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

                Directory.CreateDirectory(_testFolderPath);
            }

            [Fact]
            public async Task GenerateLargeFileAsync_CreatesFileWithCorrectSize()
            {
                // Arrange
                var generator = new LargeFileGenerator();
                long fileSizeInMb = 1; // 1 MB
                long expectedSizeInBytes = fileSizeInMb * 1024 * 1024;
                bool fileCreated = false;
                long bytesWrittenReported = 0;

                void OnFileCreated(bool success) => fileCreated = success;
                void OnBytesWritten(long bytes) => bytesWrittenReported = bytes;

                // Act
                await generator.GenerateLargeFileAsync(
                    _testFolderPath,
                    fileSizeInMb,
                    OnFileCreated,
                    OnBytesWritten);

                // Assert
                string filePath = Path.Combine(_testFolderPath, "MySuperFile.txt");
                var fileInfo = new FileInfo(filePath);

                Assert.True(fileCreated, "File creation callback should return true");
                Assert.True(File.Exists(filePath), "File should exist");

                Assert.True(fileInfo.Length >= expectedSizeInBytes * 0.95 && fileInfo.Length <= expectedSizeInBytes * 1.05,
                    $"File size ({fileInfo.Length} bytes) should be close to {expectedSizeInBytes} bytes");

                if (Directory.Exists(_testFolderPath))
                {
                    Directory.Delete(_testFolderPath, true);
                }
        }
        }
}