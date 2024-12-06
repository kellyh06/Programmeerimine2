using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class ShowSchedulesIndexModel
    {
        public ShowScheduleSearch Search { get; set; }
        public PagedResult<ShowSchedule> Data { get; set; }
    }
}