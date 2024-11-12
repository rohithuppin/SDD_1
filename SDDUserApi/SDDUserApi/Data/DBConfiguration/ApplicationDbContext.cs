using Microsoft.EntityFrameworkCore;
using SDDUserApi.Data.Model;
using System.Collections.Generic;

namespace SDDUserApi.Data.DBConfiguration
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<AuditTrail> AuditTrails { get; set; }

        public DbSet<UserRoles> UserRoles { get; set; }
    }
}
