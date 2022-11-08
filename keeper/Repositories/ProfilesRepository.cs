namespace keeper.Repositories
{
  public class ProfilesRepository : IRepository<Profile, string>
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    public Profile Create(Profile data)
    {
      throw new NotImplementedException();
    }

    public void Delete(string id)
    {
      throw new NotImplementedException();
    }

    public List<Profile> Get()
    {
      throw new NotImplementedException();
    }

    public Profile GetById(string profileId)
    {
      string sql = @"
        SELECT * FROM accounts
        WHERE id = @profileId
      ;";
      Profile profile = _db.QueryFirstOrDefault<Profile>(sql, new { profileId });
      return profile;
    }

    internal List<Keep> GetKeepsByCreatorId(string profileId)
    {
      string sql = @"
        SELECT 
          k.*,
          a.* 
        FROM keeps k
          JOIN accounts a ON a.id = k.creatorId
        WHERE creatorId = @profileId
      ;";
      List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }, new { profileId }).ToList();
      return keeps;
    }

    internal List<Vault> GetVaultsByCreatorId(string profileId)
    {
      string sql = @"
        SELECT 
          v.*,
          a.* 
        FROM vaults v
          JOIN accounts a ON a.id = v.creatorId
        WHERE creatorId = @profileId AND isPrivate = false
      ;";
      List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
      {
        vault.Creator = profile;
        return vault;
      }, new { profileId }).ToList();
      return vaults;
    }

    public Profile Update(Profile data)
    {
      throw new NotImplementedException();
    }
  }
}