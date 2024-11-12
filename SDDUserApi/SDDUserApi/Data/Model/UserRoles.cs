namespace SDDUserApi.Data.Model
{
    public class UserRoles
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Createdby { get; set; }
        public bool? IsActive { get; set; }
    }
}
