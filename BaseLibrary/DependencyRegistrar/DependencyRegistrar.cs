using BaseLibrary.DependencyRegistrar.Interfaces;
using BaseLibrary.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BaseLibrary.DependencyRegistrar
{
    public static class DependencyRegistrar
    {
        /// <summary>
        /// Register all classes inherited from ITransientDependency as Transient, ITransientDependency as Scoped, ISingletonDependency as singleton in CURRENT assembly
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static async Task<IServiceCollection> RegisterDependencies(this IServiceCollection services)
        {
            //Register all types... from CURRENT assembly
            return await services.RegisterDependencies(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Register all classes inherited from ITransientDependency as Transient, ITransientDependency as Scoped, ISingletonDependency as singleton
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static async Task<IServiceCollection> RegisterDependencies(this IServiceCollection services, params Assembly[] assemblies)
        {
            //Load all types (public class) of this services from assemblies
            List<Type> types = await assemblies.SelectMany(p => p.ExportedTypes)
                .Where(p => p.IsPublic && p.IsClass).ToListAsync();

            foreach (Type type in types)
            {
                if (type.IsInheritFrom<ITransientDependency>())
                {
                    services.RegisterAsSelfAndInterfaces(type, ServiceLifetime.Transient);
                }

                if (type.IsInheritFrom<IScopedDependency>())
                {
                    services.RegisterAsSelfAndInterfaces(type, ServiceLifetime.Scoped);
                }

                if (type.IsInheritFrom<ISingletonDependency>())
                {
                    services.RegisterAsSelfAndInterfaces(type, ServiceLifetime.Singleton);
                }
            }

            return services;
        }

        /// <summary>
        /// Register this Services as all interfaces that inherited
        /// </summary>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="service"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAsSelfAndInterfaces<TImplementation>(this IServiceCollection service, ServiceLifetime lifetime)
        {
            return service.RegisterAsSelfAndInterfaces(typeof(TImplementation), lifetime);
        }

        /// <summary>
        /// Register this Services as all interfaces that inherited
        /// </summary>
        /// <param name="service"></param>
        /// <param name="implementationType"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAsSelfAndInterfaces(this IServiceCollection service, Type implementationType, ServiceLifetime lifetime)
        {
            if (implementationType.IsAbstract || implementationType.IsInterface)
            {
                return service;
            }

            service.Add(new ServiceDescriptor(implementationType, implementationType, lifetime));

            var interfaces = implementationType.GetInterfaces()
                .Where(interfaceType => interfaceType != typeof(ITransientDependency));// && !interfaceType.Name.Contains("IRepository"));

            foreach (Type serviceType in interfaces)
            {
                service.Add(new ServiceDescriptor(serviceType, implementationType, lifetime));
            }

            return service;
        }
    }
}
