using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LibraryTest.Models;
using LibraryTest.SmtpServer;

namespace LibraryTest.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TrackBooks()
        {
            BooksModel model = new BooksModel();
            model.PageSize = 10;

            BookDbWorking dataBase = new BookDbWorking();

            List<BookGetModel> books = dataBase.GetBooksFromDB();

            if (books != null)
            {
                model.TotalCount = books.Count;
                model.Books = books;
            }
            ViewBag.Model = model;
            return View();
        }

        public ActionResult ShowBooksTakenByUser(string book)
        {
            TakenBooksModel model = new TakenBooksModel();
            model.PageSize = 10;

            BookDbWorking dataBase = new BookDbWorking();

            List<TakenBookViewModel> books = dataBase.GetTakenBooks(book);

            if (books != null)
            {
                model.TotalCount = books.Count;
                model.Books = books;
            }
            ViewBag.Model = model;
            return View();
        }


        // Shows all books where user can take a book
        public ActionResult TakeBook()
        {
            BooksModel model = new BooksModel();
            model.PageSize = 10;

            BookDbWorking dataBase = new BookDbWorking();

            List<BookGetModel> books = dataBase.GetBooksFromDB();

            if (books != null)
            {
                model.TotalCount = books.Count;
                model.Books = books;
            }
            ViewBag.Model = model;
            return View();
        }


        // User get the book and send title of the book to email address
        [HttpGet]
        public ActionResult GiveBookToUser(string book, int quantity)
        {
            BookDbWorking dataBase = new BookDbWorking();
            DateTime date = DateTime.Now;

            --quantity;

            dataBase.TakeBook(book, Session["UserMail"].ToString(), date, quantity);

            EmailService sendEmail = new EmailService();

            sendEmail.SendEmail(book, Session["UserMail"].ToString());

            return RedirectToAction("TakeBook");
        }

        public ActionResult AddNewBooks()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewBooks(BooksViewModel book)
        {
            if (ModelState.IsValid)
            {
                BookDbWorking dataBase = new BookDbWorking();

                dataBase.AddNewBookToDB(book);
                ModelState.Clear();
            }
            return View();
        }


        // Admin panale
        [HttpGet]
        public ActionResult ManageBook()
        {
            BooksModel model = new BooksModel();
            model.PageSize = 10;

            BookDbWorking dataBase = new BookDbWorking();

            List<BookGetModel> books = dataBase.GetBooksFromDB();

            if (books != null)
            {
                model.TotalCount = books.Count;
                model.Books = books;
            }
            ViewBag.Model = model;

            return View();
        }

        
        // Select book by quantity
        public ActionResult GetBooksByQuantity(int quantity, string book)
        {
            BookDbWorking dataBase = new BookDbWorking();
            var books = dataBase.GetBooksFromDB().Where((x) => x.Title == book); 

            if (books != null)
            {
                BooksModel model = new BooksModel();

                foreach (var item in books)
                {
                    model.Quantity = item.Quantity;
                    model.Title = item.Title;
                   
                }

                ViewBag.Model = model;
                return View("GridEdit");
            }

            return RedirectToAction("ManageBook");
        }


        // Delete books from database
        [HttpGet]
        public ActionResult Delete(string book)
        {
            BookDbWorking dataBase = new BookDbWorking();

            dataBase.DeleteBookFromDB(book);

            return RedirectToAction("ManageBook");
        }


        // Change quantity in database
        [HttpPost]
        public ActionResult UpdateBooks(BooksViewModel book)
        {
            BookDbWorking dataBase = new BookDbWorking();

            // Always will be more then 0
            book.Quantity = Math.Abs(book.Quantity);

            dataBase.EditBookValuesInDB(book);

            return RedirectToAction("ManageBook");
        }

    }
}