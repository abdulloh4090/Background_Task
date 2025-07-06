

using Project.Models;
using Project.Persistance.SQLServer.Repasitories;

namespace Project.Servcies
{
    public class QueryService : BackgroundService
    {
        private readonly ILogger<QueryService> _logger;
        private readonly DatabaseService _databaseService;
        private readonly QueueService<ClientData> _queue;

        public QueryService(ILogger<QueryService> logger, DatabaseService databaseService, QueueService<ClientData> queue)
        {
            _logger = logger;
            _databaseService = databaseService;
            _queue = queue;
        }

        #region Methods region

        #region Public methods region
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var clients = await _databaseService.GetAllClientsAsync();

                foreach (var client in clients)
                {
                    await _queue.EnqueueAsync(client);
                }

                _logger.LogInformation($"{clients.Count} ta client queuega qo'shildi");

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }

        #endregion 

        #endregion 
    }
}
