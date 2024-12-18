using TrumpVersApi.interfaces;

namespace TrumpVersApi.Models;

public class TrumpMerch : ITrumpMerch
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Image { get; set; }
    public string? Description { get; set; }
    public int? Price { get; set; }
    public int? Quantity { get; set; }
    public string? Category { get; set; }
}