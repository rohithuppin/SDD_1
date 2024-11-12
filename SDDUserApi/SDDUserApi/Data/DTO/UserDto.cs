using SDDUserApi.Data.Model;

namespace SDDUserApi.Data.DTO
{
    public class UserDto
    {
        public string Fullname { get; set; }
        public string EmailId { get; set; }
        public int UserId { get; set; }
        public string PasswordHash { get; set; }
        public string MobileNo { get; set; }
        public string City { get; set; }
        public UserRole UserRole { get; set; }
    }
}
