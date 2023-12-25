using BookingService.Data.Dto;
using BookingService.Models.Dto;
using BookingService.Services.Iservice;
using Newtonsoft.Json;

namespace BookingService.Services
{
    public class CouponsService : ICoupon
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CouponsService(IHttpClientFactory httpClientFactory)
        {
            
            _httpClientFactory = httpClientFactory;
        }
        public async Task<CouponDTO> GetCouponByCode(string code)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Coupons");
                var response = await client.GetAsync(code);
                var content = await response.Content.ReadAsStringAsync();
                var responseDto = JsonConvert.DeserializeObject<ResponseDTO>(content);
                if ( response.IsSuccessStatusCode)
                {

                    return JsonConvert.DeserializeObject<CouponDTO>(responseDto.Result.ToString());
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
