using Management.Domain.Entityes;
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
