namespace Shared.DDD
{
    public interface IEntity<T> : IEntity
    {
        public T Id { get; set; }
    }

    public interface IEntity
    {
        public string? LastModifiedBy { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime LastModifiedAt { get; set; }
    }
}
