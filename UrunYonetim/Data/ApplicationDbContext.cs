using Microsoft.EntityFrameworkCore;
using UrunYonetim.Models;

namespace UrunYonetim.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Urun> Urunler { get; set; }
    }
}