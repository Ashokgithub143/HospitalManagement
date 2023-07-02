using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Models
{
    public class Doctor
    {

        [Key]
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public User? User { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Specialization { get; set; }
        public string? Qualifications { get; set; }
        public string? LicenseNumber { get; set; }
        public int Experience { get; set; }
        public string? Status { get; set; }
    }
}
