using Microsoft.EntityFrameworkCore;
using TrumpVersApi.Models;

namespace TrumpVersApi.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<TrumpThoughts> TrumpThoughts { get; set; }
    public DbSet<TrumpMerch> TrumpMerch { get; set; }

}
