using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.API.ViewModels
{
    public class EmployeeFilterViewModel
    {

        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; } = 0;

        [JsonProperty("pageSize")]
        public int PageSize { get; set; } = 5;
    }
}
