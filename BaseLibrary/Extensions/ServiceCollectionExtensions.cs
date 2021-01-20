using BaseLibrary.Tool.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace BaseLibrary.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServiceLocator(
            this IServiceCollection services
        )
        {
            ServiceLocatorTools.Bind(services);
        }
    }
}