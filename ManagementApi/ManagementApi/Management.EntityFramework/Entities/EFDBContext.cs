﻿using Management.Domain.Entityes;
using Management.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Management.EntityFramework.Entities
{
    public class EFDBContext : DbContext, IDBContext
    {
        private const string DeleteFilter = "DeleteFilter";
        public EFDBContext():base("name=DBEntities")
        {

        }
        #region Entities
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role{ get; set; }
        public DbSet<UserRoleMap> UserRoleMap { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<RolePermissionMap> RolePermissionMap { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeProjectMap> EmployeeProjectMap { get; set; }
        public DbSet<Departemnt> Departemnt { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Posittion> Posittion { get; set; }



        #endregion
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        public IDBTransaction BeginTrainsaction()
        {
            throw new NotImplementedException();
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void SqlBulkInsert(string tableName, Dictionary<string, string> mapping, DataTable data, int batchSize = 10000, IDBTransaction transaction = null)
        {
            throw new NotImplementedException();
        }
    }
}
