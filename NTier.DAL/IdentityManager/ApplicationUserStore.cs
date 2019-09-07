using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NTier.DAL.DBInitialization;
using NTier.DAL.Models;
using NTier.DAL.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.DAL.IdentityManager
{
    public class ApplicationUserStore : UserStore<User, Role, string, IdentityUserLogin, UserRole, IdentityUserClaim>, IUserStore<User>
    {
        public ApplicationUserStore(ApplicationContext context)
           : base(context)
        {

        }
    }
}
