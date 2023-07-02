using HospitalManagement.Models.DTOs;
using HospitalManagement.Models;

namespace HospitalManagement.Interfaces
{
    public interface IPatientService
    {
        public Task<UserResponse?> AddPatient(PatientDTO patient);
        public Task<Patient?> DeletePatient(int id);
        public Task<Patient?> GetPatient(int id);
        public Task<ICollection<Patient>?> GetAllPatients();
        public Task<Patient?> UpdatePatient(Patient patient);
    }
}
