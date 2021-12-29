using Microsoft.AspNetCore.Mvc;
using SalutAPI.Domain;

namespace SalutAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComponentController : ControllerBase {
    ILogger<ComponentController> _log;
    
    public ComponentController(ILogger<ComponentController> logger) { _log = logger; }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Component component) {
        

        return Accepted();
    }
}