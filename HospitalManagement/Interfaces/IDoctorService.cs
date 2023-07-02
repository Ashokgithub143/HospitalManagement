using HospitalManagement.Models.DTOs;
using HospitalManagement.Models;

namespace HospitalManagement.Interfaces
{
    public interface IDoctorService
    {
        public Task<UserResponse?> AddDcotor(DoctorDTO doctor);
        public Task<Doctor?> DeleteDcotor(int id);
        public Task<Doctor?> GetDcotor(int id);
        public Task<ICollection<Doctor>?> GetAllDcotors();
        public Task<Doctor?> UpdateDoctor(Doctor doctor);
        public Task<Doctor?> ChangeDoctorStatus(ChangeStatus changeStatus);
    }
}
