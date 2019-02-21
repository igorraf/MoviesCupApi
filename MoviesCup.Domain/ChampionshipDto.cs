using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesCup.Domain
{
    public class ChampionshipDto
    {
        public List<MatchDto> Matchs { get; set; }
        public ChampionshipDto(List<MatchDto> matchs)
        {
            Matchs = matchs;
        }
    }
}
