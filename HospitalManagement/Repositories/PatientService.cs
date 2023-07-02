using HospitalManagement.Interfaces;
using HospitalManagement.Models.DTOs;
using HospitalManagement.Models;
using System.Security.Cryptography;
using System.Text;

namespace HospitalManagement.Repositories
{
    public class PatientService:IPatientService
    {
        private readonly IRepo<int, Patient> _repo;
        private readonly ITokenGenerate _tokenService;

        public PatientService(IRepo<int, Patient> repo, ITokenGenerate tokenService)
        {
            _repo = repo;
            _tokenService = tokenService;
        }
        public async Task<UserResponse?> AddPatient(PatientDTO patient)
        {
            UserResponse user = null;
            var hmac = new HMACSHA512();
            patient.User.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(patient.PasswordClear));
            patient.User.PasswordKey = hmac.Key;
            patient.User.Role = "Patient";
            var addedPatient = await _repo.Add(patient);
            if (addedPatient != null)
            {
                user = new UserResponse
                {
                    Id = addedPatient.User.Id,
                    Role = addedPatient.User.Role,
                    Token = await _tokenService.GenerateToken(addedPatient.User)
                };
                return user;
            }
            return null;
        }

        public async Task<Patient?> DeletePatient(int id)
        {
            var deletedPatient = await _repo.Delete(id);
            if (deletedPatient != null)
            {
                return deletedPatient;
            }
            return null;
        }

        public async Task<ICollection<Patient>?> GetAllPatients()
        {
            var patients = await _repo.GetAll();
            if (patients != null)
            {
                return patients;
            }
            return null;
        }

        public async Task<Patient?> GetPatient(int id)
        {
            var patient = await _repo.Get(id);
            if (patient != null)
            {
                return patient;
            }
            return null;
        }

        public async Task<Patient?> UpdatePatient(Patient patient)
        {
            var updatedPatient = await _repo.Update(patient);
            if (updatedPatient != null)
            {
                return updatedPatient;
            }
            return null;
        }
    }
}
