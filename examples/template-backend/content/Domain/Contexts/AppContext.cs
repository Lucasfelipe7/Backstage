using Domain.Entities;
using Mappings;
using Microsoft.EntityFrameworkCore;

namespace Domain.Contexts
{
    public class AppContext : DbContext
    {
        public virtual DbSet<TbMensagem> Mensagens { get; set; }
        
        public AppContext(DbContextOptions<AppContext> options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Outra forma para o container iniciar o DbContext.
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MensagemMap());
        }
    }
}
