using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
    public class BookRL: IBookRL
    {
        private readonly IConfiguration _configuration;
        private SqlConnection connection;
        public BookRL(IConfiguration configuration)
        {
            this._configuration = configuration;
            this.connection = new SqlConnection(configuration.GetConnectionString("DBConnect"));
        }

        public BookModel AddBook(BookModel bookModel)
        {
            try
            {
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spAddNewBook", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@BookName", bookModel.BookName);
                    sqlCommand.Parameters.AddWithValue("@AuthorName", bookModel.AuthorName);
                    sqlCommand.Parameters.AddWithValue("@BookTotalRating", bookModel.BookTotalRating);
                    sqlCommand.Parameters.AddWithValue("@TotalPeopleRated", bookModel.TotalPeopleRated);
                    sqlCommand.Parameters.AddWithValue("@DiscountPrice", bookModel.DiscountPrice);
                    sqlCommand.Parameters.AddWithValue("@OriginalPrice", bookModel.OriginalPrice);
                    sqlCommand.Parameters.AddWithValue("@BookDescription", bookModel.BookDescription);
                    sqlCommand.Parameters.AddWithValue("@BookImage", bookModel.BookImage);
                    sqlCommand.Parameters.AddWithValue("@BookQuantity", bookModel.BookQuantity);

                    connection.Open();
                    int result = sqlCommand.ExecuteNonQuery();

                    if (result >= 1)
                    {
                        return bookModel;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
