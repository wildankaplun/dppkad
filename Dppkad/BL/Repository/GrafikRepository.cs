using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using Dppkad.BL;
using Dppkad.BL.Repository;
using Dppkad.Models.Entities;

namespace Dppkad.BL.Repository
{
    public interface IGrafikRepository
    {
        IEnumerable<TGrafik> SelectAll();
        TGrafik SelectById(int Id);
        void Insert(TGrafik obj);
        void Update(TGrafik obj);
        void UpdateWithRowVersion(TGrafik obj, byte[] rowVersion);

        IQueryable<TGrafik> AsQueryable(Expression<Func<TGrafik, bool>> where);
        IQueryable<TGrafik> AsQueryable();
    }

    public class GrafikRepository : RepositoryBase<TGrafik>, IGrafikRepository
    {
        public GrafikRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}