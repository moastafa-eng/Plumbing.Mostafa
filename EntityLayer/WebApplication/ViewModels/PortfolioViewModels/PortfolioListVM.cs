using EntityLayer.WebApplication.ViewModels.CategoryViewModels;

namespace EntityLayer.WebApplication.ViewModels.PortfolioViewModels
{
    public class PortfolioListVM
    {
        public virtual int Id { get; set; }
        public virtual string CreatedDate { get; set; } = DateTime.Now.ToString("d");
        public virtual string? UpdatedDate { get; set; }

        public string Title { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public string FileType { get; set; } = null!;

        public int CategoryId { get; set; }
        public CategoryListVM Category { get; set; } = null!;
    }
}
