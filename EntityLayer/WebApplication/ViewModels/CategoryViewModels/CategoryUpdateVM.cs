namespace EntityLayer.WebApplication.ViewModels.CategoryViewModels
{
    public class CategoryUpdateVM
    {
        public virtual int Id { get; set; }
        public virtual string? UpdatedDate { get; set; }
        public virtual byte[] RowVersion { get; set; } = null!;

        public string Name { get; set; } = null!;
    }
}
