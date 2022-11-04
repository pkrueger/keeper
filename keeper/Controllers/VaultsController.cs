namespace keeper.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;
    private readonly Auth0Provider _a0p;

    public VaultsController(VaultsService vs, Auth0Provider a0p)
    {
      _vs = vs;
      _a0p = a0p;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
    {
      try
      {
        Profile userInfo = await _a0p.GetUserInfoAsync<Profile>(HttpContext);
        vaultData.CreatorId = userInfo.Id;
        Vault createdVault = _vs.Create(vaultData);
        return Ok(createdVault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{vaultId}")]
    public async Task<ActionResult<Vault>> GetById(int vaultId)
    {
      try
      {
        Profile userInfo = await _a0p.GetUserInfoAsync<Profile>(HttpContext);

        Vault vault = _vs.GetById(vaultId, userInfo);
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{vaultId}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Edit(int vaultId, [FromBody] Vault vaultData)
    {
      try
      {
        Profile userInfo = await _a0p.GetUserInfoAsync<Profile>(HttpContext);
        vaultData.CreatorId = userInfo.Id;
        vaultData.Id = vaultId;
        Vault vault = _vs.Edit(vaultData, userInfo);
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}