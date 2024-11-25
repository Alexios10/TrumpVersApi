using Microsoft.EntityFrameworkCore;
using TrumpVersApi.Models;

namespace TrumpVersApi.Contexts;

public class TrumpThoughtsContext : DbContext
{
    public TrumpThoughtsContext(DbContextOptions<TrumpThoughtsContext> options) : base(options) { }
    public DbSet<TrumpThoughts> TrumpThoughts { get; set; }
}
