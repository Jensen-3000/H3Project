using H3Project.Data.DTOs.SeatAvailabilities;
using H3Project.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeatAvailabilitiesController : ControllerBase
    {
        private readonly ISeatAvailabilityService _service;

        public SeatAvailabilitiesController(ISeatAvailabilityService service)
        {
            _service = service;
        }

        [HttpGet("screening/{screeningId}")]
        public async Task<ActionResult<IEnumerable<SeatAvailabilitySimpleDto>>> GetByScreening(int screeningId)
        {
            return Ok(await _service.GetByScreeningAsync(screeningId));
        }

        [HttpGet("screening/{screeningId}/seat/{seatId}")]
        public async Task<ActionResult<SeatAvailabilityDetailedDto>> GetByScreeningAndSeat(int screeningId, int seatId)
        {
            return Ok(await _service.GetByScreeningAndSeatAsync(screeningId, seatId));
        }

        [HttpPatch("toggle")]
        public async Task<ActionResult<bool>> ToggleAvailability(SeatAvailabilityToggleDto toggleDto)
        {
            var isAvailable = await _service.ToggleAvailabilityAsync(toggleDto);
            return Ok(new { IsAvailable = isAvailable });
        }
    }
}
