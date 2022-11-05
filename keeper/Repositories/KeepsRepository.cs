namespace keeper.Repositories
{
  public class KeepsRepository : IRepository<Keep, int>
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    public Keep Create(Keep keepData)
    {
      string sql = @"
        INSERT INTO keeps(
          creatorId, name, description, img
        )
        VALUES (
          @CreatorId, @Name, @Description, @Img
        );
        SELECT LAST_INSERT_ID()
      ;";

      int keepId = _db.ExecuteScalar<int>(sql, keepData);
      return GetById(keepId);
    }

    public void Delete(int keepId)
    {
      string sql = "DELETE FROM keeps WHERE id = @keepId LIMIT 1;";
      int rows = _db.Execute(sql, new { keepId });
      if (rows == 0)
      {
        throw new Exception("Keep was not able to be deleted.");
      }
    }

    public List<Keep> Get()
    {
      string sql = @"
        SELECT 
          k.*,
          a.*
        FROM keeps k
          JOIN accounts a ON a.id = k.creatorId
      ;";
      List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }).ToList();
      return keeps;
    }

    public Keep GetById(int keepId)
    {
      // NOTE Populates the creator as well as the kept count
      string sql = @"
        SELECT 
        k.*,
        COUNT(vk.id) AS kept,
        a.*
        FROM keeps k
        JOIN accounts a ON a.id = k.creatorId
        LEFT JOIN vaultKeeps vk ON vk.keepId = k.id
        WHERE k.id = @keepId
        GROUP BY k.id
      ;";
      // REVIEW What does the group by do here?
      Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }, new { keepId }).FirstOrDefault();
      return keep;
    }

    internal List<KeptKeep> GetByVaultId(int vaultId)
    {
      string sql = @"
        SELECT
          k.*,
          vk.id AS VaultKeepId,
          vk.creatorId AS KeeperId,
          vk.vaultId,
          a.*
        FROM vaultKeeps vk
          JOIN keeps k ON k.id = vk.keepId
          JOIN accounts a ON a.id = k.creatorId
        WHERE vk.vaultId = @vaultId
      ;";
      List<KeptKeep> keptKeeps = _db.Query<KeptKeep, Profile, KeptKeep>(sql, (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }, new { vaultId }).ToList();
      return keptKeeps;
    }

    public Keep Update(Keep keepData)
    {
      string sql = @"
        UPDATE keeps SET
          name = @Name,
          description = @Description
        WHERE id = @Id LIMIT 1
      ;";
      int rows = _db.Execute(sql, keepData);
      if (rows == 0)
      {
        throw new Exception("No keep changes were saved.");
      }
      return GetById(keepData.Id);
    }
  }
}