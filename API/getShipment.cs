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
    public static class getShipment
    {
        [FunctionName("getShipment")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.System , "get", "post", Route = "getShipment/{shipperID:maxlength(50)}")] HttpRequest req,
            [CosmosDB(
                databaseName: "SP519as4",
                containerName: "shipments",
                SqlQuery = "SELECT * FROM c where c.ShipperID={shipperID}",
                Connection = "CosmosDBConnection")] IEnumerable<Shipment> shipmentsIn,
                string shipperID,
                ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Check for status codes


            return new JsonResult(shipmentsIn);     
        }
    }
}
