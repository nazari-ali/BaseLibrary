using BaseLibrary.Mongo.Models;
using BaseLibrary.Mongo.Models.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Sample.Mongo.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMongoDbSettings(
            this IServiceCollection services,
            IConfigurationSection configurationSection
        )
        {
            services.Configure<MongoDbSettings>(configurationSection);

            services.AddSingleton<IMongoDbSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
        }
    }
}