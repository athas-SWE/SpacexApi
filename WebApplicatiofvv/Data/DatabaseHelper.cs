using Microsoft.Data.SqlClient;
using WebApplicatiofvv.Model;

namespace WebApplicatiofvv.Data
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;
        public DatabaseHelper(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public async Task<Launch?> GetById(string id)
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            var cmd = new SqlCommand("SELECT * FROM Launches WHERE Id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Launch
                {
                    Id = reader["Id"].ToString()!,
                    Name = reader["Name"].ToString()!,
                    DateUtc = Convert.ToDateTime(reader["DateUtc"]),
                    Rocket = reader["Rocket"].ToString()!
                };
            }
            return null;
        }

        public async Task<List<Launch>> GetAll()
        {
            List<Launch> launches = [];
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            var cmd = new SqlCommand("SELECT * FROM Launches", conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                launches.Add(new Launch
                {
                    Id = reader["Id"].ToString()!,
                    Name = reader["Name"].ToString()!,
                    DateUtc = Convert.ToDateTime(reader["DateUtc"]),
                    Rocket = reader["Rocket"].ToString()!
                });
            }
            return launches;
        }

        public async Task InsertLaunch(Launch launch)
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            var cmd = new SqlCommand(
                "INSERT INTO Launches (Id, Name, DateUtc, Rocket) VALUES (@id, @name, @dateUtc, @rocket)", conn);
            cmd.Parameters.AddWithValue("@id", launch.Id);
            cmd.Parameters.AddWithValue("@name", launch.Name);
            cmd.Parameters.AddWithValue("@dateUtc", launch.DateUtc);
            cmd.Parameters.AddWithValue("@rocket", launch.Rocket);
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
