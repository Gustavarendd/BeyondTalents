using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeyondTalents.Models
{
    public class CandidateModel
    {
        public int CandidateID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public CityModel City { get; set; }  // City name
        public List<EducationModel> Education { get; set; }  // Education details
        public List<string> Skills { get; set; }  // List of skills
    }

    public class EducationModel
    {
        public string SchoolName { get; set; }
        public string EducationName { get; set; }
        public string Degree { get; set; }
    }

    public class CityModel
    {
        public int CityID { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public int ZipCode { get; set; }
    }

}
