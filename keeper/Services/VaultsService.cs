namespace keeper.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vr;

    public VaultsService(VaultsRepository vr)
    {
      _vr = vr;
    }

    internal Vault Create(Vault vaultData)
    {
      return _vr.Create(vaultData);
    }

    internal Vault GetById(int vaultId, Profile userInfo)
    {
      Vault foundVault = _vr.GetById(vaultId);
      if (foundVault == null) { throw new Exception("Bad Vault Id."); }
      if (foundVault.IsPrivate && (userInfo == null || foundVault.CreatorId != userInfo.Id))
      {
        throw new Exception("You can't access private vaults that aren't yours.");
      }
      return foundVault;
    }

    internal Vault Edit(Vault vaultData, Profile userInfo)
    {
      Vault originalVault = GetById(vaultData.Id, userInfo);
      if (originalVault.CreatorId != userInfo.Id)
      {
        throw new Exception("You can't edit a vault that isn't yours.");
      }

      originalVault.Name = vaultData.Name ?? originalVault.Name;
      originalVault.Description = vaultData.Description ?? originalVault.Description;
      originalVault.Img = vaultData.Img ?? originalVault.Img;
      originalVault.IsPrivate = vaultData.IsPrivate;

      Vault vault = _vr.Update(originalVault);
      return vault;
    }
  }
}