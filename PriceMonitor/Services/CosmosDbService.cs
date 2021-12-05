using PriceMonitor.Models;
using PriceMonitor.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using System.Linq;

namespace PriceMonitor.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(CosmosClient cosmosClient)
        {
            _container = cosmosClient.GetContainer(Constants.Cosmos.DatabaseId, Constants.Cosmos.CollectionId);
        }
        public async Task AddItemAsync(Item item)
        {
            await _container.CreateItemAsync<Item>(item);
        }

        public async Task DeleteItemAysnc(Item item)
        {
            await _container.DeleteItemAsync<Item>(item.DocumentId, new PartitionKey(item.Name));
        }

        public Task<Item> GetItemAync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Item>> GetItemsAysnc(string queryString)
        {
            FeedIterator<Item> query = _container.GetItemQueryIterator<Item>(new QueryDefinition(queryString));
            List<Item> results = new List<Item>();

            while (query.HasMoreResults)
            {
                FeedResponse<Item> response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAysnc(Item item)
        {
            await _container.UpsertItemAsync<Item>(item);
        }
    }
}
