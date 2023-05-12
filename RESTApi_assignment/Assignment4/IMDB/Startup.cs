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
            services.AddScoped<DbContext>();
            services.AddScoped<ExceptionHandlingMiddleware>();
            services.AddScoped<ReviewRepository>(); 
            services.AddScoped<IActorServices, ActorServices>();
            services.AddScoped<IProducerServices, ProducerServices>();
            services.AddScoped<IMovieServices, MovieServices>();
            services.AddScoped<IGenreServices, GenreServices>();
            services.AddScoped<IReviewServices, ReviewServices>();
            services.AddScoped<IDataHelper , DataHelper>();
            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IProducerRepository, ProducerRepository>();  
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();

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
          
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
