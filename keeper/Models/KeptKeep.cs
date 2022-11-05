namespace keeper.Models
{
  public class KeptKeep : Keep
  {
    public int VaultKeepId { get; set; }
    public string KeeperId { get; set; }
    public int VaultId { get; set; }

  }
}