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
        public DbSet<ProgramMusic> ProgramMusics { get; set; }
        public DbSet<MusicTrack> MusicTracks { get; set; }
        public DbSet<Artist> Artist { get; set; } = default!;
        public DbSet<KooliProjekt.Data.ShowSchedule> ShowSchedule { get; set; } = default!;

    }
}