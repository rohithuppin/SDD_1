namespace SDDUserApi.Services
{
    public interface IAuditService
    {

        public void LogActivity(int userId, string action, string details);
    }
}
