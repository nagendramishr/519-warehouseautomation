using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

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
                SqlQuery = "SELECT * FROM c",
                Connection = "CosmosDBConnection")] IEnumerable<Shipment> shipmentsIn,
                ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return new JsonResult(shipmentsIn);     
        }
    }
}
