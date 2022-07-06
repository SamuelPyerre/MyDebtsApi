using Microsoft.EntityFrameworkCore;
using MyDebtsApi.Data.Mappings;
using MyDebtsApi.Models;

namespace MyDebtsApi.Data
{
    public class MyDebtsDbContext : DbContext           
    {
        public DbSet<DividaModel> Dividas { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=localhost,1433;Database=MyDebts;User ID=sa;Password=1q2w3e4r@#$");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DividaMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());

        }
    }
}
