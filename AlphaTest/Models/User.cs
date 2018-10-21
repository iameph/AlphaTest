﻿using System.Collections.Generic;

namespace AlphaTest.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public bool IsAdmin { get; set; }

        public virtual IEnumerable<Query> Queries { get; set; }
    }
}