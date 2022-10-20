using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using MoviesAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        //private readonly IFileStorageService fileStorageService;
        //private string container = "movies";

        public MoviesController(ApplicationDbContext context, IMapper mapper)//, IFileStorageService fileStorageService)
        {
            this.context = context;
            this.mapper = mapper;
            //this.fileStorageService = fileStorageService;
        }

        [HttpPost, Route("saveMovie")]
        public async Task<ActionResult> Post([FromForm] MovieCreationDTO movieCreationDTO)
        {
            var movie = mapper.Map<Movie>(movieCreationDTO);
            //if(movieCreationDTO.Poster != null)
            //{
            //    movie.Poster = await fileStorageService.SaveFile(container, movieCreationDTO.Poster);
            //}
            AnnotateActorsOrder(movie);
            context.Add(movie);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private void AnnotateActorsOrder(Movie movie)
        {
            if (movie.MoviesActors != null)
            {
                for (int i = 0; i< movie.MoviesActors.Count; i++)
                {
                    movie.MoviesActors[i].Order = i;
                }
            }
        }
    }
}
