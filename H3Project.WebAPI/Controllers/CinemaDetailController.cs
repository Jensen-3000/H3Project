using H3Project.Data.Models.Domain;
using H3Project.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CinemaDetailController : ControllerBase
{
    private readonly CinemaDetailRepository _cinemaDetailRepository;

    public CinemaDetailController(CinemaDetailRepository cinemaDetailRepository)
    {
        _cinemaDetailRepository = cinemaDetailRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetCinemaDetails()
    {
        var cinemaDetails = await _cinemaDetailRepository.GetAllCinemaDetailsAsync();
        return Ok(cinemaDetails);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCinemaDetail(int id)
    {
        var cinemaDetail = await _cinemaDetailRepository.GetCinemaDetailByIdAsync(id);

        if (cinemaDetail == null)
        {
            return NotFound();
        }

        return Ok(cinemaDetail);
    }

    [HttpPost]
    public async Task<IActionResult> PostCinemaDetail(CinemaDetail cinemaDetail)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _cinemaDetailRepository.AddCinemaDetailAsync(cinemaDetail);

        return CreatedAtAction(nameof(GetCinemaDetail), new { id = cinemaDetail.CinemaDetailId }, cinemaDetail);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCinemaDetail(int id, CinemaDetail cinemaDetail)
    {
        if (id != cinemaDetail.CinemaDetailId)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _cinemaDetailRepository.UpdateCinemaDetailAsync(cinemaDetail);
        }
        catch (Exception)
        {
            var exists = await _cinemaDetailRepository.GetCinemaDetailByIdAsync(id);
            if (exists == null)
            {
                return NotFound();
            }

            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCinemaDetail(int id)
    {
        await _cinemaDetailRepository.DeleteCinemaDetailAsync(id);
        return NoContent();
    }
}
