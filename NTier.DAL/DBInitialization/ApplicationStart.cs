﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.DAL.DBInitialization
{
    public class ApplicationStart
    {
        public void InitializeDatabase()
        {
            Database.SetInitializer<ApplicationContext>(new DatabaseInitialization());
            var context = new ApplicationContext();
            context.Database.Initialize(true);
        }
    }
}
