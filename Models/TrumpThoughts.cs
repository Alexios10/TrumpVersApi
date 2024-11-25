using TrumpVersApi.interfaces;

namespace TrumpVersApi.Models;

public class TrumpThoughts : ITrumpThoughts
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Thought { get; set; }
    public string? Category { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}