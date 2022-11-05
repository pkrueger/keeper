namespace keeper.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _pr;

    public ProfilesService(ProfilesRepository pr)
    {
      _pr = pr;
    }

    internal Profile GetById(string profileId)
    {
      Profile profile = _pr.GetById(profileId);
      return profile;
    }

    internal List<Keep> GetKeepsByCreatorId(string profileId)
    {
      List<Keep> keeps = _pr.GetKeepsByCreatorId(profileId);
      return keeps;
    }

    internal List<Vault> GetVaultsByCreatorId(string profileId)
    {
      List<Vault> vaults = _pr.GetVaultsByCreatorId(profileId);
      return vaults;
    }
  }
}