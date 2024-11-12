using System.ComponentModel.DataAnnotations;
using System.Data;

namespace SDDUserApi.Data.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string EmailId { get; set; }
        public string PasswordHash { get; set; }        
        public string MobileNo  { get; set; }
        public string City { get; set; }
        public UserRole UserRole { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDateTime { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Createdby { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}
