using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class MusicTrack
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string Artist { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Pace { get; set; }
    }
}
