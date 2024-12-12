using TrumpVersApi.Interfaces;

namespace TrumpVersApi.Models;

public class StaffMembers : IStaffMembers
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Image { get; set; }
    public string? Description { get; set; }
    public string? Title { get; set; }
    public string? Email { get; set; }
}