using HospitalManagement.Interfaces;
using HospitalManagement.Models.DTOs;
using HospitalManagement.Models;
using System.Security.Cryptography;
using System.Text;

namespace HospitalManagement.Repositories
{
    public class DoctorService:IDoctorService
    {
        private readonly IRepo<int, Doctor> _repo;
        private readonly IRepo<int, User> _userRepo;
        private readonly ITokenGenerate _tokenService;

        public DoctorService(IRepo<int, Doctor> repo, ITokenGenerate tokenService, IRepo<int, User> userRepo)
        {
            _repo = repo;
            _userRepo = userRepo;
            _tokenService = tokenService;
        }
        public async Task<UserResponse?> AddDcotor(DoctorDTO doctor)
        {
            UserResponse? user = null;
            var hmac = new HMACSHA512();
            if (doctor.User != null)
            {
                if (doctor.PasswordClear != null)
                {
                    doctor.User.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(doctor.PasswordClear));
                }
                doctor.User.PasswordKey = hmac.Key;
                doctor.Status = "Not Approved";
                doctor.User.Role = "Doctor";
                var addedDoctor = await _repo.Add(doctor);
                if (addedDoctor != null && addedDoctor.User != null)
                {
                    user = new UserResponse
                    {
                        Id = addedDoctor.User.Id,
                        Role = addedDoctor.User.Role,
                        Token = await _tokenService.GenerateToken(addedDoctor.User)
                    };
                    return user;
                }
            }
            return null;
        }

        public async Task<Doctor?> ChangeDoctorStatus(ChangeStatus changeStatusDTO)
        {
            var doctor = await _repo.Get(changeStatusDTO.DoctorId);
            if (doctor != null)
            {
                doctor.Status = changeStatusDTO.UpdatedStatus;
                var updatedDoctor = await _repo.Update(doctor);
                return updatedDoctor;
            }
            return null;
        }

        public async Task<Doctor?> DeleteDcotor(int id)
        {
            var deletedDoctor = await _repo.Delete(id);
            if (deletedDoctor != null)
            {
                return deletedDoctor;
            }
            return null;
        }

        public async Task<ICollection<Doctor>?> GetAllDcotors()
        {
            var doctors = await _repo.GetAll();
            if (doctors != null)
            {
                return doctors;
            }
            return null;
        }

        public async Task<Doctor?> GetDcotor(int id)
        {
            var doctor = await _repo.Get(id);
            if (doctor != null)
            {
                return doctor;
            }
            return null;
        }

        public async Task<Doctor?> UpdateDoctor(Doctor doctor)
        {
            var updatedDoctor = await _repo.Update(doctor);
            if (updatedDoctor != null)
            {
                return updatedDoctor;
            }
            return null;
        }
    }
}
