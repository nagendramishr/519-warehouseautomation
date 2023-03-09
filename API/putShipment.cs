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
using Newtonsoft.Json;
using System.IO;
using System;

namespace API
{
    public static class putShipment
    {
        [FunctionName("putShipment")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "putShipment/{shipperID:maxlength(50)}")] HttpRequest req,
            [CosmosDB(
                databaseName: "SP519as4",
                containerName: "shipments",
                Connection = "CosmosDBConnection")]IAsyncCollector<Shipment> shipmentsOut,
                string shipperID,
                ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string ShipmentID  = data?.ShipmentID;
            string WarehouseID = data?.WarehouseID;
            string ShippingPO  = data?.ShippingPO;
            string BoxesRcvd   = data?.BoxesRcvd;
            //string ShipperID   = data?.ShipperID;
            System.DateTime Date        = data?.Date;

            try {
                var s = new Shipment() {ShipmentID=ShipmentID,
                WarehouseID=WarehouseID, ShippingPO=ShippingPO,
                BoxesRcvd=BoxesRcvd, ShipperID=shipperID, Date=Date};

                await shipmentsOut.AddAsync(s);
                return new JsonResult(s);
            }
            catch (Exception e) {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
