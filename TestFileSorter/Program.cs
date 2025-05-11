using TestFileSorter.Core;
using Microsoft.Extensions.DependencyInjection;

namespace TestFileSorter
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            Application.Run(ServiceProvider.GetRequiredService<SortBigFileForm>());
        }

        public static void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ILargeFileSorter, LargeFileSorter>();
            serviceCollection.AddTransient<SortBigFileForm>();
        }
    }
}