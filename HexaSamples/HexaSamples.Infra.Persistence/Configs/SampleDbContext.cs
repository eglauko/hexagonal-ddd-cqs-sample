using Microsoft.EntityFrameworkCore;

namespace HexaSamples.Infra.Persistence.Configs;

public class SampleDbContext : DbContext
{
    public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SampleDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}