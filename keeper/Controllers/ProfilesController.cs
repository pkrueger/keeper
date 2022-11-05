namespace keeper.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;
    private readonly Auth0Provider _a0p;

    public ProfilesController(ProfilesService ps, Auth0Provider a0p)
    {
      _ps = ps;
      _a0p = a0p;
    }

    [HttpGet("{profileId}")]
    public ActionResult<Profile> GetById(string profileId)
    {
      try
      {
        Profile profile = _ps.GetById(profileId);
        return Ok(profile);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetKeepsByCreatorId(string profileId)
    {
      try
      {
        List<Keep> keeps = _ps.GetKeepsByCreatorId(profileId);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{profileId}/vaults")]
    public ActionResult<List<Vault>> GetVaultsByCreatorId(string profileId)
    {
      try
      {
        List<Vault> vaults = _ps.GetVaultsByCreatorId(profileId);
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}