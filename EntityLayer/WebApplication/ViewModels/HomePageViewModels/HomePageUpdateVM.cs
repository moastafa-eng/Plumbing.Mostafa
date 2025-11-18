namespace EntityLayer.WebApplication.ViewModels.HomePageViewModels
{
    public class HomePageUpdateVM
    {
        public virtual int Id { get; set; }
        public virtual string? UpdatedDate { get; set; }
        public virtual byte[] RowVersion { get; set; } = null!;

        public string Header { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string VideoLink { get; set; } = null!;
    }
}
