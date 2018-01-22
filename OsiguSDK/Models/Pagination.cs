using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsiguSDK.Core.Models
{
    public class Pagination<T>
    {

        /// <summary>
        /// Number of pages available
        /// </summary>
            [JsonProperty(PropertyName = "total_pages")]
            public int TotalPages { get; set; }


        /// <summary>
        /// Number of elements in the current page
        /// </summary>
        [JsonProperty(PropertyName = "number_of_elements")]
        public int NumberOfElements { get; set; }

        /// <summary>
        /// Total elements in the result of the query
        /// </summary>
        [JsonProperty(PropertyName = "total_elements")]
        public int TotalElements { get; set; }


        /// <summary>
        /// bool indicating if the current page is the first
        /// </summary>
        [JsonProperty(PropertyName = "first_page")]
        public bool FirstPage { get; set; }

        /// <summary>
        /// bool indicating if the current page is the last
        /// </summary>
        [JsonProperty(PropertyName = "last_page")]
        public bool LastPage { get; set; }


        /// <summary>
        /// Content of the result
        /// </summary>
        [JsonProperty(PropertyName = "content")]
        public List<T> Content { get; set; }

        /// <summary>
        /// Links for navigate between pages
        /// </summary>
        [JsonProperty(PropertyName = "_links")]
        public PaginationLinks Links { get; set; }
    }
}
