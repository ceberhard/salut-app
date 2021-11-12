using Microsoft.AspNetCore.Cors;
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
    [EnableCors("SalutPolicy")]
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
            },
            new ContactEntity {
                Id = 3,
                FirstName = "Anakin",
                LastName = "Skywalker"
            },
            new ContactEntity {
                Id = 4,
                FirstName = "Leia",
                LastName = "Organa"
            }
        });
    }
}