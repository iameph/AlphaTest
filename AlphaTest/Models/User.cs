﻿namespace AlphaTest.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public bool IsAdmin { get; set; }

        public virtual Query Queries { get; set; }
    }
}