using Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DAO
{
    
    public class ChamadosDbContext : DbContext
    {

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Chamado> Chamados { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public ChamadosDbContext(DbContextOptions<ChamadosDbContext> ctx) : base(ctx)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

    }
}