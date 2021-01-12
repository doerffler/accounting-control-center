using DoePaAdminApp.Models;
using DoePaAdminApp.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        private string input;
        public string Input
        {
            get => input;
            set => Set(ref input, value);
        }

        private readonly ISampleService sampleService;
        private readonly AppSettings settings;

        public RelayCommand ExecuteCommand { get; }

        public MainViewModel(ISampleService sampleService, IOptions<AppSettings> options)
        {
            this.sampleService = sampleService;
            this.settings = options.Value;

            ExecuteCommand = new RelayCommand(async () => await ExecuteAsync());
        }
        
        private Task ExecuteAsync()
        {
            Debug.WriteLine($"Current value: {input}");
            return Task.CompletedTask;
        }
    }
}
