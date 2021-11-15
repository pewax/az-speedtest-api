using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using SpeedTestApi.Models;

namespace SpeedTestApi.Services
{
    public sealed class SpeedTestEvents : ISpeedTestEvents, IDisposable
    {
        private readonly EventHubProducerClient _client;

        public SpeedTestEvents(string connectionString, string entityPath)
        {
            _client = new EventHubProducerClient(connectionString, entityPath);
        }

        // Code continues here

        public void Dispose()
        {
            _client.CloseAsync();
        }

        public async Task PublishSpeedTest(TestResult speedTest)
        {
            var message = JsonSerializer.Serialize(speedTest);
            var data = new EventData(Encoding.UTF8.GetBytes(message));

            await _client.SendAsync(new[] { data });
        }
    }
}
