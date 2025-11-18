using EntityLayer.WebApplication.ViewModels.AboutViewModels;

namespace EntityLayer.WebApplication.ViewModels.SocialMediaViewModels
{
    public class SocialMediaUpdateVM
    {
        public virtual int Id { get; set; }
        public virtual string? UpdatedDate { get; set; }
        public virtual byte[] RowVersion { get; set; } = null!;

        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? LinkedIn { get; set; }
        public string? Instagram { get; set; }

        public AboutUpdateVM AboutUs { get; set; } = null!;
    }
}
