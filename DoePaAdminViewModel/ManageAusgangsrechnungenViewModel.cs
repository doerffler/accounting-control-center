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
    public class ManageAusgangsrechnungenViewModel : DoePaAdminViewModelBase
    {

        private Geschaeftsjahr _selectedGeschaeftsjahr;
        public Geschaeftsjahr SelectedGeschaeftsjahr
        {
            get => _selectedGeschaeftsjahr;
            set => SetProperty(ref _selectedGeschaeftsjahr, value, true);
        }

        private ObservableCollection<Geschaeftsjahr> _geschaeftsjahre = new();
        public ObservableCollection<Geschaeftsjahr> Geschaeftsjahre
        {
            get => _geschaeftsjahre;
            set => SetProperty(ref _geschaeftsjahre, value, true);
        }
        
        public ManageAusgangsrechnungenViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {

            Geschaeftsjahre = Task.Run(async () => await DoePaAdminService.GetGeschaeftsjahreAsync()).Result;
            SelectedGeschaeftsjahr = CalculateCurrentGeschaeftsjahr();

        }

        private Geschaeftsjahr CalculateCurrentGeschaeftsjahr()
        {
            Geschaeftsjahr geschaeftsjahrToReturn = Geschaeftsjahre.Where(gj => gj.DatumVon <= DateTime.Now && gj.DatumBis >= DateTime.Now).FirstOrDefault();

            return geschaeftsjahrToReturn;
        }
    }
}
