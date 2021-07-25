using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SOPopularTags.Domain.Models
{
    public class SOTagRequestItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SOTagRequestId { get; set; }

        [ForeignKey(nameof(SOTagRequestId))]
        public virtual SOTagRequest SOTagRequest { get; set; }

        [JsonProperty("has_synonyms")]
        public bool HasSynonyms { get; set; }

        [JsonProperty("is_moderator_only")]
        public bool IsModeratorOnly { get; set; }

        [JsonProperty("is_required")]
        public bool IsRequired { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public double PopularityPercent { get; set; }
    }
}
