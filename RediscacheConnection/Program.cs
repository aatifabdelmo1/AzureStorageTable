using StackExchange.Redis;
using System;

namespace RediscacheConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            IDatabase redisCache = GetConnection().GetDatabase();
            //redisCache.StringSet("test", "AbdElmonem");
            //string data = redisCache.StringGet("test");
            string data = redisCache.StringGet("Classes");
            Console.WriteLine(data);
            Console.ReadLine();
        }

        private static ConnectionMultiplexer GetConnection()
        {
            string connectionString = "SchoolCash.redis.cache.windows.net:6380,password=MivXteVH4bjvWmMguN7gZ6w4BbXHenBX19rX0cY9WUo=,ssl=True,abortConnect=False";
            return ConnectionMultiplexer.Connect(connectionString);
        }
    }
}
