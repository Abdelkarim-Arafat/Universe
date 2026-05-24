namespace Universe.Core.Entities.Core;

internal interface ISoftDeleteable
{
    bool IsDeleted { get; set; }
    DateTime? DeletedAt { get; set; }
    void UndoDelete();
}
