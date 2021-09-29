using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Genre
    {
        // We get our genre values, it needs to be the same as in our sql so the sql knows what the values are
        public int GenreID { get; set; }
        public string GenreName { get; set; }
    }
}