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

    [HttpGet]
    public ActionResult<List<Keep>> GetAll()
    {
      try
      {
        List<Keep> keeps = _ks.GetAll();
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{keepId}")]
    public ActionResult<Keep> GetById(int keepId)
    {
      try
      {
        Keep keep = _ks.GetById(keepId);
        _ks.IncreaseViewCount(keep);
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{keepId}")]
    [Authorize]
    public async Task<ActionResult<Keep>> Edit(int keepId, [FromBody] Keep keepData)
    {
      try
      {
        Profile userInfo = await _a0p.GetUserInfoAsync<Profile>(HttpContext);
        keepData.CreatorId = userInfo.Id;
        keepData.Id = keepId;
        Keep keep = _ks.Edit(keepData);
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{keepId}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int keepId)
    {
      try
      {
        Profile userInfo = await _a0p.GetUserInfoAsync<Profile>(HttpContext);
        _ks.Delete(keepId, userInfo.Id);
        return Ok("Keep has been deleted.");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}