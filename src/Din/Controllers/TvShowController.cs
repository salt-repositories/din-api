using System.Threading.Tasks;
using AutoMapper;
using Din.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TMDbLib.Objects.Search;

namespace Din.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TvShowController : ControllerBase
    {
        private readonly ITvShowService _service;
        private readonly IMapper _mapper;

        public TvShowController(ITvShowService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [Authorize, HttpGet]
        public async Task<IActionResult> GetAllTvShows()
        {
            return Ok(await _service.GetAllTvShowsAsync());
        }

        [Authorize, HttpGet("{id}")]
        public async Task<IActionResult> GetTvShowById([FromRoute] int id)
        {
            return Ok(await _service.GetTvShowByIdAsync(id));
        }

        [HttpPost("search"), Authorize]
        public async Task<IActionResult> SearchTvShowAsync(string query)
        {
            if (string.IsNullOrEmpty(query)) return BadRequest();

            return Ok(await _service.SearchTvShowAsync(query));
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> AddTvShowAsync(string tvShowData)
        {
            try
            {
                var tvShow = JsonConvert.DeserializeObject<SearchTv>(tvShowData);

                if (tvShow == null) return BadRequest();

                return Ok(await _service.AddTvShowAsync(tvShow, 1));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}