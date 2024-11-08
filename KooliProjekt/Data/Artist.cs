using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace KooliProjekt.Data
{
    public class Artist
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
