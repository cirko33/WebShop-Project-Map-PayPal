using OnlineStoreApp.DTOs;
using OnlineStoreApp.Models;

namespace OnlineStoreApp.Interfaces.IServices
{
    public interface IAuthService
    {
        public Task<string> Login(LoginDTO loginDTO);
        public Task Register(RegisterDTO registerDTO);

        public Task<string> GoogleSignIn(TokenDTO token);
    }
}
