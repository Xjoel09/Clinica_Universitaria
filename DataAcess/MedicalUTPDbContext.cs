using MedicalUTP.Models;
using MedicalUTP.Utilidades;
using Microsoft.EntityFrameworkCore;


namespace MedicalUTP.DataAcess
{
    public class MedicalUTPDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Cita> Citas { get; set; }

        public DbSet<Medicamento> Medicamentos { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conexionDb = $"Filename={ConexionDB.DevolverRuta("clinica.db")}";
            optionsBuilder.UseSqlite(conexionDb);
        }

        // Método para aplicar migraciones automáticamente
        public async Task EnsureDatabaseMigratedAsync()
        {
            await this.Database.MigrateAsync();
        }
    }
}
