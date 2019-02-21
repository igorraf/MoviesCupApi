using Microsoft.AspNetCore.Http;
using MoviesCup.Api.Models;
using MoviesCup.Domain;
using MoviesCup.Infrastructure.Middleware;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MoviesCup.Infrastructure
{
    public class ChampionshipModel
    {
        MoviesCupApiContext _context;

        public ChampionshipModel(MoviesCupApiContext context)
        {
            _context = context;
        }

        public ChampionshipDto Championship(List<int> moviesId)
        {
            var list = moviesId.ConvertAll((id) => _context.Find<MovieDto>(id));
            return Championship(list);
        }

        public ChampionshipDto Championship(List<string> moviesCupId)
        {
            var list = moviesCupId
                .ConvertAll((id) => 
                    _context.MovieDto.First((item) => item.CupId.Equals(id,StringComparison.InvariantCultureIgnoreCase)));
            return Championship(list);
        }

        public ChampionshipDto Championship(List<MovieDto> movies)
        {
            Validation(movies);

            movies.Sort((a, b) => {
                if (String.IsNullOrEmpty(a.Title)) return -1;
                if (String.IsNullOrEmpty(b.Title)) return -1;
                return a.Title.CompareTo(b.Title);
            });

            List<MatchDto> lstMatch = movies.ConvertAll(item => new MatchDto(item, null, null, null));

            lstMatch = Games(lstMatch);

            return new ChampionshipDto(lstMatch);

        }

        public List<MatchDto> Games(List<MatchDto> matchs)
        {
            for (int i = 0, l= matchs.Count-1; i < matchs.Count / 2; i++, l--)
            {
                matchs[i] = Fight(matchs[i].First, matchs[l].First, clearLeaf(matchs[i]), clearLeaf(matchs[l]));
                matchs.RemoveAt(l);
            }

            if (matchs.Count == 1)
                return matchs;
            return Games(matchs);
        }

        public MatchDto clearLeaf(MatchDto match)
        {
            if (match.Second == null && match.PrevA == null && match.PrevB == null)
            {
                return null;
            }
            return match;
        }

        internal MatchDto Fight(MovieDto movieA, MovieDto movieB, MatchDto prevA, MatchDto prevB)
        {
            if (movieA.CupRate > movieB.CupRate)
            {
               return new MatchDto(movieA, movieB, prevA, prevB);
            }

            if (movieA.CupRate < movieB.CupRate)
            {
                return new MatchDto(movieB, movieA, prevA, prevB);
            }

            if (String.IsNullOrEmpty(movieA.Title))
            {
                return new MatchDto(movieB, movieA, prevA, prevB);
            }

            if (String.IsNullOrEmpty(movieA.Title))
            {
                return new MatchDto(movieA, movieB, prevA, prevB);
            }

            if (movieA.Title.CompareTo(movieB.Title) <= 0)
            {
                return new MatchDto(movieA, movieB, prevA, prevB);
            }
            else
            {
                return new MatchDto(movieB, movieA, prevA, prevB);
            }
        }

        public void Validation(List<MovieDto> movies)
        {
            if (movies == null || movies.Count == 0)
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, MsgText.InvalidChampionship);
            }

            if (!movies.TrueForAll(item => movies.Count(cItem => cItem.Id == item.Id) == 1))
            {
                var duplicate = movies.Where(item => movies.Count(cItem => cItem.Id == item.Id) > 1)
                    .Select(item => item.CupId).Distinct();

                var msgObj = JObject.FromObject(new
                {
                    Msg = String.Format("{0}, Filmes: {1}",
                    MsgText.UniqueMoviesChampionship, String.Join(", ",duplicate))
                });

                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, msgObj);
            }

            var level = Math.Log(movies.Count, 2);

            if (level % 1 != 0)
            {
                var msgObj = JObject.FromObject(new {
                    Msg = String.Format("{0}, reduza o grupo dé {1} para {2}",
                    MsgText.PreliminaryRoundChampionship, movies.Count, Math.Pow(2, Math.Truncate(level)))
                });

                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, msgObj);
            }
        }
    }
}
