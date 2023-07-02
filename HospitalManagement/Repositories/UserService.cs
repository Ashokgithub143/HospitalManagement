using HospitalManagement.Interfaces;
using HospitalManagement.Models.DTOs;
using HospitalManagement.Models;
using System.Security.Cryptography;
using System.Text;

namespace HospitalManagement.Repositories
{
    public class UserService:IManagerUser
    {

        private readonly IRepo<int, User> _userRepo;
        private readonly ITokenGenerate _tokenService;
        private readonly IDoctorService _doctorService;

        public UserService(IRepo<int, User> userRepo, ITokenGenerate tokenService, IDoctorService doctorService)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
            _doctorService = doctorService;
        }


        public async Task<UserResponse?> Login(UserRequest requestUser)
        {
            UserResponse? responseUser = null;
            var users = await _userRepo.GetAll();
            if (users != null)
            {
                var user = users.FirstOrDefault(u => u.Email == requestUser.Email);
                if (user != null)
                {
                    var hmac = new HMACSHA512(user.PasswordKey);
                    var userpass = hmac.ComputeHash(Encoding.UTF8.GetBytes(requestUser.Password));
                    for (int i = 0; i < userpass.Length; i++)
                    {
                        if (userpass[i] != user.PasswordHash[i])
                        {
                            return null;
                        }
                    }
                    responseUser = new UserResponse();
                    responseUser.Id = user.Id;
                    responseUser.Role = user.Role;
                    if (user.Role != "Doctor")
                    {
                        responseUser.Token = await _tokenService.GenerateToken(user);
                        return responseUser;
                    }
                    var doctor = await _doctorService.GetDcotor(user.Id);
                    if (doctor != null && doctor.Status == "Not Approved")
                    {
                        return responseUser;
                    }
                    responseUser.Token = await _tokenService.GenerateToken(user);
                    return responseUser;

                }
                return null;
            }
            return null;
        }


        public async Task<UserResponse?> AdminRegistration(UserDTO user)
        {
            UserResponse? userResponse = null;
            var hmac = new HMACSHA512();
            if (user.PasswordClear != null)
            {
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.PasswordClear));
                user.PasswordKey = hmac.Key;
                user.Role = "Admin";
                var addedUser = await _userRepo.Add(user);
                if (addedUser != null)
                {
                    userResponse = new UserResponse
                    {
                        Id = addedUser.Id,
                        Role = addedUser.Role,
                        Token = await _tokenService.GenerateToken(addedUser)
                    };
                    return userResponse;
                }
            }
            return null;
        }
    }
}
