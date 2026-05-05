using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Entities.Core;

public class BaseEntity : ISoftDeleteable
{
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; } = null;
    public Guid? CreatedById { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    public Guid? UpdatedById { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public void UndoDelete()
    {
        IsDeleted = false;
        DeletedAt = null;
    }
}
