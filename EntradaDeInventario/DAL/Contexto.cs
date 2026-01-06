using EntradaDeInventario.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EntradaDeInventario.DAL
{
    public class Contexto : IdentityDbContext<IdentityUser>
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Productos> Productos { get; set; }
        public DbSet<Entradas> Entradas { get; set; }
        public DbSet<EntradasDetalle> EntradasDetalle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Productos>().HasData(
                new List<Productos>()
                {
                new()
                {
                    ProductoId = 1,
                    Descripcion = "Laptop HP Pavilion 15",
                    Costo = 450.00m,
                    Precio = 650.00m,
                    Existencia = 0
                },
                new()
                {
                    ProductoId = 2,
                    Descripcion = "Mouse Inalámbrico Logitech",
                    Costo = 15.00m,
                    Precio = 25.00m,
                    Existencia = 0
                },
                new()
                {
                    ProductoId = 3,
                    Descripcion = "Teclado Mecánico RGB",
                    Costo = 35.00m,
                    Precio = 55.00m,
                    Existencia = 0
                },
                new()
                {
                    ProductoId = 4,
                    Descripcion = "Monitor Samsung 24 pulgadas",
                    Costo = 120.00m,
                    Precio = 180.00m,
                    Existencia = 0
                },
                new()
                {
                    ProductoId = 5,
                    Descripcion = "Audífonos Sony WH-1000XM4",
                    Costo = 200.00m,
                    Precio = 300.00m,
                    Existencia = 0
                }
                }
            );
        }
    }
}
