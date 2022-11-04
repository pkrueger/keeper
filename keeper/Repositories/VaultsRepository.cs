namespace keeper.Repositories
{
  public class VaultsRepository : IRepository<Vault, int>
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    public Vault Create(Vault vaultData)
    {
      string sql = @"
        INSERT INTO vaults(
          creatorId, name, description, img, isPrivate
        )
        VALUES (
          @CreatorId, @Name, @Description, @Img, @IsPrivate
        );
        SELECT LAST_INSERT_ID()
      ;";

      int vaultId = _db.ExecuteScalar<int>(sql, vaultData);
      return GetById(vaultId);
    }

    public void Delete(int vaultId)
    {
      throw new NotImplementedException();
    }

    public List<Vault> Get()
    {
      throw new NotImplementedException();
    }

    public Vault GetById(int vaultId)
    {
      string sql = @"
        SELECT
          v.*,
          a.*
        FROM vaults v
          JOIN accounts a ON a.id = v.creatorId
        WHERE v.id = @vaultId
      ;";

      Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
      {
        vault.Creator = profile;
        return vault;
      }, new { vaultId }).FirstOrDefault();
      return vault;
    }

    public Vault Update(Vault vaultData)
    {
      string sql = @"
        UPDATE vaults SET
          name = @Name,
          description = @Description,
          img = @Img,
          isPrivate = @IsPrivate
        WHERE id = @Id LIMIT 1
      ;";

      int rows = _db.Execute(sql, vaultData);
      if (rows == 0) { throw new Exception("No vault changes were saved."); }
      return GetById(vaultData.Id);
    }
  }
}