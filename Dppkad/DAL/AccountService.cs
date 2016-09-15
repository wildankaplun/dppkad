using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dppkad.Models;
using Dppkad.Models.Entities;
using Dppkad.BL;
using Dppkad.BL.Repository;

namespace Dppkad.DAL
{
    public interface IAccountService
    {
        TUser Login(string accountId, string password);
    }
    /// <summary>
    /// authentication service
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IGenericRepository<TUser> _userRepository;

        public AccountService(IGenericRepository<TUser> userRepository)
        {
            this._userRepository = userRepository;
        }

        /// <summary>
        /// authenticate user by id and password using AD
        /// </summary>
        /// <param name="accountId">user id</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        public TUser Login(string accountId, string password)
        {
            var userdata = new TUser();
            try
            {
                userdata = _userRepository.AsQueryable(u => u.UserName == accountId && u.UserPassword.Equals(password)).FirstOrDefault();

                return userdata;
            }
            catch (Exception ex)
            {
                // Failed to authenticate.
                // Most likely it is caused by unknown user id or bad password.
                Console.WriteLine(ex.ToString() + "\r\nUserName: " + accountId);
                return userdata;
            }
        }
    }
}