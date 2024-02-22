using ACCDataModel.DTO;

namespace ACCWebUI.Services
{
    public class PaginationService<T>
    {
        private int currentPage = 1;
        private int pageSize = 10;
        private int totalItems = 0;
        private int totalPages = 0;
        private IEnumerable<T> paginatedItems;

        private ApiService _apiService;

        public IEnumerable<T> PaginatedItems
        {
            get { return paginatedItems; }
            set { paginatedItems = value; }
        }

        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; }
        }

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        public int TotalItems
        {
            get { return totalItems; }
            set
            {
                totalItems = value;
                CalculateTotalPages();
            }
        }

        public int TotalPages
        {
            get { return totalPages; }
            set { totalPages = value; }
        }

        public PaginationService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public bool HasNextPage => currentPage < totalPages;
        public bool HasPreviousPage => currentPage > 1;

        public void InitializePagination(int initialPage = 1, int initialPageSize = 10)
        {
            currentPage = initialPage;
            pageSize = initialPageSize;
        }

        public async Task<IEnumerable<T>> GoToPage(int page)
        {
            if (page >= 1 && page <= totalPages)
            {
                currentPage = page;
                return await LoadPaginatetedData();
            }
            else
            {
                return null;
            }
        }

        private void CalculateTotalPages()
        {
            TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
        }

        public async Task<IEnumerable<T>> LoadPaginatetedData()
        {
            ApiResponseDTO<T> dto = await _apiService.GetAsync<ApiResponseDTO<T>>(CurrentPage, PageSize);
            PaginatedItems = dto.Items;
            TotalItems = dto.TotalCount;
            return PaginatedItems;
        }
    }
}
