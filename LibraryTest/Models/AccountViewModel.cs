using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LibraryTest.Models
{
    // For Get and Post methods
    public class AccountViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    // Get admin password
    public class AdminPanelPass
    {
        [Required]
        [DataType(DataType.Text)]
        public string Password { get; set; }
    }
}