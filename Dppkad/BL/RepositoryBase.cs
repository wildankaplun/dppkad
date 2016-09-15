using Dppkad.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dppkad.BL
{
    public class RepositoryBase<TEntity> where TEntity : class
    {
        private DppkadEntities db = null;
        protected readonly IDbSet<TEntity> dbset;

        public RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<TEntity>();
        }

        //public RepositoryBase(DatabaseFactory databaseFactory)
        //{

        //    DatabaseFactory = databaseFactory;
        //    dbset = DataContext.Set<TEntity>();
        //}

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected DppkadEntities DataContext
        {
            get { return db ?? (db = DatabaseFactory.Get()); }
        }


        public IEnumerable<TEntity> SelectAll()
        {
            return dbset.AsEnumerable();
        }

        public TEntity SelectById(int Id)
        {
            return dbset.Find(Id);
        }

        public TEntity SelectByNullableId(int? Id)
        {
            return dbset.Find(Id);
        }

        public TEntity SelectByIdForCustVisitSurvey(int? id)
        {
            return dbset.Find(id);
        }

        public void Insert(TEntity obj)
        {
            dbset.Add(obj);
        }

        public void Update(TEntity obj)
        {
            dbset.Attach(obj);
            db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(TEntity obj)
        {
            dbset.Remove(obj);
        }

        public void UpdateWithRowVersion(TEntity obj, byte[] rowVersion)
        {
            dbset.Attach(obj);
            db.Entry(obj).OriginalValues["RowVersion"] = rowVersion;
            db.Entry(obj).State = EntityState.Modified;
        }

        public IQueryable<TEntity> AsQueryable(System.Linq.Expressions.Expression<Func<TEntity, bool>> where)
        {
            return dbset.AsQueryable().Where(where);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return dbset.AsQueryable();
        }
    }
}
