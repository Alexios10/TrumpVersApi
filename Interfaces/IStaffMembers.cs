namespace TrumpVersApi.interfaces;


interface IStaffmembers
{
    int Id { get; set; }
    string? Name { get; set; }
    string? Image { get; set; }
    string? Description { get; set; }
    string? Title { get; set; }
    string? Email { get; set; }
}