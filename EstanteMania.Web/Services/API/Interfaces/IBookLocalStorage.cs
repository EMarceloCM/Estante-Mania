using EstanteManiaWebAssembly.Models.DTO_s;

namespace EstanteMania.Web.Services.API.Interfaces
{
    public interface IBookLocalStorage
    {
        Task<IEnumerable<BookDTO>> GetCollection();
        Task RemoveCollection();
    }
}
