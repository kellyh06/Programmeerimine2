using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class ProgramMusic
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int SongId { get; set; }

        [Required]
        public int showscheduleId { get; set; }
    }
}
