using Newtonsoft.Json;

namespace EmployeeManagement.API.ViewModels
{
    public class PaginationViewModel
    {
        [JsonProperty("totalRecords")]
        public int TotalRecords { get; set; }
        [JsonProperty("currentPageRecords")]
        public int CurrentPageRecords { get; set; }
        [JsonProperty("pageSize")]
        public int PageSize { get; set; } = 5;
        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; } = 0;
        [JsonProperty("startIndex")]
        public int StartIndex { get; set; } = 0;

        [JsonProperty("endIndex")]
        public int EndIndex { get; set; }
    }
}
