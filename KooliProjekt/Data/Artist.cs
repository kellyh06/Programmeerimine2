using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace KooliProjekt.Data
{
    public class Artist : Entity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
