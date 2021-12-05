using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PriceMonitor.Models;

namespace PriceMonitor.Services
{
    public interface ICosmosDbService
    {
        Task AddItemAsync(Item item);

        Task<Item> GetItemAync(string id);

        Task<IEnumerable<Item>> GetItemsAysnc(string query);

        Task UpdateItemAysnc(Item item);

        Task DeleteItemAysnc(Item item);
    }
}
