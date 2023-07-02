using HospitalManagement.Models.DTOs;

namespace HospitalManagement.Interfaces
{
    public interface IManagerUser
    {
        public Task<UserResponse?> Login(UserRequest user);
        public Task<UserResponse?> AdminRegistration(UserDTO user);
    }
}
