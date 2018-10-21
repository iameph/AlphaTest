using System.ComponentModel.DataAnnotations;

namespace AlphaTest.Models
{
    public enum QueryCategory
    {
        [Display(Name = "Ошибка")]
        Error,
        [Display(Name = "Проблема")]
        Problem,
        [Display(Name = "Задача")]
        Task,
        [Display(Name = "Предложение")]
        Offer,
        [Display(Name = "Жалоба")]
        Complaint,
        [Display(Name = "Другое")]
        Other
    }
}