using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;

namespace KooliProjekt.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<DataCarrier> DataCarriers{ get; set; }
        public DbSet<DataCarrierMusic> DataCarrierMusics { get; set; }

        public DbSet<ProgramMusic> ProgramMusics { get; set; }
        public DbSet<MusicTrack> MusicTracks { get; set; }
        public DbSet<Artist> Artists { get; set; } = default!;
        public DbSet<ShowSchedule> ShowSchedule { get; set; } = default!;

    }
}