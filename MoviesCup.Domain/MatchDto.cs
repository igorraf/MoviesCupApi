using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesCup.Domain
{
    public class MatchDto
    {
        public MovieDto First { get; set; }
        public MovieDto Second { get; set; }
        public MatchDto PrevA { get; set; }
        public MatchDto PrevB { get; set; }

        public MatchDto(MovieDto first, MovieDto second, MatchDto prevA, MatchDto prevB)
        {
            First = first;
            Second = second;
            PrevA = prevA;
            PrevB = prevB;
        }
    }
}
