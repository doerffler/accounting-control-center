using System.Collections.Generic;

namespace ACCDataModel.DTO
{
    public class ApiResponseDTO<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
