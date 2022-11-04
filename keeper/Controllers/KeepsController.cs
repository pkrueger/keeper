namespace keeper.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _ks;
    private readonly Auth0Provider _a0p;

    public KeepsController(KeepsService ks, Auth0Provider a0p)
    {
      _ks = ks;
      _a0p = a0p;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep keepData)
    {
      try
      {
        Profile userInfo = await _a0p.GetUserInfoAsync<Profile>(HttpContext);
        keepData.CreatorId = userInfo.Id;
        Keep createdKeep = _ks.Create(keepData);
        return Ok(createdKeep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}