using System;

namespace DoePaAdminApp.Services
{
    class SampleService : ISampleService
    {

        public string GetCurrentDate()
        {

            return DateTime.Now.ToLongDateString();

        }
        
    }
}
