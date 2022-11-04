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
      throw new NotImplementedException();
    }

    public List<Keep> Get()
    {
      throw new NotImplementedException();
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

    public Keep Update(Keep keepData)
    {
      throw new NotImplementedException();
    }
  }
}