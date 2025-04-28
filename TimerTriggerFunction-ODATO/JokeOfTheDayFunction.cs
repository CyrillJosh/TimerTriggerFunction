using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TimerTriggerFunction_ODATO.Context;

namespace TimerTriggerFunction_ODATO
{
    public class JokeOfTheDayFunction
    {
        private readonly ILogger _logger;
        private readonly MyDBContext _context;

        public JokeOfTheDayFunction(ILoggerFactory loggerFactory, MyDBContext context)
        {
            _logger = loggerFactory.CreateLogger<JokeOfTheDayFunction>();
            _context = context;
        }

        [Function("JokeOfTheDayFunction")]
        public async Task RunAsync([TimerTrigger("*/10 * * * * *")] MyInfo myTimer)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://official-joke-api.appspot.com/random_joke")
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(body);
                var bodyDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(body);
                _logger.LogInformation("--------------------------------------------------");
                _logger.LogInformation(bodyDict["setup"].ToString());
                _logger.LogInformation(bodyDict["punchline"].ToString());
                _logger.LogInformation("--------------------------------------------------");

                //Add Database and save the jokes
                
            }



        }

        public class MyInfo
        {
            public MyScheduleStatus ScheduleStatus { get; set; }

            public bool IsPastDue { get; set; }
        }

        public class MyScheduleStatus
        {
            public DateTime Last { get; set; }

            public DateTime Next { get; set; }

            public DateTime LastUpdated { get; set; }
        }
    }
}
