namespace keeper.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vkr;
    private readonly VaultsService _vs;

    public VaultKeepsService(VaultKeepsRepository vkr, VaultsService vs)
    {
      _vkr = vkr;
      _vs = vs;
    }

    internal VaultKeep Create(VaultKeep vaultKeepData, Profile userInfo)
    {
      Vault vault = _vs.GetById(vaultKeepData.VaultId, userInfo);
      if (vault.CreatorId != vaultKeepData.CreatorId)
      {
        throw new Exception("You can only add keeps to your own vaults.");
      }
      return _vkr.Create(vaultKeepData);
    }

    internal VaultKeep GetById(int vaultKeepId)
    {
      VaultKeep vaultKeep = _vkr.GetById(vaultKeepId);
      return vaultKeep;
    }

    internal void Delete(int vaultKeepId, Profile userInfo)
    {
      VaultKeep vaultKeep = GetById(vaultKeepId);
      if (vaultKeep.CreatorId != userInfo.Id)
      {
        throw new Exception("You can't delete vault keeps that aren't yours.");
      }
      _vkr.Delete(vaultKeepId);
    }
  }
}