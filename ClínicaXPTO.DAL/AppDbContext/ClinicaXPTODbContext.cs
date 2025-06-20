using ClinicaXPTO.Models;
using Microsoft.EntityFrameworkCore;


namespace ClinicaXPTO.DAL.AppDbContext
{
    public class ClinicaXPTODbContext : DbContext
    {
        public ClinicaXPTODbContext() { }

        public ClinicaXPTODbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions) { }

        public DbSet<ItemPedido> ItemPedidos { get; set; }
        public DbSet<PedidoMarcacao> PedidoMarcacoes { get; set; }
        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<Utente> Utentes { get; set; }
        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<SubsistemaSaude> SubsistemasSaude { get; set; }
        public DbSet<TipoActoClinico> TipoActoClinicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoActoClinico>()
                .Property(t => t.Preco)
                .HasPrecision(10, 2);
        }
    }
}
