using EntityLayer.WebApplication.ViewModels.SocialMediaViewModels;
using Microsoft.AspNetCore.Http;

namespace EntityLayer.WebApplication.ViewModels.AboutViewModels
{
    public class AboutAddVM
    {

        public string Header { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Clients { get; set; }
        public int Projects { get; set; }
        public int HourOfSupport { get; set; }
        public int HardWorkers { get; set; }
        public string FileType { get; set; } = null!;
        public string FileName { get; set; } = null!;



        // Temporary holder for the uploaded image (not stored in the database)
        public IFormFile Photo { get; set; } = null!;


        public int SocialMediaId { get; set; } 
        public SocialMediaAddVM SocialMedia { get; set; } = null!;
    }
}
