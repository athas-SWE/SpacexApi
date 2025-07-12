using WebApplicatiofvv.Data;
using WebApplicatiofvv.Model;

namespace WebApplicatiofvv.Services
{
    public class LaunchService
    {
        private readonly HttpClient _httpClient;
        private readonly DatabaseHelper _db;

        public LaunchService(HttpClient httpClient, DatabaseHelper db)
        {
            _httpClient = httpClient;
            _db = db;
        }

       
        public async Task<Launch?> GetLaunchById(string id)
        {
            try {
                var cached = await _db.GetById(id);
                if (cached != null) return cached;

                var response = await _httpClient.GetAsync($"https://api.spacexdata.com/v5/launches/{id}");
                if (!response.IsSuccessStatusCode) return null;

                var external = await response.Content.ReadFromJsonAsync<ExternalLaunch>();
                if (external == null) return null;

                var launch = new Launch
                {
                    Id = external.id,
                    Name = external.name,
                    DateUtc = external.date_utc,
                    Rocket = external.rocket
                };

                await _db.InsertLaunch(launch);
                return launch;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Service error while fetching launch ID {id}: {ex.Message}");
            }
        }

        public Task<List<Launch>> GetAll() => _db.GetAll();

        private class ExternalLaunch
        {
            public string id { get; set; }
            public string name { get; set; }
            public DateTime date_utc { get; set; }
            public string rocket { get; set; }
        }
    }
}
