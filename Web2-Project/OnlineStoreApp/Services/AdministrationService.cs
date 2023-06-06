using AutoMapper;
using OnlineStoreApp.DTOs;
using OnlineStoreApp.Exceptions;
using OnlineStoreApp.Interfaces;
using OnlineStoreApp.Interfaces.IServices;
using OnlineStoreApp.Models;

namespace OnlineStoreApp.Services
{
    public class AdministrationService : IAdministrationService
    {
        IUnitOfWork _unitOfWork;
        IMailService _mailService;
        IMapper _mapper;
        public AdministrationService(IUnitOfWork unitOfWork, IMailService mailService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
            _mapper = mapper;
        }

        public async Task<List<OrderDTO>> GetAllOrders()
        {
            var orders = await _unitOfWork.Orders.GetAll(null, x => x.OrderByDescending(y => y.OrderTime), new List<string> { "Items" });
            return _mapper.Map<List<OrderDTO>>(orders);
        }

        public async Task<List<UserDTO>> GetBuyers()
        {
            var users = await _unitOfWork.Users.GetAll(x => x.Type == UserType.Buyer);
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<List<UserDTO>> GetDeclinedUsers()
        {
            var users = await _unitOfWork.Users.GetAll(x => x.VerificationStatus == VerificationStatus.Declined && x.Type == UserType.Seller);
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<List<UserDTO>> GetVerifiedUsers()
        {
            var users = await _unitOfWork.Users.GetAll(x => x.VerificationStatus == VerificationStatus.Accepted && x.Type == UserType.Seller);
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<List<UserDTO>> GetWaitingUsers()
        {
            var users = await _unitOfWork.Users.GetAll(x => x.VerificationStatus == VerificationStatus.Waiting && x.Type == UserType.Seller);
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task SetUserStatus(VerifyDTO verifyDTO)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == verifyDTO.Id);
            if (user == null)
                throw new BadRequestException("User with this ID doesn't exist.");

            if (user.VerificationStatus != VerificationStatus.Waiting)
                throw new BadRequestException("Only verify waiting users");

            user.VerificationStatus = verifyDTO.VerificationStatus;
            _unitOfWork.Users.Update(user);

            string message = user.VerificationStatus == VerificationStatus.Accepted ? $"You have been verified.\r\nYou can now sell." : "Your verification has been denied.\r\nPlease contact administrators.";
            _ = Task.Run(async() => await _mailService.SendEmail("Verification status", message, user.Email!));
            await _unitOfWork.Save();
        }
    }
}
