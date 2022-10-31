using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{


    [Route("api/genres")]
    [ApiController]
    public class GenresController : Controller
    {

        private readonly ILogger<GenresController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GenresController(ILogger<GenresController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        
        [HttpGet, Route("selectListGenres")]
        public async Task<ActionResult<List<GenreDTO>>> Get()
        {
            logger.LogInformation("Getting all the genres");
            var genres = await context.Genres.OrderBy(x => x.Name).ToListAsync();
            return mapper.Map<List<GenreDTO>>(genres);
        }

        
        [HttpGet, Route("selectGenre/{id}")]
        public async Task<ActionResult<GenreDTO>> Get(int id)
        {
            var genre = await context.Genres.FirstOrDefaultAsync(x => x.Id == id);
            if (genre == null)
            {
                return NotFound();
            }
            return mapper.Map<GenreDTO>(genre);
        }

        
        [HttpPost, Route("saveGenre")]
        public async Task<ActionResult> Post([FromBody]GenreCreationDTO genreCreationDTO)
        {
            var genre = mapper.Map<Genre>(genreCreationDTO);    
            context.Add(genre);
            await context.SaveChangesAsync();
            return Ok();
        }

        
        [HttpPut, Route("updateGenre/{id}")]
        public async Task<ActionResult> Put (int id,[FromBody] GenreCreationDTO genreCreationDTO)
        {
            var genre = mapper.Map<Genre>(genreCreationDTO);
            genre.Id = id;
            context.Entry(genre).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok();
        }

        
        [HttpDelete, Route("deleteGenre/{id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var genre = await context.Genres.FirstOrDefaultAsync(x => x.Id == Id);
            if (genre == null)
            {
                return NotFound();
            }
            context.Remove(genre);
            await context.SaveChangesAsync();
            return Ok();
                   
        }
                       
    }
}
