﻿using DoePaAdminDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DoePaAdminApp.Controls
{
    /// <summary>
    /// Interaktionslogik für AdressenControl.xaml
    /// </summary>
    public partial class AdressenControl : UserControl
    {
        public AdressenControl()
        {
            InitializeComponent();
        }

        public Adresse SelectedItem
        {
            get { return (Adresse)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(Adresse), typeof(AdressenControl), new FrameworkPropertyMetadata(new Adresse())
        {
            BindsTwoWayByDefault = true,
            DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
        });
    }
}
