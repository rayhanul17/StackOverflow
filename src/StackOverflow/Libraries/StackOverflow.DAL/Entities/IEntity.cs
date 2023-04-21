namespace StackOverflow.DAL.Entities;

public interface IEntity<TKey>
{
    TKey Id { get; set; }
}
