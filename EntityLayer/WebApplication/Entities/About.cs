using CoreLayer.BaseEntities;

namespace EntityLayer.WebApplication.Entities
{
    public class About : BaseEntity
    {
        public string Header { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Clients { get; set; } = null!;
        public string Projects { get; set; } = null!;
        public string HourOfSupport { get; set; } = null!;
        public string HardWorkers { get; set; } = null!;
        public string FileType { get; set; } = null!;
        public string FileName { get; set; } = null!;


        public int SocialMediaId { get; set; } // Foreign Key
        public SocialMedia SocialMedia { get; set; } = null!; // Nav Property

    }
}
