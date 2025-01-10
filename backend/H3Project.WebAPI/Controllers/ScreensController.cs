using H3Project.Data.DTOs.Screens;
using H3Project.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreensController : ControllerBase
    {
        private readonly IScreenService _screenService;

        public ScreensController(IScreenService screenService)
        {
            _screenService = screenService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScreenSimpleDto>>> GetAll()
        {
            return Ok(await _screenService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScreenDetailedDto>> GetById(int id)
        {
            return Ok(await _screenService.GetByIdAsync(id));
        }

        [HttpGet("cinema/{cinemaId}")]
        public async Task<ActionResult<IEnumerable<ScreenSimpleDto>>> GetByCinema(int cinemaId)
        {
            return Ok(await _screenService.GetByCinemaAsync(cinemaId));
        }

        [HttpPost]
        public async Task<ActionResult<ScreenSimpleDto>> Create(ScreenCreateDto createDto)
        {
            var screen = await _screenService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = screen.Id }, screen);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ScreenUpdateDto updateDto)
        {
            await _screenService.UpdateAsync(id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _screenService.DeleteAsync(id);
            return NoContent();
        }
    }
}
