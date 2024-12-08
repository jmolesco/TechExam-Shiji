using ShijiGroup.Models;

namespace ShijiGroup.Infrastracture.Interface
{
    public interface IWordFinderService
    {
        Task<string> GetMatrices();
        Task<object> FindWords(List<string> inputDictionary);
    }
}
