using Blazored.LocalStorage;
using EstanteMania.API.DTO_s;
using EstanteMania.Web.Services.API.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace EstanteMania.Web.Services.API
{
    public class CartItensLocalStorage : ICartItensLocalStorage 
    {
        private const string key = "#CartItensCollection#";
        private readonly ILocalStorageService localStorageService;
        private readonly ICartService cartService;
        private readonly AuthenticationStateProvider AuthenticationStateProvider;

        public CartItensLocalStorage(ILocalStorageService localStorageService, ICartService cartService, AuthenticationStateProvider authenticationStateProvider)
        {
            this.localStorageService = localStorageService;
            this.cartService = cartService;
            AuthenticationStateProvider = authenticationStateProvider;
        }

        public async Task<List<CarrinhoItemDTO>> GetCollection()
        {
            return await this.localStorageService.GetItemAsync<List<CarrinhoItemDTO>>(key) ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await this.localStorageService.RemoveItemAsync(key);
        }

        public async Task SaveCollection(List<CarrinhoItemDTO> cartItensDTO)
        {
            await this.localStorageService.SetItemAsync(key, cartItensDTO);
        }

        private async Task<List<CarrinhoItemDTO>> AddCollection()
        {
            string? userId;

            var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authenticationState.User;

            if (user.Identity!.IsAuthenticated)
            {
                userId = user.FindFirst("unique_name")?.Value;
                var shopCartCollection = await this.cartService.GetItens(userId);

                if (shopCartCollection != null)
                {
                    await this.localStorageService.SetItemAsync(key, shopCartCollection);
                }

                return shopCartCollection;
            }

            return null;
        }
    }
}
