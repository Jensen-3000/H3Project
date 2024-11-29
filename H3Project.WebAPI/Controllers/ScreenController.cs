using H3Project.Data.Models.Domain;
using H3Project.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace H3Project.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScreenController : ControllerBase
{
    private readonly ScreenRepository _screenRepository;

    public ScreenController(ScreenRepository screenRepository)
    {
        _screenRepository = screenRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetScreens()
    {
        var screens = await _screenRepository.GetAllScreensAsync();
        return Ok(screens);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetScreen(int id)
    {
        var screen = await _screenRepository.GetScreenByIdAsync(id);

        if (screen == null)
        {
            return NotFound();
        }

        return Ok(screen);
    }

    [HttpPost]
    public async Task<IActionResult> PostScreen(Screen screen)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _screenRepository.AddScreenAsync(screen);

        return CreatedAtAction(nameof(GetScreen), new { id = screen.ScreenId }, screen);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutScreen(int id, Screen screen)
    {
        if (id != screen.ScreenId)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _screenRepository.UpdateScreenAsync(screen);
        }
        catch (Exception)
        {
            var exists = await _screenRepository.GetScreenByIdAsync(id);
            if (exists == null)
            {
                return NotFound();
            }

            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteScreen(int id)
    {
        await _screenRepository.DeleteScreenAsync(id);
        return NoContent();
    }
}
