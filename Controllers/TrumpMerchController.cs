using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrumpVersApi.Contexts;
using TrumpVersApi.Models;

namespace TrumpVersApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TrumpMerchController : ControllerBase
{
    private readonly ApplicationDbContext _trumpMerchContext;

    public TrumpMerchController(ApplicationDbContext trumpMerchContext)
    {
        _trumpMerchContext = trumpMerchContext;
    }

    [HttpGet]
    public async Task<List<TrumpMerch>> Get()
    {
        List<TrumpMerch> merch = await _trumpMerchContext.TrumpMerch.ToListAsync();
        return merch;
    }

    [HttpGet("byid/{id}")]
    public async Task<ActionResult<TrumpMerch?>> Get(int id)
    {
        TrumpMerch? merch = await _trumpMerchContext.TrumpMerch.FindAsync(id);
        return merch;
    }

    [HttpGet("byname/{name}")]
    public async Task<ActionResult<TrumpMerch?>> Get(string name)
    {
        TrumpMerch? merchandise = await _trumpMerchContext.TrumpMerch
            .FirstOrDefaultAsync(member => member.Name == name);

        if (merchandise == null)
        {
            return NotFound($"Member with name '{name}' not found.");
        }
        return Ok(merchandise);
    }

    [HttpPost]
    public async Task<ActionResult<TrumpMerch>> Post(TrumpMerch newMerch)
    {
        _trumpMerchContext.TrumpMerch.Add(newMerch);
        await _trumpMerchContext.SaveChangesAsync();
        return newMerch;
    }

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

    [HttpDelete("{id}")]
    public async Task<ActionResult<TrumpMerch>> Delete(int id)
    {
        TrumpMerch? merchToDelete = await _trumpMerchContext.TrumpMerch.FindAsync(id);

        if (merchToDelete != null)
        {
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