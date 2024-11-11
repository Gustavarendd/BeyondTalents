using BeyondTalents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeyondTalents.Interfaces
{
    internal interface ICandidateRepository
    {
        void AddCandidate(CandidateModel candidate);
        void UpdateCandidate(CandidateModel candidate);
        void DeleteCandidate(CandidateModel candidate);
        CandidateModel GetCandidateById(string id);
        List<CandidateModel> GetCandidatesBySearch(SearchCriteria searchCriteria);
        List<CandidateModel> GetAllCandidates();
    }
}
