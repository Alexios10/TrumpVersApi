namespace TrumpVersApi.Interfaces
{
    // Definerer et grensesnitt for StaffMembers
    interface IStaffmembers
    {
        // Egenskap til å representere de ulike identifikatorene til ansatte
        int Id { get; set; }
        string? Name { get; set; }
        string? Image { get; set; }
        string? Description { get; set; }
        string? Title { get; set; }
        string? Email { get; set; }
    }
}