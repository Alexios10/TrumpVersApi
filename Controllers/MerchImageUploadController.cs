using Microsoft.AspNetCore.Mvc;

namespace TrumpVersApi.Controllers;

// Definerer at dette er en API-controller
[ApiController]
[Route("api/[controller]")]
public class MerchImageUploadController : ControllerBase
{


    private readonly IWebHostEnvironment _webHostEnvironment;

    public MerchImageUploadController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    // Http POST-metode for å laste opp bilder
    [HttpPost]
    public async Task<IActionResult> Post(IFormFile file)
    {
        try
        {
            // henter rootpath for webserveren
            string webRootPath = _webHostEnvironment.WebRootPath;

            // setter opp absolutepath for lagring av bildet
            string absolutePath = Path.Combine(webRootPath, "images/merchandises", file.FileName);

            // åpner en filestream for å skrive bildet til serveren
            using (var fileStream = new FileStream(absolutePath, FileMode.Create))
            {
                // kopierer innholder fra opplastet fil til filestream
                await file.CopyToAsync(fileStream);
            }

            // returnerer en 201 created respons når opplasting er vellyket
            return Created();
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

    }

}