using EstanteMania.API.DTO_s;
using EstanteMania.Web.Models.DTO_s;
using EstanteMania.Web.Services.API.Interfaces;
using System.Net.Http.Json;

namespace EstanteMania.Web.Services.API
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ILogger<CouponService> _logger;

        private const string apiEndpoint = "/api/coupon/";

        public CouponService(IHttpClientFactory httpClientFactory, ILogger<CouponService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<CouponDTO?> GetCouponByCode(string couponCode)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("EstanteManiaCouponApi");
                var response = await httpClient.GetAsync(apiEndpoint + couponCode);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CouponDTO>();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error while trying to get coupon by code = {couponCode} - {message}");
                    throw new Exception("Status Code : " + response.StatusCode + " - " + message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while trying to get coupon by code = {couponCode} - {ex.Message}");
                throw;
            }
        }
    }
}
