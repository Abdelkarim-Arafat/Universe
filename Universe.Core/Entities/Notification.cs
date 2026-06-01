using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities.Core;
using Universe.Core.Enums;

namespace Universe.Core.Entities;

public class Notification : BaseEntity
{
    public Guid Id { get; set; }
    public Notification() { Id = Guid.CreateVersion7(); }
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = default!;
    public NotificationType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public Guid? RelatedId { get; set; }
    public bool IsSeen { get; set; }
    public ICollection<ApplicationUser> Users { get; set; } = [];
}