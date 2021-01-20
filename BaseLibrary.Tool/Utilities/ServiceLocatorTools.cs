using Microsoft.Extensions.DependencyInjection;

namespace BaseLibrary.Tool.Utilities
{
    public static class ServiceLocatorTools
    {
        private static IServiceCollection _services;

        /// <summary>
        /// Bind Service
        /// </summary>
        /// <param name="services"></param>
        public static void Bind(
            IServiceCollection services
        )
        {
            _services = services;
        }

        /// <summary>
        /// Get Service
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static TService GetService<TService>()
        {
            var provider = _services.BuildServiceProvider();
            return provider.GetRequiredService<TService>();
        }

        /// <summary>
        /// Get Service
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static TService GetService<TService>(
            this IServiceCollection services
        )
        {
            var provider = services.BuildServiceProvider();
            return provider.GetRequiredService<TService>();
        }
    }
}