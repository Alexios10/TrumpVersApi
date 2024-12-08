using Microsoft.AspNetCore.Mvc;

namespace TrumpVersApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MerchImageUploadController : ControllerBase
{

    private readonly IWebHostEnvironment _webHostEnvironment;

    public MerchImageUploadController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpPost]
    public async Task<IActionResult> Post(IFormFile file)
    {
        try
        {

            string webRootPath = _webHostEnvironment.WebRootPath;
            string absolutePath = Path.Combine(webRootPath, "images/merchandises", file.FileName);

            using (var fileStream = new FileStream(absolutePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Created();
        }
        catch (System.Exception)
        {

            throw;
        }

    }

}