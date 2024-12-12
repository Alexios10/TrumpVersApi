namespace TrumpVersApi.interfaces;

 // Definerer et grensesnitt for StaffMembers
interface ITrumpMerch
{
    // Egenskap til å representere de ulike identifikatorene til merch
    int Id { get; set; }
    string? Name { get; set; }
    string? Image { get; set; }
    string? Description { get; set; }
    int? Price { get; set; }
    int? Quantity { get; set; }
    string? Category { get; set; }
}