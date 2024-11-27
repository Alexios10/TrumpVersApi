using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrumpVersApi.Contexts;
using TrumpVersApi.Models;

namespace TrumpVersApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TrumpThoughtsController : ControllerBase
{
    private readonly TrumpThoughtsContext _trumpThoughtsContext;

    public TrumpThoughtsController(TrumpThoughtsContext trumpThoughtsContext)
    {
        _trumpThoughtsContext = trumpThoughtsContext;
    }

    [HttpGet]
    public async Task<List<TrumpThoughts>> Get()
    {
        List<TrumpThoughts> thoughts = await _trumpThoughtsContext.TrumpThoughts.ToListAsync();
        return thoughts;
    }

    [HttpGet("byid/{id}")]
    public async Task<ActionResult<TrumpThoughts?>> Get(int id)
    {
        TrumpThoughts? thought = await _trumpThoughtsContext.TrumpThoughts.FindAsync(id);
        return thought;
    }

    [HttpGet("byname/{name}")]
    public async Task<ActionResult<TrumpThoughts?>> Get(string name)
    {
        TrumpThoughts? thought = await _trumpThoughtsContext.TrumpThoughts
            .FirstOrDefaultAsync(t => t.Name == name);

        if (thought == null)
        {
            return NotFound($"TrumpThought with name '{name}' not found.");
        }
        return Ok(thought);
    }


    [HttpPost]
    public async Task<ActionResult<TrumpThoughts>> Post([FromBody] TrumpThoughts newThought)
    {
        newThought.DateCreated = DateTime.UtcNow;
        _trumpThoughtsContext.TrumpThoughts.Add(newThought);
        await _trumpThoughtsContext.SaveChangesAsync();
        return newThought;
    }

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

    [HttpDelete("{id}")]
    public async Task<ActionResult<TrumpThoughts>> Delete(int id)
    {
        TrumpThoughts? thoughtToDelete = await _trumpThoughtsContext.TrumpThoughts.FindAsync(id);

        if (thoughtToDelete != null)
        {
            _trumpThoughtsContext.TrumpThoughts.Remove(thoughtToDelete);
            await _trumpThoughtsContext.SaveChangesAsync();
            return thoughtToDelete;
        }
        else
        {
            return NotFound();
        }
    }
}