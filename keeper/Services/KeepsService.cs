namespace keeper.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _kr;

    public KeepsService(KeepsRepository kr)
    {
      _kr = kr;
    }

    internal Keep Create(Keep keepData)
    {
      return _kr.Create(keepData);
    }
  }
}