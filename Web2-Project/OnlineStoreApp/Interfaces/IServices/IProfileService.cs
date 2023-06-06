using OnlineStoreApp.DTOs;

namespace OnlineStoreApp.Interfaces.IServices
{
    public interface IProfileService
    {
        public Task<UserDTO> GetProfile(int id);
        public Task EditProfile(int id, EditProfileDTO profile);
    }
}
