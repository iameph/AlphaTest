using System.ComponentModel.DataAnnotations;

namespace AlphaTest.Models
{
    public class CreateQueryModel
    {
        [Required]
        [Display(Name = "Категория")]
        public QueryCategory Category { get; set; }

        [Required]
        [Display(Name = "Текст заявки")]
        [StringLength(1000, ErrorMessage = "Минимальная длина : {2} символов.", MinimumLength = 10)]
        public string Text { get; set; }
    }
}