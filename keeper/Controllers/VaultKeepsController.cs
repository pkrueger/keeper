namespace keeper.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;
    private readonly Auth0Provider _a0p;

    public VaultKeepsController(VaultKeepsService vks, Auth0Provider a0p)
    {
      _vks = vks;
      _a0p = a0p;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vaultKeepData)
    {
      try
      {
        Profile userInfo = await _a0p.GetUserInfoAsync<Profile>(HttpContext);
        vaultKeepData.CreatorId = userInfo.Id;
        VaultKeep createdVaultKeep = _vks.Create(vaultKeepData, userInfo);
        return Ok(createdVaultKeep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{vaultKeepId}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int vaultKeepId)
    {
      try
      {
        Profile userInfo = await _a0p.GetUserInfoAsync<Profile>(HttpContext);
        _vks.Delete(vaultKeepId, userInfo);
        return Ok("Vault Keep was deleted.");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}