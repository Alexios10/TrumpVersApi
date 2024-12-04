using Microsoft.AspNetCore.Mvc;

namespace TrumpVersApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MerchImageUploadController : ControllerBase
{

    private readonly IWebHostEnvironment _webHostEnvironment;

    public MerchImageUploadController(IWebHostEnvironment webHostEnvironment) // IWebHostEnvironment er en hjelpeklasse for å blant annet opprette filstier i Web APIet
    {
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpPost]
    public async Task<IActionResult> Post(IFormFile file) // En IFormFile kan inneholde alle typer filer
    {
        // TODO: ha try catch rundt blokken her
        string webRootPath = _webHostEnvironment.WebRootPath; // WebRootPath er en filsti som brukes innad i Web APIet
        string absolutePath = Path.Combine(webRootPath, "images/merchandises", file.FileName); // Setter sammen en filsti

        using (var fileStream = new FileStream(absolutePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return Created();
    }

}