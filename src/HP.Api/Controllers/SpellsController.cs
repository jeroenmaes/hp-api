using HP.Api.Dto;
using HP.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpellsController : ControllerBase
    {        
        private readonly ILogger<SpellsController> _logger;
        private readonly DataService _service;

        public SpellsController(ILogger<SpellsController> logger, DataService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet(Name = "spells")]
        public async Task<IEnumerable<Spell>> GetSpells()
        {
            return await _service.GetSpells();
        }
    }
}
