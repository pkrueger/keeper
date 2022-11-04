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

    internal List<Keep> GetAll()
    {
      return _kr.Get();
    }

    internal Keep GetById(int keepId)
    {
      Keep keep = _kr.GetById(keepId);
      if (keep == null)
      {
        throw new Exception("Bad Keep Id");
      }
      return keep;
    }

    internal Keep Edit(Keep keepData)
    {
      Keep originalKeep = GetById(keepData.Id);

      if (originalKeep.CreatorId != keepData.CreatorId)
      {
        throw new Exception("You can't edit a Keep that isn't yours.");
      }

      originalKeep.Description = keepData.Description ?? originalKeep.Description;
      originalKeep.Name = keepData.Name ?? originalKeep.Name;

      Keep keep = _kr.Update(originalKeep);
      return keep;
    }

    internal void Delete(int keepId, string creatorId)
    {
      Keep keep = GetById(keepId);

      if (keep.CreatorId != creatorId)
      {
        throw new Exception("You can't delete a Keep that isn't yours.");
      }

      _kr.Delete(keepId);
    }
  }
}