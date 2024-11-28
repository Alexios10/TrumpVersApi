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

    [HttpGet("{id}")]
    public async Task<ActionResult<TrumpMerch?>> Get(int id)
    {
        TrumpMerch? merch = await _trumpMerchContext.TrumpMerch.FindAsync(id);
        return merch;
    }


    // [HttpPost]
    // public async Task<ActionResult<TrumpThoughts>> Post([FromBody] TrumpThoughts newThought)
    // {
    //     newThought.DateCreated = DateTime.UtcNow;
    //     _trumpMerchContext.TrumpThoughts.Add(newThought);
    //     await _trumpMerchContext.SaveChangesAsync();
    //     return newThought;
    // }

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