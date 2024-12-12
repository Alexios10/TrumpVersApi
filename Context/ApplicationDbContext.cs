using Microsoft.EntityFrameworkCore;
using TrumpVersApi.Models;

namespace TrumpVersApi.Contexts
{
    // Definerer ApplicationDbContext som en klasse som arver fra DbContext
    public class ApplicationDbContext : DbContext
    {
        // Konstruktøren tar inn DbContextOptions som parameter og sender det videre til basisklassen DbContext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Definerer DbSet
        public DbSet<TrumpThoughts> TrumpThoughts { get; set; }
        public DbSet<TrumpMerch> TrumpMerch { get; set; }
        public DbSet<StaffMembers> StaffMembers { get; set; }
    }
}