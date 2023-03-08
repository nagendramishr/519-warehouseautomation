using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace API
{
    public static class warehouseAPI
    {
        [FunctionName("warehouseAPI")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "SP519as4",
                containerName: "shipments",
                Connection   = "CosmosDBConnection")]
                IAsyncCollector<Shipment> shipmentsOut,
                ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return new JsonResult(shipmentsOut);
        }
    }
}
