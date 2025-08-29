using SQLite;

namespace ITHSystems.Model;

public abstract class BaseEntity<T>
{
    [PrimaryKey]
    public virtual T Id { get; set; } = default!;
    public DateTime CreationTime { get; set; } = DateTime.Now;
    public DateTime? LastModified { get; set; }
    public DateTime? DeletionTime { get; set; }
    public bool IsActive { get; set; } 
    public bool IsDeleted { get; set; }
}
