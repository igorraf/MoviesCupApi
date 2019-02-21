using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesCup.Domain
{
    public static class MsgText
    {
        public static string InvalidChampionship { get; } = "Campeonato inválido!";
        public static string PreliminaryRoundChampionship { get; } = "É necessario uma rodada préliminar!";
        public static string UniqueMoviesChampionship { get; } = "Filmes participantes devem ser únicos!";
    }
}
