using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using System.Net.Http;
using AzureFunctions.Extensions.Swashbuckle;
using System.Net;
using WidgetApi.FunctionHelpers;

namespace Sample2
{
    public static class Function1
    {
         
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]       
        [FunctionName("PostOrder")]
        public static async Task<IActionResult> PostOrder(
             [HttpTrigger(AuthorizationLevel.Anonymous,  "post", Route = "order")]
             [RequestBodyType(typeof(Order), "Order")]  Order order ,

             ILogger log)
        {
            var validator = new OrderValidator();
            var result=validator.Validate(order);

            log.LogInformation("C# HTTP trigger function processed a request.");


            
            //var order = JsonConvert.DeserializeObject<Order>(json);
            log.LogInformation("C# HTTP trigger function processed a request.");

            //string name = req.Query["name"];

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            //string responseMessage = string.IsNullOrEmpty(name)
            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //    : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(result);
        }


        [QueryStringParameter("test","test prop",DataType=typeof(Order))]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]

        [FunctionName("GetOrder")]
        public static async Task<IActionResult> GetOrder(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get",  Route = "order")] HttpRequest req,
             
            ILogger log)
        {
             
            //string name = req.Query["name"];

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            //string responseMessage = string.IsNullOrEmpty(name)
            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //    : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(new Order());
        }
    }
}
