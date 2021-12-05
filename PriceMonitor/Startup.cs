using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Cosmos;
using PriceMonitor.Common;
using PriceMonitor.Services;

[assembly: FunctionsStartup(typeof(PriceMonitor.Startup))]
namespace PriceMonitor
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

            builder.Services.AddHttpClient();

            builder.Services.AddSingleton<ICosmosDbService>((s) =>
            {
                string connString = Utility.GetEnvironmentValue(Constants.Cosmos.ConnStringKey);
                return new CosmosDbService(new CosmosClient(connString));
            });
        }
    }
}
