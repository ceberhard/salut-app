using Microsoft.AspNetCore.Mvc;
using SalutAPI.Domain;

namespace SalutAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameSystemController : ControllerBase {
    ILogger<GameSystemController> _log;
    
    public GameSystemController(ILogger<GameSystemController> logger) { _log = logger; }

    [HttpGet]
    public async Task<IActionResult> Get() {
        return Ok(await new GameSystemRepo().FindByIdAsync(1001));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] GameSystem gameSystem) {
        return Accepted(await new GameSystemEntity().CreateGameSystem(gameSystem));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] GameSystem gameSystem) {
        return Accepted(await new GameSystemEntity().UpdateGameSystem(gameSystem));
    }

    [HttpPost("build/{gameSystemid}")]
    public async Task<IActionResult> Build([FromRoute]long gameSystemId) {
        return Ok(await new GameSystemEntity().BuildGameInstanceAsync(1001));
    }
}