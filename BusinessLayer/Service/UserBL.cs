using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL:IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public UserModel AddUser(UserModel model)
        {
            try
            {
                return userRL.AddUser(model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string Login(LoginModel model)
        {
            return userRL.Login(model);
        }
    }
}
