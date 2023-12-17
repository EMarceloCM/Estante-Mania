using EstanteMania.Web.Models;

namespace EstanteManiaWebAssembly.Services.Authentication
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
    }
}
