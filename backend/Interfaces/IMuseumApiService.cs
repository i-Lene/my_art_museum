

namespace backend.Interfaces
{
    public interface IMuseumApiService
    {
        Task<string> GetDataAsync(string endpoint);
    }

}

