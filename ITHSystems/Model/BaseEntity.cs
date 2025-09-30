namespace ITHSystems.Model;

public interface IBaseEntity
{
   public DateTime CreationTime { get; set; } 
   public DateTime? LastModified { get; set; }
   public DateTime? DeletionTime { get; set; }
   public bool IsActive { get; set; } 
   public bool IsDeleted { get; set; }
}
