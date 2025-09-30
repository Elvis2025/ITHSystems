using SQLite;

namespace ITHSystems.Model;

public abstract class Entity<TPrimaryKey> : IBaseEntity
{
    [PrimaryKey]
    public virtual TPrimaryKey Id { get; set; } = default!;
    public DateTime CreationTime { get; set; }
    public DateTime? LastModified { get; set; }
    public DateTime? DeletionTime { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}
