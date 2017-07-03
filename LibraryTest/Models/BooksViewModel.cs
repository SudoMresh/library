using System.ComponentModel.DataAnnotations;


namespace LibraryTest.Models
{
    public class BooksViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string Author { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [DataType("int")]
        [Range(0, 1000, ErrorMessage = "Sorry, but you can not enter number less then 0 or more then 1000.")]
        public int Quantity { get; set; }

        [DataType("string")]
        public string Taken { get; set; }
    }

    
}