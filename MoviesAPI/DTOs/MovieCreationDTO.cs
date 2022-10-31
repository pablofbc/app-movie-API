using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.DTOs
{
    public class MovieCreationDTO
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Trailer { get; set; }
        public bool InTheaters { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IFormFile Poster { get; set; }

        [ModelBinder(BinderType= typeof(typeBinder<List<int>>))]
        public List<int> GenresIds { get; set; }

        [ModelBinder(BinderType = typeof(typeBinder<List<int>>))]
        public List<int> MovieTheatersIds { get; set; }

        [ModelBinder(BinderType = typeof(typeBinder<List<MoviesActorsCrationDTO>>))]
        public List<MoviesActorsCrationDTO> Actors { get; set; }
    }

}
