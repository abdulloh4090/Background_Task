
using Microsoft.Data.SqlClient;
using Project.Models;

namespace Project.Persistance.SQLServer.Repasitories
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        #region Methods region

        #region Public methods region
        public async Task<List<ClientData>> GetAllClientsAsync()
        {
            var result = new List<ClientData>();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand("SELECT Id, FullName, Email FROM ClientData", connection);

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(new ClientData()
                {
                    Id = reader.GetInt32(0),
                    FullName = reader.GetString(1),
                    Email = reader.GetString(2)
                });
            }

            return result;
        }
        #endregion

        #endregion

    }
}
