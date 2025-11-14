using CoreLayer.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.WebApplication.Entities
{
    public class Testimonial : BaseEntity
    {
        string Comment { get; set; } = null!;
        string FullName { get; set; } = null!;
        string title { get; set; } = null!;
        string FileName { get; set; } = null!;
        string FileType { get; set; } = null!;
    }
}
