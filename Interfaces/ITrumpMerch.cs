namespace TrumpVersApi.interfaces;


interface ITrumpMerch
{
    int Id { get; set; }
    string? Name { get; set; }
    string? Image { get; set; }
    string? Description { get; set; }
    int? Price { get; set; }
    int? Quantity { get; set; }
}