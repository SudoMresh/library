using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LibraryTest.Models
{
    public class AdminViewModel
    {
        [Required]
        [DataType("string")]
        public string Password { get; set; }
    }

}