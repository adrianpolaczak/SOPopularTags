namespace SOPopularTags.Application.ViewModels
{
    public class PopularTagForHomeVM
    {
        public bool Has_synonyms { get; set; }
        public bool Is_moderator_only { get; set; }
        public bool Is_required { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
    }
}
