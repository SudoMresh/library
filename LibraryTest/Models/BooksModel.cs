using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryTest.Models
{
    public class BooksModel
    {
        public string Author { get; set; }
        public string Title { get; set; }
        [Range(0, 1000, ErrorMessage = "Sorry, but you can not enter number less then 0 or more then 1000.")]
        public int Quantity { get; set; }

        //pagination
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PagerCount { get; set; }

        public List<BookGetModel> Books { get; set; }
    }

    public class TakenBooksModel
    {
        public string Title { get; set; }
        public string Email { get; set; }
        public string DateTime { get; set; }

        //pagination
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PagerCount { get; set; }

        public List<TakenBookViewModel> Books { get; set; }
    }
}