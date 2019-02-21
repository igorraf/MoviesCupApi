using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesCup.Domain
{
    public static class MovieExtension
    {
        public static MovieDto ToDto(this MovieJsonDto jsonObj)
        {
            MovieDto dto = new MovieDto()
            {
                Id = jsonObj.Id,
                CupId = jsonObj.CupId,
                CupRate = jsonObj.CupRate,
                Title = jsonObj.Title,
                Year = jsonObj.Year
            };

            return dto;
        }

        public static List<MovieDto> ToDto(this List<MovieJsonDto> list)
        {
            if (list == null)
                return new List<MovieDto>();

            return list.ConvertAll(item => item.ToDto());
        }
    }
}
