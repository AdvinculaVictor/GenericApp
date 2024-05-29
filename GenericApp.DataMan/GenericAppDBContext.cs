using GenericApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GenericApp.DataMan{
public class GenericAppDBContext:DbContext
{
    DbContextOptions<GenericAppDBContext>? options;
    public GenericAppDBContext(){
        // this.Database.EnsureCreated();
    }

    public GenericAppDBContext(DbContextOptions<GenericAppDBContext> _options){
        this.options = _options;
    }
    public DbSet<Cliente>? Clientes { get; set; }
    public DbSet<Pedido>? Pedidos { get; set; }
    public DbSet<PedidoDetalle>? PedidoDetalles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=66.175.233.172;Initial Catalog=genericApp;User=sa;password=<YourStrong@Passw0rd>;");
    }

}
}