using System;
using System.ComponentModel.DataAnnotations;

namespace AlphaTest.Models
{
    public class Query
    {
        public int Id { get; set; }

        [Display(Name = "Текст заявки")]
        public string Text { get; set; }

        [Display(Name = "Статус")]
        public QueryState State { get; set; }

        [Display(Name = "Категория")]
        public QueryCategory Category { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime QueryDate { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}