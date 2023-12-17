using Blazored.LocalStorage;
using EstanteMania.Web.Services.API.Interfaces;
using EstanteManiaWebAssembly.Models.DTO_s;
using EstanteManiaWebAssembly.Services.API.Interfaces;

namespace EstanteMania.Web.Services.API
{
    public class BookLocalStorage : IBookLocalStorage
    {
        private const string key = "#BookCollection#";
        private readonly ILocalStorageService localStorageService;
        private readonly IBookService bookService;

        public BookLocalStorage(ILocalStorageService localStorageService, IBookService bookService)
        {
            this.localStorageService = localStorageService;
            this.bookService = bookService;
        }

        public async Task<IEnumerable<BookDTO>> GetCollection()
        {
            return await this.localStorageService.GetItemAsync<IEnumerable<BookDTO>>(key) ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await this.localStorageService.RemoveItemAsync(key);
        }

        private async Task<IEnumerable<BookDTO>> AddCollection()
        {
            var bookCollection = await this.bookService.GetBooksAsync();
            if (bookCollection != null)
            {
                await this.localStorageService.SetItemAsync(key, bookCollection);
            }

            return bookCollection;
        }
    }
}
