using OnlineStoreApp.DTOs;
using OnlineStoreApp.Models;

namespace OnlineStoreApp.Interfaces.IServices
{
    public interface IAdministrationService
    {
        public Task<List<OrderDTO>> GetAllOrders();
        public Task<List<UserDTO>> GetWaitingUsers();
        public Task<List<UserDTO>> GetVerifiedUsers();
        public Task<List<UserDTO>> GetBuyers();
        public Task<List<UserDTO>> GetDeclinedUsers();
        public Task SetUserStatus(VerifyDTO verifyDTO);
    }
}
