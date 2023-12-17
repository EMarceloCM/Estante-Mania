using EstanteMania.API.DTO_s;
using EstanteMania.Web.Models.DTO_s;
using EstanteMania.Web.Services.API.Interfaces;
using EstanteManiaWebAssembly.Models.DTO_s;
using System.Net.Http.Json;
using System.Text.Json;

namespace EstanteMania.Web.Services.API
{
    public class CartService : ICartService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ILogger<CartService> _logger;

        private const string apiEndpoint = "/api/cart/";

        public event Action<int> OnShopCartChanged;

        public CartService(IHttpClientFactory httpClientFactory, ILogger<CartService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<List<CarrinhoItemDTO>> GetItens(string userId)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("EstanteManiaApi");
                var response = await httpClient.GetAsync(apiEndpoint + userId + "/GetItens");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<CarrinhoItemDTO>().ToList();
                    }
                    return await response.Content.ReadFromJsonAsync<List<CarrinhoItemDTO>>();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return Enumerable.Empty<CarrinhoItemDTO>().ToList();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error while trying to get cart by userId = {userId} - {message}");
                    throw new Exception("Status Code : " + response.StatusCode + " - " + message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while trying to get cart by userId = {userId} - {ex.Message}");
                throw;
            }
        }

        public async Task<CarrinhoItemDTO> AddItem(AddBookToCartDTO addBookToCartDTO, string userId)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("EstanteManiaApi");
                var response = await httpClient.PostAsJsonAsync(apiEndpoint + userId, addBookToCartDTO);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default;
                    }
                    return await response.Content.ReadFromJsonAsync<CarrinhoItemDTO>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error while trying to add cart - {message}");
                    throw new Exception("Status Code : " + response.StatusCode + " - " + message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while trying to add cart - {ex.StackTrace}");
                throw;
            }
        }

        public async Task<CarrinhoItemDTO> Delete(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("EstanteManiaApi");

                using (var response = await httpClient.DeleteAsync(apiEndpoint + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadFromJsonAsync<CarrinhoItemDTO>();
                    }
                    return default;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while trying to delete item - {ex.StackTrace}");
                throw;
            }
        }

        public async Task<CarrinhoItemDTO> UpdateQuantity(UpdateBookQuantityOnCartDTO updateBookQuantityOnCartDTO)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("EstanteManiaApi");
                var jsonRequest = JsonSerializer.Serialize(updateBookQuantityOnCartDTO);
                var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json-patch+json");

                using (var response = await httpClient.PatchAsync(apiEndpoint + updateBookQuantityOnCartDTO.CarrinhoItemId, content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadFromJsonAsync<CarrinhoItemDTO>();
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while trying to update quantity - {ex.Message}");
                throw;
            }
        }

        public void RaiseEventOnShopCartChanged(int totalQuantity)
        {
            if (OnShopCartChanged != null)
            {
                OnShopCartChanged.Invoke(totalQuantity);
            }
        }

        public async Task<int> GetCartId(string userId)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("EstanteManiaApi");
                var response = await httpClient.GetAsync($"{apiEndpoint}get-cart-from-user/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<int>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error while trying to get cart by userId = {userId} - {message}");
                    throw new Exception("Status Code : " + response.StatusCode + " - " + message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while trying to get cart by userId = {userId} - {ex.Message}");
                throw;
            }
        }

        public async Task<int> CreateCartForUser(string userId)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("EstanteManiaApi");
                var response = await httpClient.GetAsync($"{apiEndpoint}create-cart/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<int>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error while trying to get cart by userId = {userId} - {message}");
                    throw new Exception("Status Code : " + response.StatusCode + " - " + message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while trying to get cart by userId = {userId} - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> ApplyCouponAsync(UserCoupon userCoupon)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("EstanteManiaApi");
                var response = await httpClient.PostAsJsonAsync(apiEndpoint + "applycoupon", userCoupon);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return false;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error while trying to apply coupon - {message}");
                    throw new Exception("Status Code : " + response.StatusCode + " - " + message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while trying to apply coupon - {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> DeleteCouponAsync(string userId)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("EstanteManiaApi");

                using (var response = await httpClient.DeleteAsync(apiEndpoint + "deletecoupon/" + userId))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadFromJsonAsync<bool>();
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while trying to delete coupon - {ex.StackTrace}");
                throw;
            }   
        }

        public async Task<string?> GetCouponFromUserAsync(string userId)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("EstanteManiaApi");
                var response = await httpClient.GetAsync($"{apiEndpoint}get-coupon-from-user/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<string>();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return string.Empty;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error while trying to get coupon by userId = {userId} - {message}");
                    throw new Exception("Status Code : " + response.StatusCode + " - " + message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while trying to get coupon by userId = {userId} - {ex.Message}");
                throw;
            }
        }

        public async Task<CartHeaderDTO> Checkout(CartHeaderDTO cartHeader)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("EstanteManiaApi");
                var response = await httpClient.PostAsJsonAsync(apiEndpoint + "checkout", cartHeader);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CartHeaderDTO>();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error while trying to checkout - {message}");
                    throw new Exception("Status Code : " + response.StatusCode + " - " + message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while trying to checkout - {ex.StackTrace}");
                throw;
            }
        }
    }
}
