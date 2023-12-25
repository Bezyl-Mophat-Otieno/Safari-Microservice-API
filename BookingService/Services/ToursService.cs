using BookingService.Data.Dto;
using BookingService.Models.Dto;
using BookingService.Services.Iservice;
using Newtonsoft.Json;

namespace BookingService.Services
{
    public class ToursService : ITour
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ToursService(IHttpClientFactory httpClientFactory)
        {

            _httpClientFactory = httpClientFactory;
        }
        public async Task<TourDTO> GetTourByID(Guid id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Tours");
                var response = await client.GetAsync(id.ToString());
                var content = await response.Content.ReadAsStringAsync();
                var responseDto = JsonConvert.DeserializeObject<ResponseDTO>(content);
                if (responseDto.Result != null && response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<TourDTO>(responseDto.Result.ToString());
                }
                return null;

            }
            catch (Exception ex)
            {

                return null;

            }
        }
    }
}
