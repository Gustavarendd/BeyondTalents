using BeyondTalents.Interfaces;
using BeyondTalents.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeyondTalents.Repositories
{
    public class CandidateRepository : RepositoryBase, ICandidateRepository
    {
        public void AddCandidate(CandidateModel candidate)
        {
            throw new NotImplementedException();
        }

        public void DeleteCandidate(CandidateModel candidate)
        {
            throw new NotImplementedException();
        }

        public List<CandidateModel> GetAllCandidates()
        {
            List<CandidateModel> candidates = new List<CandidateModel>();

            // Open the connection and execute the query
            using (var connection = GetConnection())
            {
                connection.Open();

                // Create a SqlCommand object to execute the query
                string query = @"
            SELECT c.CandidateID, c.FirstName, c.LastName, c.Address, c.PhoneNumber, c.Email, 
                   ci.CityName, ci.ZipCode, ci.Region, 
                   sc.SchoolName, e.EducationName, e.Degree
            FROM Candidate c
            LEFT JOIN City ci ON c.CityID = ci.CityID
            LEFT JOIN Education e ON c.CandidateID = e.CandidateID
            LEFT JOIN School sc ON e.SchoolID = sc.SchoolID";

                using (var command = new SqlCommand(query, connection))
                {
                    // Execute the query and read the results
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Check if this is a new candidate or an existing one
                            var candidateID = reader.GetInt32(reader.GetOrdinal("CandidateID"));
                            var candidate = candidates.FirstOrDefault(c => c.CandidateID == candidateID);

                            if (candidate == null)
                            {
                                candidate = new CandidateModel
                                {
                                    CandidateID = candidateID,
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                                    PhoneNumber = reader.IsDBNull(reader.GetOrdinal("PhoneNumber")) ? null : reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                                    City = new CityModel
                                    {
                                        Name = reader.IsDBNull(reader.GetOrdinal("CityName")) ? null : reader.GetString(reader.GetOrdinal("CityName")),
                                        ZipCode = reader.IsDBNull(reader.GetOrdinal("ZipCode")) ? 0 : reader.GetInt32(reader.GetOrdinal("ZipCode")),
                                        Region = reader.IsDBNull(reader.GetOrdinal("Region")) ? null : reader.GetString(reader.GetOrdinal("Region"))
                                    },
                                    Education = new List<EducationModel>()  // Initialize the list to store multiple education records
                                };

                                candidates.Add(candidate);
                            }

                            // Add the education record if it exists
                            if (!reader.IsDBNull(reader.GetOrdinal("SchoolName")) && !reader.IsDBNull(reader.GetOrdinal("Degree")))
                            {
                                candidate.Education.Add(new EducationModel
                                {
                                    SchoolName = reader.GetString(reader.GetOrdinal("SchoolName")),
                                    EducationName = reader.GetString(reader.GetOrdinal("EducationName")),
                                    Degree = reader.GetString(reader.GetOrdinal("Degree"))
                                });
                            }
                        }
                    }
                }
            }
            return candidates;
        }


        public CandidateModel GetCandidateById(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateCandidate(CandidateModel candidate)
        {
            throw new NotImplementedException();
        }


        public List<CandidateModel> GetCandidatesBySearch(SearchCriteria searchCriteria)
        {
            // Build the SQL query based on the search criteria
            StringBuilder queryBuilder = new StringBuilder(@"SELECT c.CandidateID, c.FirstName, c.LastName, c.Address, c.PhoneNumber, c.Email, 
                                                         ci.CityName, ci.ZipCode, ci.Region, sc.SchoolName e.EducationName, e.Degree, s.Name AS SkillName
                                                    FROM Candidate c
                                                    LEFT JOIN City ci ON c.CityID = ci.CityID
                                                    LEFT JOIN Education e ON c.CandidateID = e.CandidateID
                                                    LEFT JOIN School sc ON e.SchoolID = sc.SchoolID
                                                    LEFT JOIN CandidateSkill cs ON c.CandidateID = cs.CandidateID
                                                    LEFT JOIN Skill s ON cs.SkillID = s.SkillID
                                                    WHERE 1=1");

            if (!string.IsNullOrEmpty(searchCriteria.FirstName))
            {
                queryBuilder.Append(" AND c.FirstName LIKE @FirstName");
            }

            if (!string.IsNullOrEmpty(searchCriteria.LastName))
            {
                queryBuilder.Append(" AND c.LastName LIKE @LastName");
            }

            if (!string.IsNullOrEmpty(searchCriteria.City))
            {
                queryBuilder.Append(" AND ci.CityName LIKE @City");
            }

            if (!string.IsNullOrEmpty(searchCriteria.Skill))
            {
                queryBuilder.Append(" AND s.Name LIKE @Skill");
            }

            if (!string.IsNullOrEmpty(searchCriteria.SchoolName))
            {
                queryBuilder.Append(" AND e.SchoolName LIKE @SchoolName");
            }

            if (!string.IsNullOrEmpty(searchCriteria.Degree))
            {
                queryBuilder.Append(" AND e.Degree LIKE @Degree");
            }

            // Prepare the SQL query string
            string query = queryBuilder.ToString();

            // Create a list to hold the results
            List<CandidateModel> candidates = new List<CandidateModel>();

            // Open the connection and execute the query
            using (var connection = GetConnection())
            {
                connection.Open();

                // Create a SqlCommand object to execute the query
                using (var command = new SqlCommand(query, connection))
                {
                    // Add parameters for each condition to prevent SQL injection
                    if (!string.IsNullOrEmpty(searchCriteria.FirstName))
                        command.Parameters.AddWithValue("@FirstName", "%" + searchCriteria.FirstName + "%");

                    if (!string.IsNullOrEmpty(searchCriteria.LastName))
                        command.Parameters.AddWithValue("@LastName", "%" + searchCriteria.LastName + "%");

                    if (!string.IsNullOrEmpty(searchCriteria.City))
                        command.Parameters.AddWithValue("@City", "%" + searchCriteria.City + "%");

                    if (!string.IsNullOrEmpty(searchCriteria.Skill))
                        command.Parameters.AddWithValue("@Skill", "%" + searchCriteria.Skill + "%");

                    if (!string.IsNullOrEmpty(searchCriteria.SchoolName))
                        command.Parameters.AddWithValue("@SchoolName", "%" + searchCriteria.SchoolName + "%");

                    if (!string.IsNullOrEmpty(searchCriteria.Degree))
                        command.Parameters.AddWithValue("@Degree", "%" + searchCriteria.Degree + "%");

                    // Execute the query and read the results
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Map the data to a CandidateModel
                            var candidate = new CandidateModel
                            {
                                CandidateID = reader.GetInt32(reader.GetOrdinal("CandidateID")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                                PhoneNumber = reader.IsDBNull(reader.GetOrdinal("PhoneNumber")) ? null : reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),

                                // Fill City, Education, and Skills
                                City = new CityModel
                                {
                                    Name = reader.IsDBNull(reader.GetOrdinal("CityName")) ? null : reader.GetString(reader.GetOrdinal("CityName")),
                                    ZipCode = reader.IsDBNull(reader.GetOrdinal("ZipCode")) ? 0 : reader.GetInt32(reader.GetOrdinal("ZipCode")),
                                    Region = reader.IsDBNull(reader.GetOrdinal("Region")) ? null : reader.GetString(reader.GetOrdinal("Region"))
                                },
                                Education = new List<EducationModel>(),
                                Skills = new List<string>()
                            };

                            // Collect skills for the current candidate
                            if (!reader.IsDBNull(reader.GetOrdinal("SkillName")))
                            {
                                candidate.Skills.Add(reader.GetString(reader.GetOrdinal("SkillName")));
                            }
                            // Add the education record if it exists
                            if (!reader.IsDBNull(reader.GetOrdinal("SchoolName")) && !reader.IsDBNull(reader.GetOrdinal("Degree")))
                            {
                                candidate.Education.Add(new EducationModel
                                {
                                    SchoolName = reader.GetString(reader.GetOrdinal("SchoolName")),
                                    EducationName = reader.GetString(reader.GetOrdinal("EducationName")),
                                    Degree = reader.GetString(reader.GetOrdinal("Degree"))
                                });
                            }

                            candidates.Add(candidate);
                        }
                    }
                }
            }

            return candidates;



        }
    }
    
}
