using Dppkad.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dppkad.BL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory databaseFactory;
        private DppkadEntities dataContext;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        //public UnitOfWork(DatabaseFactory databaseFactory)
        //{
        //    this.databaseFactory = databaseFactory;
        //}

        protected DppkadEntities DataContext
        {
            get { return dataContext ?? (dataContext = databaseFactory.Get()); }
        }

        public void Save()
        {
            DataContext.SaveChanges();
        }

        public async Task<bool> SaveAsync()
        {
            await DataContext.SaveChangesAsync();
            return true;
        }

    }
}
