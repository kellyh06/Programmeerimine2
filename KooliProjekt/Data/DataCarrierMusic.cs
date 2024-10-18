using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class DataCarrierMusic
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int datacarrierId { get; set; }

        [Required]
        public int SongId { get; set; }
    }
}
