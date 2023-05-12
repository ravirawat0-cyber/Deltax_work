using IMDB.CustomMiddleware;
using IMDB.Helper;
using IMDB.Repository;
using IMDB.Repository.Interfaces;
using IMDB.Services;
using IMDB.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IMDB
{
    public class TestStartup
    {
        
        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IActorServices, ActorServices>();
            services.AddScoped<IProducerServices, ProducerServices>();
            services.AddScoped<IGenreServices, GenreServices>();
            services.AddScoped<IMovieServices, MovieServices>();
            services.AddScoped<ExceptionHandlingMiddleware>();
            services.AddScoped<IDataHelper, DataHelper>();
            // TODO: Register Mock Implementations
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

