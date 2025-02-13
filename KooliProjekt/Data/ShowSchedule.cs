using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class ShowSchedule : Entity
    {

        [Required]
        public DateTime date { get; set; }
        public string Date { get; set; }
    }
}
