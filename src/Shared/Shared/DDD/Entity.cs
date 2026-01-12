namespace Shared.DDD
{
    public abstract class Entity<T> : IEntity<T>
    {
        public string? LastModifiedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
        public T Id { get; set; }
    }
}
