namespace keeper.Interfaces
{
  public interface IHasCreator
  {
    string CreatorId { get; set; }
    Profile Creator { get; set; }
  }
}