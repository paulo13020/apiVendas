using APIGestaoVendas.Model;
using Microsoft.EntityFrameworkCore;

namespace APIGestaoVendas.Data
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options) { }
        public DbSet<Vendedor> Vendedores{ get; set; }
        public DbSet<Oportunidade> Oportunidades { get; set; }
        public DbSet<VendedorOportunidade> VendedorOportunidades { get; set; }
    }
}
