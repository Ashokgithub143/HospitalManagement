namespace HospitalManagement.Models.DTOs
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string? Token { get; set; }
        public string? Role { get; set; }
    }
}
