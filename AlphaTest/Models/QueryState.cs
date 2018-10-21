using System.ComponentModel.DataAnnotations;

namespace AlphaTest.Models
{
    public enum QueryState
    {
        [Display(Name = "Новая")]
        New,
        [Display(Name = "В работе")]
        InProcess,
        [Display(Name = "Отклонена")]
        Refused,
        [Display(Name = "Завершена")]
        Finished
    }
}