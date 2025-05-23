using Alertas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Alertas.Infra
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options)
            : base(options)
        {
        }
        //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=localhost;Database=AlertasDB;TrustServerCertificate=True;");
        //}

        public DbSet<Alerta> Alertas { get; set; }
    }
}