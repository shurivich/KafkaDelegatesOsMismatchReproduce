using System;
using Confluent.Kafka;

namespace KafkaDelegatesOsMismatchReproduce
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                Console.WriteLine("Succeed to build");
            }

            Console.WriteLine("We finished");
        }
    }
}
