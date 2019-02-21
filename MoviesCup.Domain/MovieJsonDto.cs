using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MoviesCup.Domain
{
    [DataContract(Name = "movies")]
    public class MovieJsonDto
    {
        [IgnoreDataMember]
        public int Id { get; set; }

        [DataMember(Name = "id")]
        public string CupId { get; set; }

        [DataMember(Name = "titulo")]
        public string Title { get; set; }

        [DataMember(Name = "ano")]
        public int Year { get; set; }

        [DataMember(Name = "nota")]
        public double CupRate { get; set; }
    }
}
