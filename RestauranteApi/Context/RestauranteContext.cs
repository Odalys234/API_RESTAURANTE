using System;
using Microsoft.EntityFrameworkCore;
using RestauranteApi.Models;

namespace RestauranteApi.Context
{
    public class RestauranteContext : DbContext
    {
        public RestauranteContext(DbContextOptions<RestauranteContext> options) : base(options)
        {
        }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Platillo> Platillos { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Puesto> Puestos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }  
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
