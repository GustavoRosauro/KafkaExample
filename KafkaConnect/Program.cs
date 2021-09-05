using Confluent.Kafka;
using System;
using System.Net;
using System.Threading.Tasks;

namespace KafkaConnect
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"                
            };
            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                for (int i = 0; i < 10; i++)
                {
                    await producer.ProduceAsync("quickstart-events", new Message<Null, string> { Value = $"Gustavo {i}" });
                }
            }
        }
    }
}
