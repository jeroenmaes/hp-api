namespace HP.Api.Services
{
    public class CacheService : IHostedService
    {
        private readonly DataService _dataService;

        public CacheService(DataService dataService)
        {
            _dataService = dataService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {            
            await _dataService.PreloadCache();
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
