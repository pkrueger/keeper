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
  }
}