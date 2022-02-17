using Microsoft.AspNetCore.Mvc;
using TableTopBattleTracker.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TableTopBattleTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonstersController : ControllerBase
    {
        private readonly ILogger<MonstersController> _logger;
        public MonstersController(ILogger<MonstersController> logger)
        {
            _logger = logger;
        }

        // GET: <MonstersController>
        [HttpGet]
        public IEnumerable<Monster> Get()
        {
            return new Monster[]{ new Monster() {Id = "2", Name = "MKo", Url = "2_Mko" },
                new Monster() {Id = "1", Name = "MKo", Url = "1_Mko" },
                new Monster() {Id = "5", Name = "MKo", Url = "5_Mko" },
            };
        }

        // GET api/<MonstersController>/5

        [HttpGet("{id}")]
        public Monster Get(int id)
        {
            return new Monster() { Id = "2", Name = "MKo", Url = "2_Mko" };
        }

    }
}
