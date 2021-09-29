using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Movies
    {
        // We get our Movie values, it needs to be the same as in our sql so the sql knows what the values are
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public DateTime DateOfRelease { get; set; }
        public string Genre { get; set; }
        public string PhotoFileName { get; set; }
    }
}