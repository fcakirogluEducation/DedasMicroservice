using Refit;

namespace Microservice1.Services
{
    public interface IMicroService2Refit
    {
        [Get("/api/Values")]
        Task<List<string>> GetCityList();


        [Get("/api/Values")]
        Task<ApiResponse<List<string>>> GetCityList2();
    }
}