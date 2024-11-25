namespace TrumpVersApi.interfaces;


interface ITrumpThoughts
{
    int Id { get; set; }
    string? Name { get; set; }
    string? Thought { get; set; }
    string? Category { get; set; }
    DateTime DateCreated { get; set; }
}