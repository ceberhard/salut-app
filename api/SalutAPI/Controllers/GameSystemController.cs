using Microsoft.AspNetCore.Mvc;
using SalutAPI.Domain;

namespace SalutAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameSystemController : ControllerBase {
    ILogger<ContactsController> _log;
    
    public GameSystemController(ILogger<ContactsController> logger) { _log = logger; }

    [HttpGet]
    public async Task<IActionResult> Get() {
        return Ok(await new GameSystemRepo().FindByIdAsync(1001));
    }

    [HttpPost("build/{gameSystemid}")]
    public async Task<IActionResult> Build([FromRoute]long gameSystemId) {
        return Ok(await new GameSystemEntity().BuildGameInstance(1001));
    }
}