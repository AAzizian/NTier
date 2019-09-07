using Microsoft.AspNet.Identity.EntityFramework;
using NTier.Common.Utility;
using NTier.DAL.Models.Account;
using NTier.DAL.Models.Authentication;
using NTier.DAL.Models.Global;
using NTier.DAL.ModelsConfiguration.Account;
using NTier.DAL.ModelsConfiguration.Authentication;
using NTier.DAL.ModelsConfiguration.Global;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.DAL.DBInitialization
{
    public class ApplicationContext : IdentityDbContext<User, Role, string, IdentityUserLogin, UserRole, IdentityUserClaim>
    {
        public ApplicationContext()
            : base(Config.ConnectionStringName)
        {

        }

        //Account
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<UserWebClientConnection> UserWebClientConnections { get; set; }
        public DbSet<BrowsingHistory> BrowsingHistory { get; set; }

        //OAuth
        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ClientUsers> ClientUsers { get; set; }

        //Logging
        public DbSet<ErrorLog> ErrorLogs { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Account
            modelBuilder.Configurations.Add(new UserRoleConfig());
            modelBuilder.Entity<User>().ToTable("UserProfile");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Configurations.Add(new UserNotificationConfig());
            modelBuilder.Configurations.Add(new UserWebClientConnectionConfig());
            modelBuilder.Configurations.Add(new BrowsingHistoryConfig());

            //OAuth
            modelBuilder.Configurations.Add(new ClientUsersConfig());
            modelBuilder.Configurations.Add(new ClientConfig());
            modelBuilder.Configurations.Add(new RefreshTokenConfig());

            //Logging
            modelBuilder.Configurations.Add(new ErrorLogConfig());

        }
    }
}
