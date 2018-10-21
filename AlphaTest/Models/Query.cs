using System;

namespace AlphaTest.Models
{
    public class Query
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public QueryState State { get; set; }

        public QueryCategory Category { get; set; }

        public DateTime QueryDate { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}