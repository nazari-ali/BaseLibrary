﻿using BaseLibrary.MongoDB;
using BaseLibrary.MongoDB.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Extensions
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
