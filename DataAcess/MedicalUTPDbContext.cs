using MedicalUTP.Models;
using MedicalUTP.Utilidades;
using Microsoft.EntityFrameworkCore;


namespace MedicalUTP.DataAcess
{
    public class MedicalUTPDbContext : DbContext
    {
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conexionDb = $"Filename={ConexionDB.DevolverRuta("clinica.db")}";
            optionsBuilder.UseSqlite(conexionDb);
        }
    }
}
