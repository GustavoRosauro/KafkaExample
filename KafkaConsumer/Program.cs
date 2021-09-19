using Confluent.Kafka;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "quickstart-events"
            };
            Task.Run(() => KafkaConsumer(config));
            Console.WriteLine("finalizado a execução");
            Console.ReadKey();
        }
        private static void KafkaConsumer(ConsumerConfig config)
        {
            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe("quickstart-events");
                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true;
                    cts.Cancel();
                };
                while (true)
                {
                    var cr = consumer.Consume(cts.Token);
                    Console.WriteLine($"Consumed message '{cr.Message.Value}' at '{cr.TopicPartitionOffset}'");
                }
            }
        }
    }
}
