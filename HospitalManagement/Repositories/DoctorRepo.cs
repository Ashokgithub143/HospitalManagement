using HospitalManagement.Interfaces;
using HospitalManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Repositories
{
    public class DoctorRepo:IRepo<int, Doctor>
    {

        private readonly HospitalDbContext _context;
        private readonly ILogger<DoctorRepo> _logger;

        public DoctorRepo(HospitalDbContext context, ILogger<DoctorRepo> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Doctor?> Add(Doctor item)
        {
            try
            {
                _context.Doctors.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Doctor?> Delete(int key)
        {
            try
            {
                var doctor = await Get(key);
                if (doctor != null)
                {
                    _context.Doctors.Remove(doctor);
                    if (doctor.User != null)
                    {
                        _context.Users.Remove(doctor.User);
                    }
                    await _context.SaveChangesAsync();
                    return doctor;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Doctor?> Get(int key)
        {
            try
            {
                var user = await _context.Doctors.Include(d => d.User).FirstOrDefaultAsync(d => d.DoctorId == key);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<ICollection<Doctor>?> GetAll()
        {
            try
            {
                var doctors = await _context.Doctors.Include(d => d.User).ToListAsync();
                if (doctors.Count > 0)
                    return doctors;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Doctor?> Update(Doctor item)
        {
            try
            {
                var doctor = await Get(item.DoctorId);
                if (doctor != null)
                {
                    doctor.DoctorId = item.DoctorId;
                    doctor.Name = item.Name;
                    doctor.Address = item.Address;
                    doctor.Gender = item.Gender;
                    doctor.PhoneNumber = item.PhoneNumber;
                    doctor.DateOfBirth = item.DateOfBirth;
                    doctor.Specialization = item.Specialization;
                    doctor.Qualifications = item.Qualifications;
                    doctor.Status = item.Status;
                    doctor.LicenseNumber = item.LicenseNumber;
                    doctor.Experience = item.Experience;
                    await _context.SaveChangesAsync();
                    return doctor;
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
