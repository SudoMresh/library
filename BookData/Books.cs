using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookData
{
    class Books
    {
        Books() { }

        Books(string author, string title, int quantity)
        {
            Author = author;
            Title = title;
            Quantity = quantity;

        }

        public string Author { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
    }
}
