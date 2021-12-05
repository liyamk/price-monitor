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
using PriceMonitor.Services;

namespace PriceMonitor
{
    public class AddItem
    {
        private ILogger<AddItem> _log;
        private readonly ICosmosDbService _cosmosDbService;
        public AddItem(ILogger<AddItem> logger, ICosmosDbService cosmosDbService)
        {
            this._log = logger;
            this._cosmosDbService = cosmosDbService;
        }

        [FunctionName("AddItem")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "item")] HttpRequest req)
        {
            _log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Item itemAdd = JsonConvert.DeserializeObject<Item>(requestBody);
            StringBuilder builder = Validate(itemAdd);

            if(builder.Length > 0)
            {
                return new BadRequestObjectResult(HandleResponse("404", builder.ToString()));
            }

            try
            {
                _log.LogInformation(JsonConvert.SerializeObject(itemAdd));

                await AddItemAysnc(itemAdd);
                return new OkObjectResult(HandleResponse("200", "Success"));
            }
            catch(Exception ex)
            {
                string errorGuid = (Guid.NewGuid()).ToString();
                _log.LogError($"Encountered error - trace code {errorGuid} - {ex}");

                return new BadRequestObjectResult(HandleResponse("404", $"Encountered error - trace code {errorGuid}"));
            }

        }

        private async Task AddItemAysnc(Item item)
        {
            item.DocumentId = (Guid.NewGuid()).ToString();
            string date = DateTime.Now.ToString("o");
            item.CreatedDate = date;
            item.ModifiedDate = date;

            await _cosmosDbService.AddItemAsync(item);
        }

        private Response HandleResponse(string code, string message)
        {
            return new Response()
            { 
                ResponseCode = code,
                Message = message
            
            };

        }
        private StringBuilder Validate(Item item)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string prefix = "Item property";
            string suffix = "was expected but received null or empty. ";

            if(string.IsNullOrEmpty(item.Name))
            {
                stringBuilder.Append($"{prefix} 'name' {suffix}");
            }

            if(item.Sku == default)
            {
                stringBuilder.Append($"{prefix} 'sku' {suffix}");
            }

            if (item.TargetPrice == default)
            {
                stringBuilder.Append($"{prefix} 'targetPrice' {suffix}");
            }

            if(string.IsNullOrEmpty(item.Email))
            {
                stringBuilder.Append($"{prefix} 'email' {suffix}");
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

            return stringBuilder;
        }
    }
}
