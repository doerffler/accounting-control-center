using System;

namespace DoePaAdmin.ViewModel.Services
{
    public class SampleService : ISampleService
    {

        public string GetCurrentDate()
        {

            return DateTime.Now.ToLongDateString();

        }

    }
}