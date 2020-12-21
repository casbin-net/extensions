Casbin.NET Extensions
Helpful extensions for Casbin.NET, such as frontend extension to casbin.js.

## Frontend

Example of [advanced usage](https://casbin.org/docs/en/frontend#advanced-usage) of authZ

```csharp
// {...}
using Casbin.Extension.Frontend;

[Route("api/casbin")]
public class CasbinController : Controller
{
    private readonly IEnforcer _enforcer;

    public CasbinController(IEnforcer enforcer)
    {
        _enforcer = enforcer;
    }

    [HttpGet]
    public ActionResult Get([FromQuery("casbin_subject")] string user)
    {
        var permissions = _enforcer.CasbinJsGetPermissionForUser(user);
        return Json(new { perm = permissions })
    }
}
```