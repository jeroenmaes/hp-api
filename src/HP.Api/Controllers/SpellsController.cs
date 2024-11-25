using HP.Api.Dto;
using HP.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HP.Api.Controllers
{
  [ApiController]
  [Route("v1/[controller]")]
  public class SpellsController : ControllerBase
  {
    private readonly ILogger<SpellsController> _logger;
    private readonly DataService _service;

    public SpellsController(ILogger<SpellsController> logger, DataService service)
    {
      _logger = logger;
      _service = service;
    }

    [Route("")]
    [HttpGet]
    public async Task<IEnumerable<Spell>> GetSpells()
    {
      return await _service.GetSpells();
    }

    [Route("{id}")]
    [HttpGet]
    public async Task<Spell> GetCharacterById(string id)
    {
      return (await _service.GetSpells()).SingleOrDefault(x => x.id == id);
    }
  }
}
