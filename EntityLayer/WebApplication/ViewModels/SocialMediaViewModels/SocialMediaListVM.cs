using EntityLayer.WebApplication.ViewModels.AboutViewModels;

namespace EntityLayer.WebApplication.ViewModels.SocialMediaViewModels
{
    public class SocialMediaListVM
    {
        public virtual int Id { get; set; }
        public virtual string CreatedDate { get; set; } = DateTime.Now.ToString("d");
        public virtual string? UpdatedDate { get; set; }

        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? LinkedIn { get; set; }
        public string? Instagram { get; set; }

        public AboutListVM AboutUs { get; set; } = null!;
    }
}
