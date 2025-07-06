using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Persistance.SQLServer.Repasitories;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly DatabaseService _databaseService;
        private readonly ILogger<ClientController> _logger;

        public ClientController(DatabaseService databaseService, ILogger<ClientController> logger)
        {
            _databaseService = databaseService;
            _logger = logger;
        }

        #region Methods region

        #region Public methods region

        /// <summary>
        /// Queue orqali kelgan Client malumotini qabul qiladi
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody] ClientData client)
        {
            _logger.LogInformation($"POST qabul qilindi: {client.FullName} - {client.Email}");
            return Ok(new
            {
                message = "Muvaffaqiyatli qabul qilindi !",
                data = client
            });
        }

        /// <summary>
        /// DatabaseService orqali databasedan barcha Clientlarni oladi
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var result = await _databaseService.GetAllClientsAsync();
            return Ok(result);
        }

        #endregion

        #endregion

    }
}
