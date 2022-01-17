using Microsoft.EntityFrameworkCore;

namespace SalutAPI.Domain;

public class SalutAppRepo : DbContext {
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseMySQL("Server=localhost; Port=3307; Database=SalutApp; Uid=admin; Pwd=admin1234;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PlayerSetupStep>().Ignore(x => x.ComponentType);
    }

    public DbSet<GameSystem> GameSystem { get; set; }
    public DbSet<PlayerConfig> PlayerConfig { get; set; }
}
