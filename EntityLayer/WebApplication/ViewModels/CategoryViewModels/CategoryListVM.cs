namespace EntityLayer.WebApplication.ViewModels.CategoryViewModels
{
    public class CategoryListVM
    {
        public virtual int Id { get; set; }
        public virtual string CreatedDate { get; set; } = DateTime.Now.ToString("d");
        public virtual string? UpdatedDate { get; set; }

        public string Name { get; set; } = null!;
    }
}
