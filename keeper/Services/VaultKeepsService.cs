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
  }
}