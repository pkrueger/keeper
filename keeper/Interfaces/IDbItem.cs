namespace keeper.Interfaces
{
  public interface IDbItem<T>
  {
    T Id { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
  }
}