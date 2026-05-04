using Microsoft.EntityFrameworkCore;
using LocadoraCarros.Models;

namespace LocadoraCarros.Data
{
    
    /// Contexto do banco de dados Oracle usando Entity Framework Core.
    
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        
        /// Tabela de Carros no banco Oracle.
        
        public DbSet<Carro> Carros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração explícita da entidade Carro
            modelBuilder.Entity<Carro>(entity =>
            {
                entity.ToTable("TB_CARROS");
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Id)
                      .HasColumnName("ID_CARRO")
                      .ValueGeneratedOnAdd();

                entity.Property(c => c.Modelo)
                      .HasColumnName("MODELO")
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(c => c.Marca)
                      .HasColumnName("MARCA")
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(c => c.Ano)
                      .HasColumnName("ANO")
                      .IsRequired();

                entity.Property(c => c.ValorDiaria)
                      .HasColumnName("VALOR_DIARIA")
                      .IsRequired();
            });
        }
    }
}
