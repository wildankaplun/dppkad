using Dppkad.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dppkad.BL.Repository
{
    public interface IRealisasiBelanjaRepository
    {
        IEnumerable<SP2DDETR> SelectAll();
        SP2DDETR SelectById(int Id);
        void Insert(SP2DDETR obj);
        void Update(SP2DDETR obj);
        void UpdateWithRowVersion(SP2DDETR obj, byte[] rowVersion);
        SP2DDETR SelectByNumber(string number);

        IQueryable<SP2DDETR> AsQueryable(Expression<Func<SP2DDETR, bool>> where);
        IQueryable<SP2DDETR> AsQueryable();
    }
}