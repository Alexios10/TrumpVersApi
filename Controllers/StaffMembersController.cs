using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrumpVersApi.Contexts;
using TrumpVersApi.Models;

namespace TrumpVersApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class StaffMembersController : ControllerBase
{
    private readonly ApplicationDbContext _staffMembersContext;

    public StaffMembersController(ApplicationDbContext staffMembersContext)
    {
        _staffMembersContext = staffMembersContext;
    }

    [HttpGet]
    public async Task<List<StaffMembers>> Get()
    {
        List<StaffMembers> staffMember = await _staffMembersContext.StaffMembers.ToListAsync();
        return staffMember;
    }

    [HttpGet("byid/{id}")]
    public async Task<ActionResult<StaffMembers?>> Get(int id)
    {
        StaffMembers? staffMembers = await _staffMembersContext.StaffMembers.FindAsync(id);
        return staffMembers;
    }

    [HttpGet("byname/{name}")]
    public async Task<ActionResult<StaffMembers?>> Get(string name)
    {
        StaffMembers? staffMembers = await _staffMembersContext.StaffMembers
            .FirstOrDefaultAsync(t => t.Name == name);

        if (staffMembers == null)
        {
            return NotFound($"Member with name '{name}' not found.");
        }
        return Ok(staffMembers);
    }


    [HttpPost]
    public async Task<ActionResult<StaffMembers>> Post([FromBody] StaffMembers newStaffMember)
    {
        _staffMembersContext.StaffMembers.Add(newStaffMember);
        await _staffMembersContext.SaveChangesAsync();
        return newStaffMember;
    }

    [HttpPut]
    public async Task<ActionResult<StaffMembers>> Put([FromBody] StaffMembers updateMember)
    {
        try
        {
            _staffMembersContext.Entry(updateMember).State = EntityState.Modified;
            await _staffMembersContext.SaveChangesAsync();
            return updateMember;
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<StaffMembers>> Delete(int id)
    {
        StaffMembers? staffToDelete = await _staffMembersContext.StaffMembers.FindAsync(id);

        if (staffToDelete != null)
        {
            _staffMembersContext.StaffMembers.Remove(staffToDelete);
            await _staffMembersContext.SaveChangesAsync();
            return staffToDelete;
        }
        else
        {
            return NotFound();
        }
    }
}