namespace keeper.Repositories;

public class AccountsRepository
{
  private readonly IDbConnection _db;

  public AccountsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Account GetByEmail(string userEmail)
  {
    string sql = "SELECT * FROM accounts WHERE email = @userEmail";
    return _db.QueryFirstOrDefault<Account>(sql, new { userEmail });
  }

  internal Account GetById(string id)
  {
    string sql = "SELECT * FROM accounts WHERE id = @id";
    return _db.QueryFirstOrDefault<Account>(sql, new { id });
  }

  internal Account Create(Account newAccount)
  {
    string sql = @"
            INSERT INTO accounts
              (name, picture, email, id)
            VALUES
              (@Name, @Picture, @Email, @Id)";
    _db.Execute(sql, newAccount);
    return newAccount;
  }

  internal Account Edit(Account update)
  {
    string sql = @"
              UPDATE accounts SET 
                name = @Name,
                picture = @Picture,
                coverImg = @CoverImg
              WHERE id = @Id LIMIT 1
            ;";
    int rows = _db.Execute(sql, update);
    if (rows == 0)
    {
      throw new Exception("No keep changes were saved.");
    }
    return GetById(update.Id);
  }

  internal List<Vault> GetVaultsByCreatorId(string creatorId)
  {
    string sql = @"
      SELECT 
        v.*,
        a.* 
      FROM vaults v
        JOIN accounts a ON a.id = v.creatorId
      WHERE creatorId = @creatorId
    ;";
    List<Vault> vaults = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
    {
      vault.Creator = account;
      return vault;
    }, new { creatorId }).ToList();
    return vaults;
  }

  internal List<Keep> GetKeepsByCreatorId(string creatorId)
  {
    string sql = @"
      SELECT 
        k.*,
        a.* 
      FROM keeps k
        JOIN accounts a ON a.id = k.creatorId
      WHERE creatorId = @creatorId
    ;";
    List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
    {
      keep.Creator = account;
      return keep;
    }, new { creatorId }).ToList();
    return keeps;
  }
}

