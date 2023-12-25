using BookingService.Data.Dto;
using BookingService.Models.Dto;
using BookingService.Services.Iservice;
using Newtonsoft.Json;
using System.Net.Http;

namespace BookingService.Services
{
    public class HotelsService : IHotel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HotelsService(IHttpClientFactory httpClientFactory)
        {

            _httpClientFactory = httpClientFactory;
        }
        public async Task<HotelDTO> GetHotelById(Guid id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Hotels");
                var response = await client.GetAsync(id.ToString());
                var content = await response.Content.ReadAsStringAsync();
                var responseDto = JsonConvert.DeserializeObject<ResponseDTO>(content);
                if (responseDto.Result != null && response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<HotelDTO>(responseDto.Result.ToString());
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
