using AdviceMovieAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdviceMovie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private List<Movie> movies = new List<Movie>()
            {
                new Movie()
                {
                    Id = 1,
                    Name = "Zindanlar ve Ejderhalar: Hırsızlar Arasında Onur (2023)",
                    Comment = "\r\nThis movie is such a gem"
                },
                 new Movie()
                {
                    Id = 2,
                    Name = "Şeytanın Düşmanı",
                    Comment = "At The Beginning Of The New Millennium, more than one observer described R. Crowe as \"The Next Richard Burton\""
                }
            };
        [HttpGet]
        [Route("getMovies")]
        public ActionResult<Movie> GetMovies()
        {

            return Ok(movies);
        }
        [HttpGet]
        [Route("getMovie")]
        public ActionResult<Movie> GetMovie(int id)
        {
            var movie = movies.Find(x => x.Id == id);
            if (movie == null)
                return BadRequest("No movie found!");
            return Ok(movies);
        }
    }
}