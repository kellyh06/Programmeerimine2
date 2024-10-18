using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class ShowSchedule
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime date { get; set; }
    }
}
