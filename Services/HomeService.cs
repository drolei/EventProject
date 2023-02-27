using EventProject.Controllers;
using EventProject.Core;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace EventProject.Services
{
    public class HomeService : IHomeService
    {
        private readonly ILogger<HomeService> _logger;
        public HomeService(ILogger<HomeService> logger)
        {
            _logger = logger;
        }
        public  string GetValuesFromLog()
        {
            string result = string.Empty;
            uint num = 0;
            

            DateTime dateTimeNow = DateTime.Now;
            DateTime dateTime1MinuteAgo = dateTimeNow.Add(new TimeSpan(0, -1, 0));
            string path = $@"{Directory.GetCurrentDirectory()}\logs\log-20230227.txt";

            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read,
                    FileShare.ReadWrite))
                {
                  using (StreamReader streamReader = new StreamReader(fileStream))
                  {
                    string line = string.Empty;

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (line.Contains(dateTime1MinuteAgo.ToString("yyyy-MM-ddTHH:mm")) &&
                                line.Contains("Some value:") )
                        {
                            
                            Regex regex = new Regex(@"Some value: \d+");
                            MatchCollection matches = regex.Matches(line);
                            if (matches.Count > 0)
                            {
                                foreach (Match match in matches)
                                {
                                  
                                    num += Convert.ToUInt32(match.Value.Replace("Some value: ", ""));


                                }
                            }
                           

                        }

                        if (line.Contains(dateTimeNow.ToString("yyyy-MM-ddTHH:mm")))
                        {
                            break;
                        }

                        
                    }                

                  }
                }

                _logger.LogInformation($"Sum values: {num} Time: {dateTimeNow.ToString("yyyy-MM-ddTHH:mm")}");

                result = $"Sum values: {num} Time: {dateTimeNow.ToString("MM/dd/yyyy hh:mm tt")}";
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error: {ex}");
            }

            


            return result;
        }
            
    }
}
