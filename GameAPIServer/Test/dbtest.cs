using System;
using System.Threading.Tasks;
using MySqlConnector;
using Microsoft.Extensions.Configuration;

namespace GameAPIServer
{
    public class DbTest
    {
        public static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var connStr = config["DbConfig:MasterDb"];
            try
            {
                using var conn = new MySqlConnection(connStr);
                await conn.OpenAsync();
                Console.WriteLine("DB 연결 성공!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DB 연결 실패: {ex.Message}");
            }
        }
    }
}