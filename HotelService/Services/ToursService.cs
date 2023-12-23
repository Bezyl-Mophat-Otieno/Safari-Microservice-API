using HotelService.Models.Dto;
using HotelService.Services.Iservice;
using Newtonsoft.Json;

namespace HotelService.Services
{
    public class ToursService : ITour
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ToursService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            
        }
        public async Task<TourDTO> GetTourById(Guid Id)
        {
            try {
                var client = _httpClientFactory.CreateClient("Tours");
                var response = await client.GetAsync($"{Id}");
                var content = await response.Content.ReadAsStringAsync();

                var Tour = JsonConvert.DeserializeObject<TourDTO>(content);

                if (response.IsSuccessStatusCode)
                {
                    return Tour;

                }
                 return null;

            }catch (Exception) {

                return null;
            }
        }
    }
}
