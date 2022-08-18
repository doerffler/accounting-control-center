using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel.Services
{
    public interface IApiReciever<T>
    {
        public string Endpoint { get; set; }
        public T Response { get; set; }

        public Task<T> ReadData();
    }
}