using BaseLibrary.MongoDB;
using BaseLibrary.MongoDB.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Sample.Mongo.Extensions;
using Sample.Sql.Persistence;

namespace Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample", Version = "v1" });
            });

            // Mongo Dependency
            services.AddMongoDbSettings(Configuration.GetSection("MongoDbSettings"));
            services.AddSingleton<IMongoContext, MongoContext>();
            services.AddTransient<Mongo.UnitOfWork.IUnitOfWork, Mongo.UnitOfWork.UnitOfWork>();

            // Sql Dependency
            services.AddDbContextPool<AppDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("SqlServer"))
            );
            services.AddTransient<Sql.UnitOfWork.IUnitOfWork, Sql.UnitOfWork.UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
