using Microsoft.Extensions.DependencyInjection;
using TestFileGenerator.Core;

namespace TestFileGenerator
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
            ApplicationConfiguration.Initialize();

           var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            Application.Run(ServiceProvider.GetRequiredService<GenerateTestFileForm>());
            
        }

        public static void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ILargeFileGenerator, LargeFileGenerator>();
            serviceCollection.AddTransient<GenerateTestFileForm>();
        }
    }
}