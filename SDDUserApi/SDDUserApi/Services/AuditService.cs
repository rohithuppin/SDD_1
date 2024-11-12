using SDDUserApi.Data.DBConfiguration;
using SDDUserApi.Data.Model;

namespace SDDUserApi.Services
{
    public class AuditService : IAuditService
    {
        private readonly ApplicationDbContext _context;

        public AuditService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void LogActivity(int userId, string action, string details)
        {
            var auditTrail = new AuditTrail
            {
                UserId = userId,
                ActionName = action,
                Timestamp = DateTime.UtcNow
            };
            _context.AuditTrails.Add(auditTrail);
            _context.SaveChanges();
        }
    }
}
