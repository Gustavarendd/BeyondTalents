using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeyondTalents.Models
{
    public class SearchCriteria
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Skill { get; set; }
        public string SchoolName { get; set; }
        public string Degree { get; set; }

        // Add other relevant search fields
    }
}
