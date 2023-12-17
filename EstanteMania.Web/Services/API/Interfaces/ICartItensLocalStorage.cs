using EstanteMania.API.DTO_s;

namespace EstanteMania.Web.Services.API.Interfaces
{
    public interface ICartItensLocalStorage
    {
        Task<List<CarrinhoItemDTO>> GetCollection();
        Task SaveCollection(List<CarrinhoItemDTO> cartItensDTO);
        Task RemoveCollection();
    }
}
