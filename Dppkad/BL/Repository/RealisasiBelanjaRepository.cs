using Dppkad.Models.Entities;
using Dppkad.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dppkad.BL.Repository
{
    public class RealisasiBelanjaRepository : RepositoryBase<SP2DDETR>, IRealisasiBelanjaRepository
    {
        public RealisasiBelanjaRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public SP2DDETR SelectByNumber(string number)
        {
            return dbset.Where(o => o.KDKEGUNIT == number).FirstOrDefault();
        }
    }
}