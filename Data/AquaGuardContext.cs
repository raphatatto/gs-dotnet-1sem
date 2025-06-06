using Microsoft.EntityFrameworkCore;
using api_aquaguard_dotnet.Models;


namespace api_aquaguard_dotnet.Data
{
    public class AquaGuardContext : DbContext
    {
        public AquaGuardContext(DbContextOptions<AquaGuardContext> options)
            : base(options)
        {
        }

        public DbSet<Alerta> Alertas { get; set; }
        public DbSet<Regiao> Regioes { get; set; }
        public DbSet<Sensor> Sensores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Regiao>()
                .HasOne(r => r.Sensor)
                .WithMany(s => s.Regioes)
                .HasForeignKey(r => r.IdSensor);

            modelBuilder.Entity<Alerta>()
                .HasOne(a => a.Regiao)
                .WithMany(r => r.Alertas)
                .HasForeignKey(a => a.IdRegiao);
        }
    }
}
