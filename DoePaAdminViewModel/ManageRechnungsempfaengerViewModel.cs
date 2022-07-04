using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel
{
    public class ManageKreditorViewModel : DoePaAdminViewModelBase
    {

        private ObservableCollection<Kreditor> _kreditoren;

        public ObservableCollection<Kreditor> Kreditoren
        {
            get => _kreditoren;
            set => SetProperty(ref _kreditoren, value, true);
        }

        private Kreditor _selectedKreditor;

        public Kreditor SelectedKreditor
        {
            get => _selectedKreditor;
            set => SetProperty(ref _selectedKreditor, value, true);
        }

        public ManageKreditorViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {

        }

    }
}
