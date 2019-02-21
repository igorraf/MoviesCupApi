using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MoviesCup.Domain
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string CupId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public double CupRate { get; set; }
    }
}
