using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class DataCarrier
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Feature { get; set; }
    } 
}
