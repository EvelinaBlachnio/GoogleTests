using System.Threading.Tasks;

namespace Project.Core.RestApiServices
{
    public interface IRestApiService
    {
        Task<T> GetResponse<T>(string endpoint) where T : class;
    }
}
