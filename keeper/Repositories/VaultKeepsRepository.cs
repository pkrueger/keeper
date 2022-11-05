namespace keeper.Repositories
{
  public class VaultKeepsRepository : IRepository<VaultKeep, int>
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    public VaultKeep Create(VaultKeep vaultKeepData)
    {
      string sql = @"
        INSERT INTO vaultKeeps(
          creatorId, vaultId, keepId
        )
        VALUES (
          @CreatorId, @VaultId, @KeepId
        );
        SELECT LAST_INSERT_ID()
      ;";
      int vaultKeepId = _db.ExecuteScalar<int>(sql, vaultKeepData);
      VaultKeep vk = GetById(vaultKeepId);
      return vk;
    }

    public void Delete(int vaultKeepId)
    {
      throw new NotImplementedException();
    }

    public List<VaultKeep> Get()
    {
      throw new NotImplementedException();
    }

    public VaultKeep GetById(int vaultKeepId)
    {
      string sql = @"
        SELECT * FROM vaultKeeps
        WHERE id = @vaultKeepId
      ;";
      VaultKeep vk = _db.QueryFirstOrDefault<VaultKeep>(sql, new { vaultKeepId });
      return vk;
    }

    public VaultKeep Update(VaultKeep vaultKeepData)
    {
      throw new NotImplementedException();
    }
  }
}