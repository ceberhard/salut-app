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
        return Ok(await new GameSystemRepo().FindById(1001));
    }
}