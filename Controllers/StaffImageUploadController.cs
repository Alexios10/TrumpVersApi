using Microsoft.AspNetCore.Mvc;

namespace TrumpVersApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaffImageUploadController : ControllerBase
{

    private readonly IWebHostEnvironment _webHostEnvironment;

    public StaffImageUploadController(IWebHostEnvironment webHostEnvironment) // IWebHostEnvironment er en hjelpeklasse for å blant annet opprette filstier i Web APIet
    {
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpPost]
    public async Task<IActionResult> Post(IFormFile file) // En IFormFile kan inneholde alle typer filer
    {
        try
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            string absolutePath = Path.Combine(webRootPath, "images/staff-members", file.FileName);

            using (var fileStream = new FileStream(absolutePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Created();
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

    }

}