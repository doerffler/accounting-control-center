using DoePaAdminDataModel.Stammdaten;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel
{
    public class UebergeordneteKostenstellenViewModel : ObservableRecipient
    {

        private Kostenstelle _kostenstelle;
        public Kostenstelle Kostenstelle
        {
            get => _kostenstelle;
            set => SetProperty(ref _kostenstelle, value);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public UebergeordneteKostenstellenViewModel(Kostenstelle kst)
        {
            this.Kostenstelle = kst;
            this.IsSelected = false;
        }

    }
}
