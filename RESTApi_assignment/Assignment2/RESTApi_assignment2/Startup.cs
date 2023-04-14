using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RESTApi_assignment2.Helper;
using RESTApi_assignment2.Repository;
using RESTApi_assignment2.Repository.Interfaces;
using RESTApi_assignment2.Services;
using RESTApi_assignment2.Services.Interfaces;

namespace RESTApi_assignment2
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
            services.AddSingleton<IActorRepository, ActorRepository>();
            services.AddSingleton<IProducerRepository, ProducerRepository>();
            services.AddSingleton<IMovieRepository, MovieRepository>();
            services.AddSingleton<IGenreRepository, GenreRepository>();
            services.AddSingleton<IReviewRepository, ReviewRepository>(); 

            services.AddSingleton<IActorServices, ActorServices>();
            services.AddSingleton<IProducerServices, ProducerServices>();
            services.AddSingleton<IMovieServices, MovieServices>();
            services.AddSingleton<IGenreServices, GenreServices>();
            services.AddSingleton<IReviewServices, ReviewServices>();
            services.AddSingleton<IDataHelper , DataHelper>();
            services.AddAutoMapper(typeof(Startup));
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
