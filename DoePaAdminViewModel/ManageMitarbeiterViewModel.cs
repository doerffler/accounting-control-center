using DoePaAdminDataModel.Stammdaten;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel
{
    public class ManageMitarbeiterViewModel : ObservableRecipient
    {

        private ObservableCollection<Mitarbeiter> _mitarbeiter = new();

        public ObservableCollection<Mitarbeiter> Mitarbeiter
        {
            get => _mitarbeiter;
            set => SetProperty(ref _mitarbeiter, value, true);
        }

        private Mitarbeiter _selectedMitarbeiter;

        public Mitarbeiter SelectedMitarbeiter
        {
            get => _selectedMitarbeiter;
            set => SetProperty(ref _selectedMitarbeiter, value);
        }

    }
}
