namespace TestFileGenerator.Core
{
    public interface ILargeFileGenerator
    {
        Task GenerateLargeFileAsync(string folderPath,
            long fileSizeInMegabytes,
            Action<bool> onFileCreated,
            Action<long> onBytesWritten);
    }
}
