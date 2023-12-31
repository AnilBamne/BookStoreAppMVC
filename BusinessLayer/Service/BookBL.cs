﻿using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class BookBL: IBookBL
    {
        private readonly IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }

        public BookModel AddBook(BookModel bookModel)
        {
            return bookRL.AddBook(bookModel);
        }
        public List<BookModel> GetBooks()
        {
            return bookRL.GetBooks();
        }
    }
}
