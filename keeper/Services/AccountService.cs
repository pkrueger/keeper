namespace keeper.Services;

public class AccountService
{
  private readonly AccountsRepository _repo;

  public AccountService(AccountsRepository repo)
  {
    _repo = repo;
  }

  internal Account GetProfileByEmail(string email)
  {
    return _repo.GetByEmail(email);
  }

  internal Account GetOrCreateProfile(Account userInfo)
  {
    Account profile = _repo.GetById(userInfo.Id);
    if (profile == null)
    {
      return _repo.Create(userInfo);
    }
    return profile;
  }

  internal List<Vault> GetMyVaults(string creatorId)
  {
    List<Vault> vaults = _repo.GetVaultsByCreatorId(creatorId);
    return vaults;
  }

  internal List<Keep> GetMyKeeps(string creatorId)
  {
    List<Keep> keeps = _repo.GetKeepsByCreatorId(creatorId);
    return keeps;
  }

  internal Account Edit(Account accountData)
  {
    Account original = GetOrCreateProfile(accountData);
    original.Name = accountData.Name.Length > 0 ? accountData.Name : original.Name;
    original.Picture = accountData.Picture.Length > 0 ? accountData.Picture : original.Picture;
    original.CoverImg = accountData.CoverImg.Length > 0 ? accountData.CoverImg : original.CoverImg;
    return _repo.Edit(original);
  }
}
