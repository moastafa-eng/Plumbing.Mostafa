namespace EntityLayer.WebApplication.ViewModels.ServiceViewModels
{
    public class ServiceListVM
    {
        public virtual int Id { get; set; }
        public virtual string CreatedDate { get; set; } = DateTime.Now.ToString("d");
        public virtual string? UpdatedDate { get; set; }

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Icon { get; set; } = null!;
    }
}
