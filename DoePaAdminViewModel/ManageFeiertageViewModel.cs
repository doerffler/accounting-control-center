using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.APIFeiertage;
using DoePaAdminDataModel.Stammdaten;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel
{
    public class ManageFeiertageViewModel : DoePaAdminViewModelBase
    {
        public IRelayCommand ImportDataCommand { get; }

        #region Feiertag
        private ObservableCollection<Datum> _datuemer = new();
        public ObservableCollection<Datum> Datuemer
        {
            get => _datuemer;
            set => SetProperty(ref _datuemer, value, true);
        }
        #endregion

        #region Geschäftsjahr
        private ObservableCollection<Geschaeftsjahr> _geschaeftsjahre = new();
        public ObservableCollection<Geschaeftsjahr> Geschaeftsjahre
        {
            get => _geschaeftsjahre;
            set => SetProperty(ref _geschaeftsjahre, value, true);
        }

        private Geschaeftsjahr _selectedGeschaeftsjahr = new();
        public Geschaeftsjahr SelectedGeschaeftsjahr
        {
            get => _selectedGeschaeftsjahr;
            set => SetProperty(ref _selectedGeschaeftsjahr, value, true);
        }
        #endregion

        public ManageFeiertageViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {
            Datuemer = new(Task.Run(async () => await DoePaAdminService.GetDatuemerAsync()).Result);
            Geschaeftsjahre = new(Task.Run(async () => await DoePaAdminService.GetGeschaeftsjahreAsync()).Result);

            ImportDataCommand = new AsyncRelayCommand(ImportDataAsync);
        }

        private async Task ImportDataAsync()
        {
            string endpoint = string.Format("https://get.api-feiertage.de?years={0}", SelectedGeschaeftsjahr.Rechnungsprefix);
            ApiReciever<Feiertage> apiReciever = new(endpoint);
            Feiertage Response = await apiReciever.ReadData();

            if (Response.Status == "success")
            {
                foreach (Feiertag feiertag in Response.FeiertagListe)
                {
                    _ = new Datum()
                    {
                        Geschaeftsjahr = SelectedGeschaeftsjahr,
                        DatumTag = feiertag.Date,
                        FeiertagName = feiertag.Name,
                        Niedersachsen = feiertag.Niedersachsen == "1",
                        Hamburg = feiertag.Hamburg == "1",
                        Sachsen = feiertag.Sachsen == "1",
                        Saarland = feiertag.Saarland == "1",
                        SachsenAnhalt = feiertag.SachsenAnhalt == "1",
                        Bremen = feiertag.Bremen == "1",
                        SchleswigHolstein = feiertag.SchleswigHolstein == "1",
                        MecklenburgVorpommern = feiertag.MecklenburgVorpommern == "1",
                        Berlin = feiertag.Berlin == "1",
                        Brandenburg = feiertag.Brandenburg == "1",
                        RheinlandPfalz = feiertag.RheinlandPfalz == "1",
                        BadenWuerttemberg = feiertag.BadenWuerttemberg == "1",
                        Bayern = feiertag.Bayern == "1",
                        Hessen = feiertag.Hessen == "1",
                        NordrheinWestfalen = feiertag.NordrheinWestfalen == "1",
                        Thueringen = feiertag.Thueringen == "1",
                        IstGanztag = true
                    };

                }
            }
        }
    }
}
