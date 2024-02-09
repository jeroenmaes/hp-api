using HP.Api.Dto;
using System.Text;
using System.Text.Json;

namespace HP.Api.Services
{
    public class DataService
    {
        public async Task<IEnumerable<Spell>> GetSpells()
        {            
            var fileName = @".\Data\spells.json";
            await using FileStream stream = File.OpenRead(fileName);
            var spells = await JsonSerializer.DeserializeAsync<IEnumerable<Spell>>(stream);
            return spells;
        }

        public async Task<IEnumerable<Character>> GetCharacters()
        {
            var fileName = @".\Data\characters.json";
            await using FileStream stream = File.OpenRead(fileName);
            var characters = await JsonSerializer.DeserializeAsync<IEnumerable<Character>>(stream);
            return characters;
        }
    }
}
