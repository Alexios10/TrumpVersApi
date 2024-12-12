namespace TrumpVersApi.interfaces;

 // Definerer et grensesnitt for StaffMembers
interface ITrumpThoughts
{
    // Egenskap til å representere de ulike identifikatorene til thoughts
    int Id { get; set; }
    string? Name { get; set; }
    string? Thought { get; set; }
    string? Category { get; set; }
    DateTime DateCreated { get; set; }
}