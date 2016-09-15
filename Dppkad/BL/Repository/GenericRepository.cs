using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dppkad.BL.Repository;

namespace Dppkad.BL.Repository
{
    public class GenericRepository<TEntity> : RepositoryBase<TEntity>, IGenericRepository<TEntity> where TEntity : class
    {
        public GenericRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        // public GenericRepository(DatabaseFactory databaseFactory)
        //    : base(databaseFactory)
        //{

        //}

    }
}