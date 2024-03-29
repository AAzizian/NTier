﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using NTier.DAL.DBInitialization;
using NTier.DAL.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.DAL.IdentityManager
{
    public class ApplicationRoleManager : RoleManager<Role>
    {
        private ApplicationContext context = new ApplicationContext();
        public ApplicationRoleManager(IRoleStore<Role, string> roleStore)
            : base(roleStore)
        {

        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var appRoleManager = new ApplicationRoleManager(new ApplicationRoleStore(context.Get<ApplicationContext>()));
            return appRoleManager;
        }
    }
}
