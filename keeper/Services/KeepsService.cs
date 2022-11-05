namespace keeper.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _kr;
    private readonly VaultsService _vs;

    public KeepsService(KeepsRepository kr, VaultsService vs)
    {
      _kr = kr;
      _vs = vs;
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

    internal List<KeptKeep> GetKeepsByVaultId(int vaultId, Profile userInfo)
    {
      Vault vault = _vs.GetById(vaultId, userInfo);
      List<KeptKeep> keptKeeps = _kr.GetByVaultId(vaultId);
      return keptKeeps;
    }
  }
}