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
        optionsBuilder.UseSqlServer(@"Server=tcp:advincula-server.database.windows.net,1433;Initial Catalog=advinculaDB;Persist Security Info=False;User ID=Bewjodvmb1;Password=Bewjodvmb*1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>().Navigation(e => e.Pedidos).AutoInclude();
        modelBuilder.Entity<Pedido>().Navigation(p => p.PedidoDetalles).AutoInclude();
        modelBuilder.Entity<PedidoDetalle>().Navigation(pd => pd.Producto).AutoInclude();
    }

}
}