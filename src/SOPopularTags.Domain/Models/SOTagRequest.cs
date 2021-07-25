using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SOPopularTags.Domain.Models
{
    public class SOTagRequest
    {
        [Key]
        public int Id { get; set; }

        [JsonProperty("items")]
        public virtual List<SOTagRequestItem> Items { get; set; }
    }
}
