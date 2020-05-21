using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Gero.API.Models
{
    public class Paging
    {
        public Paging(int totalEntries, int pageNumber, int pageSize, int totalPages)
        {
            TotalEntries = totalEntries;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = totalPages;
        }

        public int TotalEntries { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalPages { get; }

        public string ToJson() =>
            JsonConvert.SerializeObject(
                this,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
            );
    }
}
