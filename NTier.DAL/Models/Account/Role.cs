﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.DAL.Models.Account
{
    public class Role : IdentityRole<string, UserRole>
    {
    }
}
