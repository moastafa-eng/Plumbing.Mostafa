using EntityLayer.WebApplication.ViewModels.CategoryViewModels;
using Microsoft.AspNetCore.Http;

namespace EntityLayer.WebApplication.ViewModels.PortfolioViewModels
{
    public class PortfolioUpdateVM
    {
        public virtual int Id { get; set; }
        public virtual string? UpdatedDate { get; set; }
        public virtual byte[] RowVersion { get; set; } = null!;

        public string Title { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public string FileType { get; set; } = null!;

        public IFormFile Photo { get; set; } = null!;

        public int CategoryId { get; set; }
        public CategoryUpdateVM Category { get; set; } = null!;
    }
}
