using System.Collections.Generic;

namespace Domain.Modules.TestUser
{
    public class Response
    {
        public List<RandomedUser> Results { get; set; }
        public Info Info { get; set; }
    }
}