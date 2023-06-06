using AutoMapper;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using OnlineStoreApp.DTOs;
using OnlineStoreApp.Exceptions;
using OnlineStoreApp.Interfaces;
using OnlineStoreApp.Interfaces.IServices;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using BC = BCrypt.Net;

namespace OnlineStoreApp.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProfileService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task EditProfile(int id, EditProfileDTO profile)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == id) ?? throw new UnauthorizedException("Error with id in token. Logout and login again");
            if (!string.IsNullOrEmpty(profile.Password) && !string.IsNullOrEmpty(profile.NewPassword))
            {
                if (!BC.BCrypt.Verify(profile.Password, user.Password))
                    throw new BadRequestException("Password doesn't match");

                user.Password = BC.BCrypt.HashPassword(profile.NewPassword);
            }

            user.Address = profile.Address;
            if(user.Email != profile.Email)
                if ((await _unitOfWork.Users.Get(x => x.Email == profile.Email)) != null)
                    throw new BadRequestException("Email already exists.");

            user.Email = profile.Email;
            user.Birthday = profile.Birthday;
            user.FullName = profile.FullName;
            if(profile.ImageFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    profile.ImageFile.CopyTo(ms);
                    user.Image = ms.ToArray();
                }
            }

            if (user.Username != profile.Username)
                if ((await _unitOfWork.Users.Get(x => x.Username == profile.Username)) != null)
                    throw new BadRequestException("Username already exists.");
            user.Username = profile.Username;

            _unitOfWork.Users.Update(user);
            await _unitOfWork.Save();
        }

        public async Task<UserDTO> GetProfile(int id)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == id) ?? throw new UnauthorizedException("Error with id in token. Logout and login again"); 
            return _mapper.Map<UserDTO>(user);
        }
    }
}
