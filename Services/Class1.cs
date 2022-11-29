using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Services
{
    public class Class1
    {
        //TEST
    }
}


//namespace tempConsole
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine("Hello World!!!");

//            var configuration = new ConfigurationBuilder()
//                            .SetBasePath(Directory.GetCurrentDirectory())
//                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//                            //.AddJsonFile($"appsettings.{env}.json", true, true)
//                            .AddEnvironmentVariables()
//                            .Build();

//            var serviceProvider = new Startup(configuration).ServiceProvider;
//            //var cardService = serviceProvider.GetRequiredService<CardProcessingService>();

//            var l = serviceProvider.GetService<ILogger<Program>>();
//            l.LogInformation("i am some information");


//            var s = serviceProvider.GetService<BcMooreService>();

//            var teams = s.GetTeamRecords().Result;
//            foreach (var t in teams)
//            {
//                Console.WriteLine(t);
//            }



//        }
//    }



//    public class Startup
//    {
//        public IConfiguration Configuration { get; }
//        public IServiceCollection ServiceCollection { get; }
//        public IServiceProvider ServiceProvider { get; }

//        public Startup(IConfiguration configuration)
//        {
//            this.Configuration = configuration;
//            this.ServiceCollection = new ServiceCollection();
//            this.ConfigureServices(this.ServiceCollection);
//            this.ServiceProvider = this.ServiceCollection.BuildServiceProvider();

//        }

//        private void ConfigureServices(IServiceCollection services)
//        {
//            services.AddLogging(logging =>
//            {
//                logging.ClearProviders();
//                logging.AddConfiguration(Configuration.GetSection("Logging"));
//                logging.AddConsole();
//                logging.AddDebug();
//            }
//            );



//            services.ConfigureDowlingCatholicServices(Configuration);

//            services.AddHttpClient<BcMooreService>();


//            //services.AddHttpClient<IWebApiClient, WebApiClient>();
//        }


//    }


//    public static class ServiceCollectionExtensionMethods
//    {
//        public static void ConfigureDowlingCatholicServices(this IServiceCollection services, IConfiguration configuration)
//        {
//            //services.ConfigureDowlingCatholicRepositories(configuration);
//            //services.AddTransient<IStudentService, StudentService>();
//            //services.AddTransient<IActivityAttendanceService, ActivityAttendanceService>();
//            //services.AddTransient<CardProcessingService, CardProcessingService>(); //TODO: What is the best way to do this?
//        }
//    }


//}