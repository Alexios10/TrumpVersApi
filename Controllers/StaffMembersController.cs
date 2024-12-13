using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrumpVersApi.Contexts;
using TrumpVersApi.Models;

namespace TrumpVersApi.Controllers;

// Definerer at dette er en API-controller
[ApiController]
[Route("api/[controller]")]

public class StaffMembersController : ControllerBase
{
    // Lagrer konteksten for databasen som inneholder member
    private readonly ApplicationDbContext _staffMembersContext;

    // Konstruktør som tar inn ApplicationDbContext
    public StaffMembersController(ApplicationDbContext staffMembersContext)
    {
        _staffMembersContext = staffMembersContext;
    }

    // Http GET-metode for å hente alle members
    [HttpGet]
    public async Task<List<StaffMembers>> Get()
    {
        // Henter listen over member fra databasen asynkront
        List<StaffMembers> staffMember = await _staffMembersContext.StaffMembers.ToListAsync();
        return staffMember;
    }

    // Http GET-metode for å hente en member basert på ID
    [HttpGet("byid/{id}")]
    public async Task<ActionResult<StaffMembers?>> Get(int id)
    {
        // Søker etter member med gitt ID
        StaffMembers? staffMembers = await _staffMembersContext.StaffMembers.FindAsync(id);
        return staffMembers;
    }

    // Http GET-metode for å hente en member basert på navn
    [HttpGet("byname/{name}")]
    public async Task<ActionResult<StaffMembers?>> Get(string name)
    {
        // Søker etter den første member med gitt navn
        StaffMembers? staffMembers = await _staffMembersContext.StaffMembers
            .FirstOrDefaultAsync(thought => thought.Name == name);

        // Hvis ingen ansatt ble funnet, returneres 404 Not Found
        if (staffMembers == null)
        {
            return NotFound($"Member with name '{name}' not found.");
        }
        return Ok(staffMembers); // Returnerer den funnet ansatte
    }

    // Http POST-metode for å legge til en ny memner
    [HttpPost]
    public async Task<ActionResult<StaffMembers>> Post(StaffMembers newStaffMember)
    {
        _staffMembersContext.StaffMembers.Add(newStaffMember);
        await _staffMembersContext.SaveChangesAsync();
        return newStaffMember;
    }

    // Http PUT-metode for å oppdatere en member
    [HttpPut]
    public async Task<ActionResult<StaffMembers>> Put(StaffMembers updateMember)
    {
        try
        {
            // Setter tilstanden til member til endret
            _staffMembersContext.Entry(updateMember).State = EntityState.Modified;
            await _staffMembersContext.SaveChangesAsync();
            return updateMember; // Returnerer den oppdaterte member
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError); // Returnerer 500 ved feil
        }
    }

    // Http DELETE-metode for å slette en member
    [HttpDelete("{id}")]
    public async Task<ActionResult<StaffMembers>> Delete(int id)
    {
        // Søker etter member med gitt ID
        StaffMembers? staffToDelete = await _staffMembersContext.StaffMembers.FindAsync(id);

        if (staffToDelete != null)
        {
            // Fjerner member fra databasen
            _staffMembersContext.StaffMembers.Remove(staffToDelete);
            await _staffMembersContext.SaveChangesAsync();
            return staffToDelete; // Returnerer den slettede member
        }
        else
        {
            return NotFound(); // Returnerer 404 Not Found hvis ansatt ikke finnes
        }
    }
}