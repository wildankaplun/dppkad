using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using Dppkad.Models;
using Dppkad.Models.Entities;
using Dppkad.BL;
using Dppkad.BL.Repository;

namespace Dppkad.DAL
{
    public interface IAdminService
    {
        void SaveContext();
    }
    public class AdminService : IAdminService
    {
        #region constructor
        private readonly IAdminRepository _realisasiRepository;
        private readonly IGrafikRepository _grafikRepository;

        private readonly IUnitOfWork _unitOfWork;
        #endregion

        public AdminService(IAdminRepository realisasiRepository
            , IGrafikRepository grafikRepository
            , IUnitOfWork unitOfWork)
        {
            _realisasiRepository = realisasiRepository;
            _grafikRepository = grafikRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// commit to database 
        /// </summary>
        public void SaveContext()
        {
            try
            {
                _unitOfWork.Save();
            }
            catch (DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity,
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }


    }
}