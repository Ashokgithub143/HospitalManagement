using HospitalManagement.Models;

namespace HospitalManagement.Interfaces
{
    public interface ITokenGenerate
    {
        public Task<string> GenerateToken(User user);

    }
}
