using System;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace Indeed.Test.ServerTest
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        
        static void Main(string[] args)
        {
            int port = 44302, limitLeft = 10, limitRight = 20, amount = 0;

            void Exit(string message)
            {
                Console.Write(message);
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
            void GetInputData()
            {
                Console.Write("Введите номер порта: ");
                port = Convert.ToInt32(Console.ReadLine());
                Console.Write("Количество запросов: ");
                string _amount = Console.ReadLine();
                amount = string.IsNullOrEmpty(_amount) ? 0 : Convert.ToInt32(_amount);
                Console.Write("Введите нижнюю границу диапазона: ");
                limitLeft = Convert.ToInt32(Console.ReadLine());
                Console.Write("Введите верхнюю границу диапазона: ");
                limitRight = Convert.ToInt32(Console.ReadLine());
            }
            void SendRequest()
            {
                try
                {
                    var response = client.PostAsync($"https://localhost:{port}/api/requests/create", new StringContent("{\"name\": \"autotest\",\"createdBy\": \"ServerTest\"}", Encoding.UTF8, "application/json"));
                    Console.WriteLine(response.Result.StatusCode.ToString() == "OK" ? "Запрос успешно отправлен!" : "Запрос не отправлен!");
                    Random random = new Random();
                    Thread.Sleep(random.Next(limitLeft, limitRight) * 1000);
                }
                catch
                {
                    Exit("Нет ответа от сервера!");
                }
                
            }

            try
            {
                GetInputData();
            }
            catch {
                Exit("Ошибка при вводе данных!");
            }
            
            if (amount == 0)
            {
                while (true)
                {
                    SendRequest();
                }
            }
            else
            {
                while(amount > 0)
                {
                    SendRequest();
                    amount--;
                }
                Exit("Запросы отправлены!");
            }
        }


        
    }
}
