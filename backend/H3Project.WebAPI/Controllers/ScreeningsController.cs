using H3Project.Data.DTOs.Screenings;
using H3Project.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScreeningsController : ControllerBase
    {
        private readonly IScreeningService _screeningService;

        public ScreeningsController(IScreeningService screeningService)
        {
            _screeningService = screeningService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScreeningSimpleDto>>> GetAll()
        {
            return Ok(await _screeningService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScreeningAvailableSeatsDto>> GetById(int id)
        {
            return Ok(await _screeningService.GetByIdAsync(id));
        }

        [HttpGet("movie/{movieId}")]
        public async Task<ActionResult<IEnumerable<ScreeningSimpleDto>>> GetByMovie(int movieId)
        {
            return Ok(await _screeningService.GetByMovieAsync(movieId));
        }

        [HttpGet("daterange")]
        public async Task<ActionResult<IEnumerable<ScreeningSimpleDto>>> GetByDateRange(
            [FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            return Ok(await _screeningService.GetByDateRangeAsync(start, end));
        }

        [HttpPost]
        public async Task<ActionResult<ScreeningSimpleDto>> Create(ScreeningCreateDto createDto)
        {
            var screening = await _screeningService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = screening.Id }, screening);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ScreeningUpdateDto updateDto)
        {
            await _screeningService.UpdateAsync(id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _screeningService.DeleteAsync(id);
            return NoContent();
        }
    }
}
