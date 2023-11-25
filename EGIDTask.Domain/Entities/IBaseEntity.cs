namespace EGIDTask.Domain.Entities
{
    public interface IBaseEntity
    {
        DateTime AddedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}