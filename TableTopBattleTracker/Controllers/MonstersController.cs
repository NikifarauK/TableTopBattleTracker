using Microsoft.AspNetCore.Mvc;
using TableTopBattleTracker.Data;
using TableTopBattleTracker.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TableTopBattleTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonstersController : ControllerBase
    {
        private readonly ILogger<MonstersController> _logger;
        private readonly IRepository<Monster> _repository;
        public MonstersController(ILogger<MonstersController> logger, IRepository<Monster> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: <MonstersController>
        [HttpGet]
        public IEnumerable<Monster> Get()
        {
            return _repository?.GetItems() ?? new[] {new Monster { Index=1, Name="Mock1", Url=@"/no"},
                                                     new Monster { Index=2, Name="Mock2", Url=@"/no"},
                                                     new Monster { Index=3, Name="Mock3", Url=@"/no"},};
        }

        // GET api/<MonstersController>/5
        [HttpGet("{id}")]
        public Monster Get(int id)
        {
            return _repository?.GetItem(id) ?? new Monster { Index = 1, Name = "Mock", Url = @"/no" };
        }

    }
}
