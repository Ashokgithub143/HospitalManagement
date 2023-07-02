using HospitalManagement.Interfaces;
using HospitalManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Repositories
{
    public class PatientRepo:IRepo<int,Patient>
    {
        private readonly HospitalDbContext _context;
        private readonly ILogger<PatientRepo> _logger;

        public PatientRepo(HospitalDbContext context, ILogger<PatientRepo> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Patient?> Add(Patient item)
        {
            try
            {
                _context.Patients.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Patient?> Delete(int key)
        {
            try
            {
                var patient = await Get(key);
                if (patient != null)
                {
                    _context.Patients.Remove(patient);
                    if (patient.User != null)
                    {
                        _context.Users.Remove(patient.User);
                    }
                    await _context.SaveChangesAsync();
                    return patient;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Patient?> Get(int key)
        {
            try
            {
                var user = await _context.Patients.Include(p => p.User).FirstOrDefaultAsync(p => p.PatientId == key);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<ICollection<Patient>?> GetAll()
        {
            try
            {
                var patients = await _context.Patients.Include(u => u.User).ToListAsync();
                if (patients.Count > 0)
                    return patients;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Patient?> Update(Patient item)
        {
            try
            {
                var patient = await Get(item.PatientId);
                if (patient != null)
                {
                    patient.PatientId = item.PatientId;
                    patient.Name = item.Name;
                    patient.Address = item.Address;
                    patient.Gender = item.Gender;
                    patient.PhoneNumber = item.PhoneNumber;
                    patient.DateOfBirth = item.DateOfBirth;
                    patient.BloodGroup = item.BloodGroup;
                    await _context.SaveChangesAsync();
                    return patient;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}

