
namespace TestFileSorter.Core
{
    public interface ILargeFileSorter
    {
        Task GenerateSortedFileAsync(string filePath, 
            Action<string> onComplete, 
            Action<string> onChunkingIsDone);
    }
}