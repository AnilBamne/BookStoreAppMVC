using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using static System.Reflection.Metadata.BlobBuilder;

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

        /// <summary>
        /// Add new book
        /// </summary>
        /// <param name="bookModel">model</param>
        /// <returns>book model</returns>
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

        public List<BookModel> GetBooks()
        {
            try
            {
                List<BookModel> books = new List<BookModel>();
                using (connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spGetAllBook", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader Reader = sqlCommand.ExecuteReader();

                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            BookModel bookModel = new BookModel()
                            {
                                BookID = Reader.IsDBNull("BookID") ? 0 : Reader.GetInt32("BookID"),
                                BookName = Reader.IsDBNull("BookName") ? String.Empty : Reader.GetString("BookName"),
                                AuthorName = Reader.IsDBNull("AuthorName") ? String.Empty : Reader.GetString("AuthorName"),
                                BookTotalRating = Reader.IsDBNull("BookTotalRating") ? 0 : Reader.GetInt32("BookTotalRating"),
                                TotalPeopleRated = Reader.IsDBNull("TotalPeopleRated") ? 0 : Reader.GetInt32("TotalPeopleRated"),
                                DiscountPrice = Reader.IsDBNull("DiscountPrice") ? 0 : Reader.GetInt32("DiscountPrice"),
                                OriginalPrice = Reader.IsDBNull("OriginalPrice") ? 0 : Reader.GetInt32("OriginalPrice"),
                                BookDescription = Reader.IsDBNull("BookDescription") ? String.Empty : Reader.GetString("BookDescription"),
                                BookImage = Reader.IsDBNull("BookImage") ? String.Empty : Reader.GetString("BookImage"),
                                BookQuantity = Reader.IsDBNull("BookQuantity") ? 0 : Reader.GetInt32("BookQuantity"),
                            };
                            books.Add(bookModel);
                        }
                        return books;
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
