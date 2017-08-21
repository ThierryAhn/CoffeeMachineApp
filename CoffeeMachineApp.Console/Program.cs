using System.Web.Http;
using System.Web.Http.SelfHost;

namespace CoffeeMachineApp.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration("http://http://localhost:59958/");

            config.Routes.MapHttpRoute(
                "API Default", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            using (HttpSelfHostServer server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                System.Console.WriteLine("Press Enter to quit.");
                System.Console.ReadLine();
            }
        }
    }
}
