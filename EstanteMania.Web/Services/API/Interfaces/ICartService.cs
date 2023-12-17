using EstanteMania.API.DTO_s;
using EstanteMania.Web.Models.DTO_s;
using EstanteManiaWebAssembly.Models.DTO_s;

namespace EstanteMania.Web.Services.API.Interfaces
{
    public interface ICartService
    {
        Task<List<CarrinhoItemDTO>> GetItens(string userId);
        Task<CarrinhoItemDTO> AddItem(AddBookToCartDTO addBookToCartDTO, string userId);
        Task<CarrinhoItemDTO> Delete(int id);
        Task<CarrinhoItemDTO> UpdateQuantity(UpdateBookQuantityOnCartDTO updateBookQuantityOnCartDTO);
        Task<int> GetCartId(string userId);
        Task<int> CreateCartForUser(string userId);
        Task<bool> ApplyCouponAsync(UserCoupon userCoupon);
        Task<bool> DeleteCouponAsync(string userId);
        Task<string?> GetCouponFromUserAsync(string userId);
        
        event Action<int> OnShopCartChanged;
        void RaiseEventOnShopCartChanged(int totalQuantity);

        Task<CartHeaderDTO> Checkout(CartHeaderDTO cartHeader);
    }
}