using System.Collections.Generic;

namespace AlphaTest.Models
{
    public class UserQueriesModel
    {
        public CreateQueryModel NewQuery { get; set; }

        public IEnumerable<Query> Queries { get; set; }

        public UserQueriesModel()
        {
            NewQuery = new CreateQueryModel();
            Queries = new List<Query>();
        }
    }
}