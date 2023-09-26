using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL: IUserRL
    {
        private readonly IConfiguration _configuration;
        private SqlConnection connection;
        public UserRL(IConfiguration configuration)
        {
            this._configuration = configuration;
            this.connection = new SqlConnection(configuration.GetConnectionString("DBConnect"));
        }
        
        public UserModel AddUser(UserModel model)
        {
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spAddUser", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@FullName", model.UserName);
                    command.Parameters.AddWithValue("@EmailId", model.UserEmail);
                    command.Parameters.AddWithValue("@Password", model.UserPassword);
                    command.Parameters.AddWithValue("@MobileNumber", model.MobileNumber);
                    connection.Open();
                    int count=command.ExecuteNonQuery();
                    if(count > 0)
                    {
                        return model;
                    }
                }
                    return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }

        public string Login(LoginModel model)
        {
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spValidateUser", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmailId", model.UserEmail);
                    command.Parameters.AddWithValue("@Password", model.UserPassword);
                    connection.Open();
                    int count = command.ExecuteNonQuery();
                    if (count > 0)
                    {
                        return "Loging Successfull";
                    }
                }
                return "Login Failed";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State==ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }
    }
}
