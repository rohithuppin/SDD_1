using System.ComponentModel.DataAnnotations;

namespace SDDUserApi.Data.Model
{
    public class AuditTrail
    {
        [Key]
        public int AuditId { get; set; }
        public string ActionName { get; set; }
        public int UserId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
