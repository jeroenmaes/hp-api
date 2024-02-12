using HP.Api.Dto;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace HP.Api.Services
{
    public class DataService
    {
        private readonly IMemoryCache _memoryCache;

        public DataService(IMemoryCache memoryCache) 
        {
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<Spell>> GetSpells()
        {
            return await _memoryCache.GetOrCreateAsync("GetSpells", async cache =>
            {
                cache.Priority = CacheItemPriority.NeverRemove;
                return await InternalGetSpells();
            });
        }

        private async Task<IEnumerable<Spell>> InternalGetSpells()
        {
            var fileName = @"Data/spells.json";
            await using FileStream stream = File.OpenRead(fileName);
            return await JsonSerializer.DeserializeAsync<IEnumerable<Spell>>(stream);
        }

        public async Task<IEnumerable<Character>> GetCharacters()
        {
            return await _memoryCache.GetOrCreateAsync("GetCharacters", async cache =>
            {                
                cache.Priority = CacheItemPriority.NeverRemove;
                return await InternalGetCharacters();
            });
        }

        private async Task<IEnumerable<Character>> InternalGetCharacters()
        {
            var fileName = @"Data/characters.json";
            await using FileStream stream = File.OpenRead(fileName);
            return await JsonSerializer.DeserializeAsync<IEnumerable<Character>>(stream);
        }

        internal async Task PreloadCache()
        {
            await GetCharacters();
            await GetSpells();
        }
    }
}
