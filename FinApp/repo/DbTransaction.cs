using FinApp.Entities.Database;
using System;
using System.Data.Entity;


namespace FinApp.repo
{
    public class DbTransaction : IDisposable
    {
        public DbContextTransaction dbContextTransaction { get; set; }
        private FinContext finContext;

        public DbTransaction(FinContext finContext)
        {
            this.finContext = finContext;
        }

        public DbContextTransaction begin()
        {
            dbContextTransaction = finContext.Database.BeginTransaction();
            return dbContextTransaction;
        }

        public void Commit()
        {
            dbContextTransaction.Commit();
        }

        public void Rollback()
        {
            dbContextTransaction.Rollback();
        }

        public void Dispose()
        {
            dbContextTransaction.Dispose();
        }
    }
}