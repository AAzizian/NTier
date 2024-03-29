﻿using NTier.DAL.DBInitialization;
using NTier.DAL.Models.Account;
using NTier.DAL.Models.Authentication;
using NTier.DAL.Models.Global;
using NTier.DAL.Repositories;
using NTier.DAL.Repositories.IRepositories;
using NTier.DAL.UnitOfWork.IUoW;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NTier.DAL.UnitOfWork
{
    public class AppUnitOfWork : IUnitOfWork
    {
        private ApplicationContext context;

        //Users
        private IUserRepository userRepository;
        private IRepository<Role> roleRepository;
        private IRepository<UserRole> userRoleRepository;
        private IRepository<UserWebClientConnection> userWebClientConnectionRepository;
        private IRepository<BrowsingHistory> browsingHistoryRepository;

        //Web API
        private IRepository<Client> clientRepository;
        private IRepository<RefreshToken> refreshTokenRepository;
        private IRepository<ClientUsers> clientUsersRepository;

        //Logging
        private IRepository<ErrorLog> errorLogRepository;



        public AppUnitOfWork(bool autoDetectChangesEnabled = true, bool validateOnSaveEnabled = true)
        {
            context = new ApplicationContext();
            context.Configuration.AutoDetectChangesEnabled = autoDetectChangesEnabled;
            context.Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;
        }

        //stored procedure

        public async Task<int> ExecuteStoredProcedureAsync(string storedProcedureName)
        {
            return await context.Database.ExecuteSqlCommandAsync(storedProcedureName);
        }

        public async Task<int> ExecuteStoredProcedureAsync(string storedProcedureName, SqlParameter[] parameters)
        {
            return await context.Database.ExecuteSqlCommandAsync(storedProcedureName, parameters);
        }

        //Bulk insert
        public void BulkInsert(string connectionString, string destinationTableName, DataTable dataTable, List<string> dataTableProperties)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = destinationTableName;

                    foreach (var prop in dataTableProperties)
                    {
                        bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(prop, prop));
                    }

                    try
                    {
                        bulkCopy.WriteToServer(dataTable);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
        }


        //Users
        public IUserRepository UserRepository
        {
            get { return userRepository ?? (userRepository = new UserRepository(context)); }
        }
        public IRepository<Role> RoleRepository
        {
            get { return roleRepository ?? (roleRepository = new GenericRepository<Role>(context)); }
        }
        public IRepository<UserRole> UserRoleRepository
        {
            get { return userRoleRepository ?? (userRoleRepository = new GenericRepository<UserRole>(context)); }
        }
        public IRepository<UserWebClientConnection> UserWebClientConnectionRepository
        {
            get { return userWebClientConnectionRepository ?? (userWebClientConnectionRepository = new GenericRepository<UserWebClientConnection>(context)); }
        }
        public IRepository<BrowsingHistory> BrowsingHistoryRepository
        {
            get { return browsingHistoryRepository ?? (browsingHistoryRepository = new GenericRepository<BrowsingHistory>(context)); }
        }


        //Web API
        public IRepository<Client> ClientRepository
        {
            get { return clientRepository ?? (clientRepository = new GenericRepository<Client>(context)); }
        }
        public IRepository<RefreshToken> RefreshTokenRepository
        {
            get { return refreshTokenRepository ?? (refreshTokenRepository = new GenericRepository<RefreshToken>(context)); }
        }
        public IRepository<ClientUsers> ClientUsersRepository
        {
            get { return clientUsersRepository ?? (clientUsersRepository = new GenericRepository<ClientUsers>(context)); }
        }

        //Error log
        public IRepository<ErrorLog> ErrorLogRepository
        {
            get { return errorLogRepository ?? (errorLogRepository = new GenericRepository<ErrorLog>(context)); }
        }



        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return context.SaveChangesAsync(cancellationToken);
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
