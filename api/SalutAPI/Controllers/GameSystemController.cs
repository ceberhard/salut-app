using Microsoft.AspNetCore.Mvc;
using SalutAPI.Domain;

namespace SalutAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameSystemController : ControllerBase {
    ILogger<GameSystemController> _log;
    
    public GameSystemController(ILogger<GameSystemController> logger) { _log = logger; }

    [HttpGet("{gameSystemId}")]
    public async Task<IActionResult> Get([FromRoute] long gameSystemId) => Ok(await new GameSystemEntity().GetGameSystemAsync(gameSystemId));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] GameSystem gameSystem) => Accepted(await new GameSystemEntity().CreateGameSystemAsync(gameSystem));

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] GameSystem gameSystem) => Accepted(await new GameSystemEntity().UpdateGameSystemAsync(gameSystem));

    [HttpPost("build/{gameSystemid}")]
    public async Task<IActionResult> Build([FromRoute]long gameSystemId) {
        return Ok(await new GameSystemEntity().BuildGameInstanceAsync(1001));
    }
}