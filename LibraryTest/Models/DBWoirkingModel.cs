using System;
using System.Collections.Generic;
using System.Data.SqlClient;



namespace LibraryTest.Models
{
    public class UsersDBWorking
    {
        public void AddNewUserToDB(AccountViewModel account)
        {
            using (SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MreSh\Desktop\Programs\LibraryTest\LibraryTest\App_Data\Users.mdf;"))
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO UsersDB(Email)
                                            values('"+account.Email+"')", sqlConnection);

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                cmd.Connection.Close();
            }
        }

        public string GetUserEmailFromDB(AccountViewModel account)
        {
            using (SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MreSh\Desktop\Programs\LibraryTest\LibraryTest\App_Data\Users.mdf;"))
            {
                SqlCommand cmd = new SqlCommand(@"SELECT Email FROM UsersDB WHERE Email = '" + account.Email+"'", sqlConnection);

                cmd.Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        account.Email = reader.GetString(0);
                    }

                    cmd.Connection.Close();
                    return account.Email;
                }
                else return "null";
            }
        }

    }

    public class BookDbWorking
    {
        public void AddNewBookToDB(BooksViewModel book)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MreSh\Desktop\Programs\LibraryTest\LibraryTest\App_Data\Books.mdf");

            book.Taken = "Not taken";
            SqlCommand cmd = new SqlCommand(@"INSERT INTO BooksInLibrary(Author, Title, Quantity, Taken)
                                            values('" + book.Author+"','"+book.Title+"','"+book.Quantity+"','" + book.Taken + "')", sqlConnection);

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();

        }

        // Post to database taken book to trace the book and update value Taken in other data table
        public void TakeBook(string book, string email, DateTime date, int quantity)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MreSh\Desktop\Programs\LibraryTest\LibraryTest\App_Data\Books.mdf");
            string takeDate = date.ToShortDateString();

            SqlCommand cmdToTakeBook = new SqlCommand(@"INSERT INTO TakingBooks(Title, Email, Date)
                                                        values('"+ book +"','"+ email +"','"+ takeDate +"')", sqlConnection);

            SqlCommand cmd = new SqlCommand(@"UPDATE BooksInLibrary SET Taken = 'Taken', Quantity ='"+ quantity +"' WHERE Title='"+ book +"'", sqlConnection);

            cmdToTakeBook.Connection.Open();
            cmdToTakeBook.ExecuteNonQuery();
            cmdToTakeBook.Connection.Close();

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            

        }

        public void DeleteBookFromDB(string book)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MreSh\Desktop\Programs\LibraryTest\LibraryTest\App_Data\Books.mdf");

            SqlCommand cmd = new SqlCommand(@"DELETE FROM BooksInLibrary WHERE
                                            Author ='" + book + "'", sqlConnection);

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }

        public void EditBookValuesInDB(BooksViewModel book)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MreSh\Desktop\Programs\LibraryTest\LibraryTest\App_Data\Books.mdf");

            SqlCommand cmd = new SqlCommand(@"UPDATE BooksInLibrary SET Quantity ='"+ book.Quantity 
                + "' WHERE Title='" + book.Title + "'", sqlConnection);

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }

        public List<BookGetModel> GetBooksFromDB()
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MreSh\Desktop\Programs\LibraryTest\LibraryTest\App_Data\Books.mdf");
            List<BookGetModel> booksFromDB = new List<BookGetModel>();

            SqlCommand cmd = new SqlCommand(@"SELECT Author, Title, Quantity, Taken FROM BooksInLibrary" , sqlConnection);
            cmd.Connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            BookGetModel book = new BookGetModel();


            if (reader.HasRows)
            {
                
                while (reader.Read())
                {
                    book.Author = reader.GetString(0);
                    book.Title = reader.GetString(1);
                    book.Quantity = reader.GetInt32(2);
                    book.Taken = reader.GetString(3);


                    booksFromDB.Add(book);
                    book = new BookGetModel();
                }
            }

            cmd.Connection.Close();
            return booksFromDB;
        }

        public List<TakenBookViewModel> GetTakenBooks(string book)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MreSh\Desktop\Programs\LibraryTest\LibraryTest\App_Data\Books.mdf");
            List<TakenBookViewModel> takenBooks = new List<TakenBookViewModel>();
            SqlCommand cmd = new SqlCommand(@"SELECT Title, Email, Date From TakingBooks WHERE Title ='"+ book +"'", sqlConnection);
            TakenBookViewModel bookToTake = new TakenBookViewModel();

            cmd.Connection.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    bookToTake.Title = reader.GetString(0);
                    bookToTake.Email = reader.GetString(1);
                    bookToTake.DateTime = reader.GetString(2);

                    takenBooks.Add(bookToTake);
                    bookToTake = new TakenBookViewModel();
                }
            }

            return takenBooks;
        }

    }
}