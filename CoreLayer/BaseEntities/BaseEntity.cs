using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.BaseEntities
{
    public abstract class BaseEntity : IBaseEntity
    {
        // virtual: allows derived classes to override and change this property's behavior.
        public virtual int Id { get; set; } // EFCore Assign this Id to all Entities as a Primary Key
        public virtual string CreatedAt { get; set; } = DateTime.Now.ToString("d");
        public virtual string? UpdatedAt { get; set; }

        // Ensures no two users update the same record with old data.
        // EF checks this value(RowVersion), and if it changed => throws DbUpdateConcurrencyException.
        public virtual byte[] RowVersion { get; set; } = null!;
    }
}
