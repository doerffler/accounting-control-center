using ACC.ViewModel.Services;
using ACCDataModel.APIFeiertage;
using ACCDataModel.Stammdaten;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACC.ViewModel
{
    public class ManageGeschaeftsjahreViewModel : ACCViewModelBase
    {
        #region Geschäftsjahr
        private ObservableCollection<Geschaeftsjahr> _geschaeftsjahre = new();
        public ObservableCollection<Geschaeftsjahr> Geschaeftsjahre
        {
            get => _geschaeftsjahre;
            set => SetProperty(ref _geschaeftsjahre, value, true);
        }

        private Geschaeftsjahr _selectedGeschaeftsjahr;
        public Geschaeftsjahr SelectedGeschaeftsjahr
        {
            get => _selectedGeschaeftsjahr;
            set => SetProperty(ref _selectedGeschaeftsjahr, value);
        }
        #endregion

        #region Feiertag
        private ObservableCollection<Feiertag> _feiertage = new();
        public ObservableCollection<Feiertag> Feiertage
        {
            get => _feiertage;
            set => SetProperty(ref _feiertage, value, true);
        }

        private Feiertag _selectedFeiertag = new();
        public Feiertag SelectedFeiertag
        {
            get => _selectedFeiertag;
            set => SetProperty(ref _selectedFeiertag, value, true);
        }
        #endregion

        public IRelayCommand AddGeschaeftsjahrCommand { get; }
        public IRelayCommand RemoveGeschaeftsjahrCommand { get; }
        public IRelayCommand ImportDataCommand { get; }
        public IRelayCommand AddFeiertagCommand { get; }
        public IRelayCommand RemoveFeiertagCommand { get; }

        public ManageGeschaeftsjahreViewModel(IACCService accService, IUserInteractionService userInteractionService) : base(accService, userInteractionService)
        {
            Feiertage = new(Task.Run(async () => await ACCService.GetFeiertageAsync()).Result); 
            Geschaeftsjahre = new(Task.Run(async () => await ACCService.GetGeschaeftsjahreAsync()).Result);

            ImportDataCommand = new AsyncRelayCommand(ImportDataAsync);

            AddFeiertagCommand = new AsyncRelayCommand(DoAddFeiertagAsync);
            AddGeschaeftsjahrCommand = new AsyncRelayCommand(DoAddGeschaeftsjahrAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveGeschaeftsjahrCommand = new RelayCommand(DoRemoveGeschaeftsjahr);
            RemoveFeiertagCommand = new RelayCommand(DoRemoveFeiertag);
        
            PropertyChanged += HandlePropertyChanged;
        }

        private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SelectedGeschaeftsjahr):
                    this.Feiertage = new ObservableCollection<Feiertag>(SelectedGeschaeftsjahr.Feiertage);

                    break;
            }
        }

        private async Task DoAddGeschaeftsjahrAsync(CancellationToken cancellationToken = default)
        {
            DateTime Zeit = DateTime.Now;

            Geschaeftsjahr geschaeftsjahr = await ACCService.CreateGeschaeftsjahrAsync(cancellationToken);
            geschaeftsjahr.Name = Zeit.Year.ToString();
            geschaeftsjahr.DatumVon = Zeit;
            geschaeftsjahr.DatumBis = Zeit;
            geschaeftsjahr.Rechnungsprefix = Zeit.Year.ToString();
            Geschaeftsjahre.Add(geschaeftsjahr);
        }

        private void DoRemoveGeschaeftsjahr()
        {
            if (SelectedGeschaeftsjahr != null)
            {
                ACCService.RemoveGeschaeftsjahr(SelectedGeschaeftsjahr);
                _ = Geschaeftsjahre.Remove(SelectedGeschaeftsjahr);
            }
        }

        private void DoRemoveFeiertag()
        {
            if (SelectedFeiertag != null)
            {
                ACCService.RemoveFeiertag(SelectedFeiertag);
                _ = Feiertage.Remove(SelectedFeiertag);
            }
        }

        private async Task DoAddFeiertagAsync(CancellationToken cancellationToken = default)
        {
            Feiertag feiertag = await ACCService.CreateFeiertagAsync(cancellationToken);
            feiertag.Datum = DateTime.Now;
            feiertag.FeiertagName = "Neuer Feiertag";
            feiertag.Geschaeftsjahr = SelectedGeschaeftsjahr;
            Feiertage.Add(feiertag);
        }

        private async Task ImportDataAsync()
        {
            List<int> Jahre = new();
            for (int jahr = SelectedGeschaeftsjahr.DatumVon.Year; jahr <= SelectedGeschaeftsjahr.DatumBis.Year; jahr++)
                Jahre.Add(jahr);

            string endpoint = string.Format("https://get.api-feiertage.de?years={0}", string.Join(",", Jahre));

            ApiFeiertage apiFeiertage = await ApiReceiver.ReadData<ApiFeiertage>(endpoint);

            if (apiFeiertage.Status == "success")
            {
                foreach (ApiFeiertag apiFeiertag in apiFeiertage.FeiertagListe)
                {
                    if (apiFeiertag.Date >= SelectedGeschaeftsjahr.DatumVon && apiFeiertag.Date < SelectedGeschaeftsjahr.DatumBis)
                    {
                        Feiertag feiertag = await ACCService.CreateFeiertagAsync();
                        feiertag.Geschaeftsjahr = SelectedGeschaeftsjahr;
                        feiertag.Datum = apiFeiertag.Date;
                        feiertag.FeiertagName = string.Format("{0} {1}", apiFeiertag.Name, apiFeiertag.Date.Year.ToString());
                        feiertag.Niedersachsen = apiFeiertag.Niedersachsen == "1";
                        feiertag.Hamburg = apiFeiertag.Hamburg == "1";
                        feiertag.Sachsen = apiFeiertag.Sachsen == "1";
                        feiertag.Saarland = apiFeiertag.Saarland == "1";
                        feiertag.SachsenAnhalt = apiFeiertag.SachsenAnhalt == "1";
                        feiertag.Bremen = apiFeiertag.Bremen == "1";
                        feiertag.SchleswigHolstein = apiFeiertag.SchleswigHolstein == "1";
                        feiertag.MecklenburgVorpommern = apiFeiertag.MecklenburgVorpommern == "1";
                        feiertag.Berlin = apiFeiertag.Berlin == "1";
                        feiertag.Brandenburg = apiFeiertag.Brandenburg == "1";
                        feiertag.RheinlandPfalz = apiFeiertag.RheinlandPfalz == "1";
                        feiertag.BadenWuerttemberg = apiFeiertag.BadenWuerttemberg == "1";
                        feiertag.Bayern = apiFeiertag.Bayern == "1";
                        feiertag.Hessen = apiFeiertag.Hessen == "1";
                        feiertag.NordrheinWestfalen = apiFeiertag.NordrheinWestfalen == "1";
                        feiertag.Thueringen = apiFeiertag.Thueringen == "1";
                        feiertag.IstGanztag = true;

                        Feiertage.Add(feiertag);
                    }
                }
            }
        }
    }
}