using Dppkad.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dppkad.BL
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private DppkadEntities dataContext;
        public DppkadEntities Get()
        {
            return dataContext ?? (dataContext = new DppkadEntities());
        }

        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}