using CommunityEventPlanner.Contracts;
using CommunityEventPlanner.Contracts.DTO;
using System.Net.Http.Json;

namespace CommunityEventPlanner.UI.Services.Interface
{
    public class CommunityEventService : ICommunityEventService
    {
        private readonly HttpClient _httpClient;
        private readonly string _path;

        public CommunityEventService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _path = "CommunityEvent";
        }

        public async Task<IEnumerable<CommunityEvent>> GetCommunityEventsAsync()
        {
            var response = await _httpClient.GetAsync(_path);

            response.EnsureSuccessStatusCode();

            return (await response.Content.ReadFromJsonAsync<IEnumerable<CommunityEvent>>())!;
        }
    }
}