using BeyondTalents.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BeyondTalents.ViewModels
{
    public class AddCandidateViewModel : ViewModelBase
    {

        private CandidateModel _candidate;

        public CandidateModel Candidate
        {
            get { return _candidate; }
            set
            {
                _candidate = value;
                OnPropertyChanged(nameof(Candidate));
            }
        }

        // Commands
        public ICommand SaveCommand { get; private set; }

        public AddCandidateViewModel()
        {
            // Initialize the Candidate object
            _candidate = new CandidateModel();

            // Initialize the SaveCommand
            SaveCommand = new RelayCommand(SaveCandidate, canSaveCandidate);
        }

        private void SaveCandidate(object obj)
        {
            // Logic to save candidate to the database goes here
            // This could be done using an ORM like Entity Framework or a direct SQL command
        }

        private bool canSaveCandidate(object obj)
        {
            bool canSave = false;
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(_candidate))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(_candidate);
                if (value == null)
                {
                    canSave = false;
                    break;
                }
                else
                {
                    canSave = true;
                }
            }

            return canSave;

        }


        }
    }
