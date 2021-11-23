using System;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PriceMonitor.Models;
using System.Net.Mail;

namespace PriceMonitor
{
    public class AddItem
    {
        private ILogger<AddItem> _log;
        public AddItem(ILogger<AddItem> logger)
        {
            this._log = logger;
        }
        [FunctionName("AddItem")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "item")] HttpRequest req)
        {
            _log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Item itemAdd = JsonConvert.DeserializeObject<Item>(requestBody);

            string responseMessage = "hello";

            return new OkObjectResult(responseMessage);
        }

        private bool IsValid(Item item)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string prefix = "Item property";
            string suffix = "was expected but received null or empty.\n";

            if(string.IsNullOrEmpty(item.Name))
            {
                stringBuilder.Append($"{prefix} name {suffix}");
            }

            if(item.Sku == default)
            {
                stringBuilder.Append($"{prefix} sku {suffix}");
            }

            if (item.TargetPrice == default)
            {
                stringBuilder.Append($"{prefix} targetPrice {suffix}");
            }

            if(string.IsNullOrEmpty(item.Email))
            {
                stringBuilder.Append($"{prefix} email {suffix}");
            }
            else
            {
                try
                {
                    MailAddress mail = new MailAddress(item.Email);
                }
                catch(FormatException)
                {
                    stringBuilder.Append($"Invalid email provided.");
                }
            }

            if(stringBuilder.Length == 0)
            {
                return true;
            }
            
            return false;
        }
    }
}
