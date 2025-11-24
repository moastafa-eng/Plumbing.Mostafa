using EntityLayer.WebApplication.ViewModels.SocialMediaViewModels;
using Microsoft.AspNetCore.Http;

namespace EntityLayer.WebApplication.ViewModels.AboutViewModels
{
    public class AboutUpdateVM
    {
        public virtual int Id { get; set; }
        public virtual string? UpdatedDate { get; set; }
        public virtual byte[] RowVersion { get; set; } = null!;

        public string Header { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Clients { get; set; }
        public int Projects { get; set; }
        public int HourOfSupport { get; set; }
        public int HardWorkers { get; set; }
        public string FileType { get; set; } = null!;
        public string FileName { get; set; } = null!;

        public IFormFile Photo { get; set; } = null!;

        public int SocialMediaId { get; set; }
        public SocialMediaUpdateVM SocialMedia { get; set; } = null!;
    }
}
