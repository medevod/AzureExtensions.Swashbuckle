using AzureFunctions.Extensions.Swashbuckle;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Sample2;
using System.Reflection;



[assembly: WebJobsStartup(typeof(Startup))]
namespace Sample2
{
   
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // register other services here
            builder.AddSwashBuckle(Assembly.GetExecutingAssembly());
        }
    }
}