using BeyondTalents.Models;
using BeyondTalents.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeyondTalents.ViewModels
{
    public class SearchCandidateViewModel: ViewModelBase
    {
        private CandidateRepository _candidateRepository;
        private ObservableCollection<CandidateModel> _candidates;
        private ObservableCollection<CandidateModel> _allCandidates; // To store the original unfiltered list
        private CandidateModel _selectedCandidate;

        // Search Criteria Properties
        private string _firstName;
        private string _lastName;
        private string _city;
        private string _skill;
        private string _schoolName;
        private string _degree;

        // Exposed properties for data binding in the UI (e.g., Textboxes for search fields)
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
                SearchCandidates();  // Trigger search when value changes
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
                SearchCandidates();  // Trigger search when value changes
            }
        }

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
                SearchCandidates();  // Trigger search when value changes
            }
        }

        public string Skill
        {
            get => _skill;
            set
            {
                _skill = value;
                OnPropertyChanged(nameof(Skill));
                SearchCandidates();  // Trigger search when value changes
            }
        }

        public string SchoolName
        {
            get => _schoolName;
            set
            {
                _schoolName = value;
                OnPropertyChanged(nameof(SchoolName));
                SearchCandidates();  // Trigger search when value changes
            }
        }

        public string Degree
        {
            get => _degree;
            set
            {
                _degree = value;
                OnPropertyChanged(nameof(Degree));
                SearchCandidates();  // Trigger search when value changes
            }
        }

        public ObservableCollection<CandidateModel> Candidates
        {
            get => _candidates;
            set
            {
                _candidates = value;
                OnPropertyChanged(nameof(Candidates));
            }
        }

        public CandidateModel SelectedCandidate
        {
            get => _selectedCandidate;
            set
            {
                _selectedCandidate = value;
                OnPropertyChanged(nameof(SelectedCandidate));
            }
        }

        public SearchCandidateViewModel()
        {
            _candidateRepository = new CandidateRepository();
            _allCandidates = new ObservableCollection<CandidateModel>(_candidateRepository.GetAllCandidates());
            Candidates = new ObservableCollection<CandidateModel>(_allCandidates);
        }

        private void SearchCandidates()
        {
            var filteredCandidates = _allCandidates.AsEnumerable();

            // Apply filters based on the search criteria
            if (!string.IsNullOrEmpty(FirstName))
            {
                filteredCandidates = filteredCandidates.Where(c => c.FirstName.Contains(FirstName, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(LastName))
            {
                filteredCandidates = filteredCandidates.Where(c => c.LastName.Contains(LastName, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(City))
            {
                filteredCandidates = filteredCandidates.Where(c => c.City.Name.Contains(City, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(Skill))
            {
                filteredCandidates = filteredCandidates.Where(c => c.Skills.Any(s => s.Contains(Skill, StringComparison.OrdinalIgnoreCase)));
            }
            if (!string.IsNullOrEmpty(SchoolName))
            {
                filteredCandidates = filteredCandidates.Where(c =>
                    c.Education?.Any(e => e.SchoolName.Contains(SchoolName, StringComparison.OrdinalIgnoreCase)) ?? false
                    );
            }
            if (!string.IsNullOrEmpty(Degree))
            {
                filteredCandidates = filteredCandidates.Where(c =>
                     c.Education?.Any(e => e.Degree.Contains(Degree, StringComparison.OrdinalIgnoreCase)) ?? false
                    );
            }

            // Update the Candidates collection with the filtered results
            Candidates = new ObservableCollection<CandidateModel>(filteredCandidates.ToList());
        }
    }
}
