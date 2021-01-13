using BaseLibrary.Sql;
using BaseLibrary.Sql.Interfaces;
using BaseLibrary.Test.Models;
using BaseLibrary.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BaseLibrary.Test.Utilities
{
    [TestFixture]
    public class ServiceLocatorToolsTests
    {
        private IServiceCollection _serviceCollection;

        [SetUp]
        public void SetUp()
        {
            _serviceCollection = new ServiceCollection();

            _serviceCollection.AddDbContext<DbContext>(options =>
                options.UseInMemoryDatabase(databaseName: "FakeConnectionString")
            );

            _serviceCollection.AddSingleton(typeof(ISqlRepository<>), typeof(SqlRepository<>));
            _serviceCollection.AddTransient<ISqlUnitOfWork, SqlUnitOfWork>();
        }

        [Test]
        public void GetService_GetBaseRepositoryUsingServiceLocator_Successed()
        {
            // Arrange
            ServiceLocatorTools.Bind(_serviceCollection);

            // Act
            var result = ServiceLocatorTools.GetService<ISqlRepository<Artist>>();

            // Assert
            Assert.That(result, Is.TypeOf(typeof(SqlRepository<Artist>)));
        }

        [Test]
        public void GetService_GetBaseRepositoryUsingServiceLocatorExtensionMethod_Successed()
        {
            // Act
            var result = _serviceCollection.GetService<ISqlRepository<Artist>>();

            // Assert
            Assert.That(result, Is.TypeOf(typeof(SqlRepository<Artist>)));
        }
    }
}
