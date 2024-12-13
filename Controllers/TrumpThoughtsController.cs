using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrumpVersApi.Contexts;
using TrumpVersApi.Models;

namespace TrumpVersApi.Controllers;

// Definerer klasse som en API-controller
[ApiController]
[Route("api/[controller]")]

public class TrumpThoughtsController : ControllerBase
{
    // definerer kontekst for databasen
    private readonly ApplicationDbContext _trumpThoughtsContext;

    public TrumpThoughtsController(ApplicationDbContext trumpThoughtsContext)
    {
        _trumpThoughtsContext = trumpThoughtsContext;
    }

    // Henter alle thoughts
    [HttpGet]
    public async Task<List<TrumpThoughts>> Get()
    {
        // henter alle thoughts fra databasen
        List<TrumpThoughts> thoughts = await _trumpThoughtsContext.TrumpThoughts.ToListAsync();
        return thoughts;
    }

    // Henter en spesifikk thought basert på ID
    [HttpGet("byid/{id}")]
    public async Task<ActionResult<TrumpThoughts?>> Get(int id)
    {
        // søker etter thought med gitt ID
        TrumpThoughts? thought = await _trumpThoughtsContext.TrumpThoughts.FindAsync(id);
        return thought;
    }

    // henter thought basert på navn
    [HttpGet("byname/{name}")]
    public async Task<ActionResult<TrumpThoughts?>> Get(string name)
    {
        // filterer thought basert på navn
        var thoughts = await _trumpThoughtsContext.TrumpThoughts
            .Where(thought => thought.Name == name)
            .ToListAsync();

        // sjekker om thought ble funnet
        if (!thoughts.Any())
        {
            return NotFound($"No TrumpThoughts with the name '{name}' found.");
        }


        return Ok(thoughts);
    }

    // Legge til en ny thought
    [HttpPost]
    public async Task<ActionResult<TrumpThoughts>> Post(TrumpThoughts newThought)
    {
        // setter dato for opprettelse til nåværende tid - ekstra funskjon 😃
        newThought.DateCreated = DateTime.UtcNow;
        // legge til den nye thought i databasen
        _trumpThoughtsContext.TrumpThoughts.Add(newThought);
        await _trumpThoughtsContext.SaveChangesAsync();
        return newThought;
    }

    // Oppdaterer en eksisterende thought
    [HttpPut]
    public async Task<ActionResult<TrumpThoughts>> Put(TrumpThoughts updateThought)
    {
        try
        {
            _trumpThoughtsContext.Entry(updateThought).State = EntityState.Modified;
            await _trumpThoughtsContext.SaveChangesAsync();
            return updateThought;
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }


    // sletter en spesifikk thought basert på ID 
    [HttpDelete("{id}")]
    public async Task<ActionResult<TrumpThoughts>> Delete(int id)
    {
        // søker etter thought som skal slettes
        TrumpThoughts? thoughtToDelete = await _trumpThoughtsContext.TrumpThoughts.FindAsync(id);

        if (thoughtToDelete != null)
        {
            // fjerner thought fra dataabsen
            _trumpThoughtsContext.TrumpThoughts.Remove(thoughtToDelete);
            await _trumpThoughtsContext.SaveChangesAsync();
            return thoughtToDelete;
        }
        else
        {
            // returnerer ikke funnet hvis thought ikke eksisterer i databasen
            return NotFound();
        }
    }
}