using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrumpVersApi.Contexts;
using TrumpVersApi.Models;

namespace TrumpVersApi.Controllers;

// definerer at dette er en API-controller
[ApiController]
[Route("api/[controller]")]

public class TrumpMerchController : ControllerBase
{
    // Lagrer kotekst for databasen som inneholder Trump merchendise
    private readonly ApplicationDbContext _trumpMerchContext;

    public TrumpMerchController(ApplicationDbContext trumpMerchContext)
    {
        _trumpMerchContext = trumpMerchContext;
    }

    // Http Get-metode for å hente all Trump merchandise
    [HttpGet]
    public async Task<List<TrumpMerch>> Get()
    {
        // henter lister over merchandise fra databasen asynkront
        List<TrumpMerch> merch = await _trumpMerchContext.TrumpMerch.ToListAsync();
        return merch;
    }

    // Http GET-metode for å hente merch basert på ID
    [HttpGet("byid/{id}")]
    public async Task<ActionResult<TrumpMerch?>> Get(int id)
    {
        // Søker etter mech med gitt ID
        TrumpMerch? merch = await _trumpMerchContext.TrumpMerch.FindAsync(id);
        return merch;
    }

    // Http GET-metode for å hente merchandise basert på navn
    [HttpGet("byname/{name}")]
    public async Task<ActionResult<TrumpMerch?>> Get(string name)
    {
        // søker etter den første merchandise med gitt navn
        TrumpMerch? merchandise = await _trumpMerchContext.TrumpMerch
            .FirstOrDefaultAsync(member => member.Name == name);

        // Hvis ingen merchandise ble funnet, returneres 404 Not found error
        if (merchandise == null)
        {
            return NotFound($"Member with name '{name}' not found.");
        }
        return Ok(merchandise); // Returnerer den funnede merchandise
    }

    // Http POST-metode for å legge til ny merch
    [HttpPost]
    public async Task<ActionResult<TrumpMerch>> Post(TrumpMerch newMerch)
    {
        try
        {
            // Legger til den nye merch i databasen
            _trumpMerchContext.TrumpMerch.Add(newMerch);
            await _trumpMerchContext.SaveChangesAsync();
            return newMerch;// Returnerer den nye merchandise
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    // Http PYT-metode for å oppdatere eksisterende merch
    [HttpPut]
    public async Task<ActionResult<TrumpMerch>> Put(TrumpMerch updateMerch)
    {
        try
        {
            _trumpMerchContext.Entry(updateMerch).State = EntityState.Modified;
            await _trumpMerchContext.SaveChangesAsync();
            return updateMerch;
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    // Http DELETE-metode for å slette merch
    [HttpDelete("{id}")]
    public async Task<ActionResult<TrumpMerch>> Delete(int id)
    {
        TrumpMerch? merchToDelete = await _trumpMerchContext.TrumpMerch.FindAsync(id);

        if (merchToDelete != null)
        {
            // fjerne merch fra listen
            _trumpMerchContext.TrumpMerch.Remove(merchToDelete);
            await _trumpMerchContext.SaveChangesAsync();
            return merchToDelete;
        }
        else
        {
            return NotFound();
        }
    }
}