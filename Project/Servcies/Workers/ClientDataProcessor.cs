

using Project.Models;

namespace Project.Servcies
{
    public class ClientDataProcessor : BackgroundService
    {
        private readonly QueueService<ClientData> _queueService;
        private readonly ILogger<ClientDataProcessor> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public ClientDataProcessor(QueueService<ClientData> queueService, ILogger<ClientDataProcessor> logger, IHttpClientFactory httpClientFactory)
        {
            _queueService = queueService;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        #region Methods region

        #region Public methods region
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var client = _httpClientFactory.CreateClient();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var clientData = await _queueService.DequeueAsync(stoppingToken);

                    var response = await client.PostAsJsonAsync("https://localhost:7297/api/client/post", clientData, stoppingToken);
                    response.EnsureSuccessStatusCode();

                    _logger.LogInformation($"Client ID {clientData.Id} muvaffaqiyatli yuborildi.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ma'lumotni yuborishda xatolik.");
                }
            }
        }

        #endregion

        #endregion
    }
}
