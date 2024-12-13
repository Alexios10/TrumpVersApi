using Microsoft.AspNetCore.Mvc;

namespace TrumpVersApi.Controllers;

// definerer at dette en en API-controller
[ApiController]
[Route("api/[controller]")]
public class StaffImageUploadController : ControllerBase
{

    // lagrer informasjon fra webserveren
    private readonly IWebHostEnvironment _webHostEnvironment;

    // IWebHostEnvironment er en hjelpeklasse for å blant annet opprette filstier i Web APIet
    public StaffImageUploadController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    // Http POST metode fot å laste opp bilder
    [HttpPost]
    public async Task<IActionResult> Post(IFormFile file)
    // En IFormFile kan inneholde alle typer filer
    {
        // WebRootPath er en filsti som brukes innad i Web APIet
        string webRootPath = _webHostEnvironment.WebRootPath;
        string absolutePath = Path.Combine(webRootPath, "images/staff-members", file.FileName); // Setter sammen en filsti

        using (var fileStream = new FileStream(absolutePath, FileMode.Create))
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