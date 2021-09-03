using Microsoft.AspNetCore.Mvc;
using SalutAPI.Domain;

namespace SalutAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase {
    ILogger<ContactsController> _log;
    
    public ContactsController(ILogger<ContactsController> logger) { 
        _log = logger;
    }

    [HttpGet]
    public IActionResult Get() {
        return Ok(new List<ContactEntity> {
            new ContactEntity {
                Id = 1,
                FirstName = "Luke",
                LastName = "Skywalker"
            },
            new ContactEntity {
                Id = 2,
                FirstName = "Han",
                LastName = "Solo"
            }
        });
    }
}