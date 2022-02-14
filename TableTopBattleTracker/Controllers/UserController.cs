using Microsoft.AspNetCore.Mvc;

namespace TableTopBattleTracker.Controllers
{
    public class UserController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
