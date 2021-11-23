﻿namespace PriceMonitor.Common
{
    public static class Constants
    {
        public struct Cosmos
        {
            public const string DatabaseId = "pricecatalog";
            public const string CollectionId = "bestbuyprices";
            public const string PartitionKey = "/name";
        }

        public struct BestBuy
        {
            public const string ApiEndpoint = "";
        }
    }
}