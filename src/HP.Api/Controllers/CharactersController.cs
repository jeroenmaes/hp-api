using HP.Api.Dto;
using HP.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HP.Api.Controllers
{
  [ApiController]
  [Route("v1/[controller]")]
  public class CharactersController : ControllerBase
  {
    private readonly ILogger<CharactersController> _logger;
    private readonly DataService _service;

    public CharactersController(ILogger<CharactersController> logger, DataService service)
    {
      _logger = logger;
      _service = service;
    }

    [Route("")]
    [HttpGet]
    public async Task<IEnumerable<Character>> GetCharacters(string name = "")
    {
      if(name == "")
        return await _service.GetCharacters();

      return (await _service.GetCharacters()).Where(x => x.name.ToLowerInvariant().StartsWith(name.ToLowerInvariant()));
    }

    [Route("{id}")]
    [HttpGet]
    public async Task<Character> GetCharacterById(string id)
    {
      return (await _service.GetCharacters()).SingleOrDefault(x => x.id == id);
    }
        
    [Route("students")]
    [HttpGet]
    public async Task<IEnumerable<Character>> GetCharacterStudents()
    {
      return (await _service.GetCharacters()).Where(x => x.hogwartsStudent);
    }

    [Route("staff")]
    [HttpGet]
    public async Task<IEnumerable<Character>> GetCharacterStaff()
    {
      return (await _service.GetCharacters()).Where(x => x.hogwartsStaff);
    }

    [Route("house/{house}")]
    [HttpGet]
    public async Task<IEnumerable<Character>> GetCharacterByHouse(string house)
    {
      return (await _service.GetCharacters()).Where(x => x.house.ToLowerInvariant().StartsWith(house.ToLowerInvariant()));
    }
  }
}
