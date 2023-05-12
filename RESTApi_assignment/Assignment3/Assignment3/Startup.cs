using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Assignment3.Helper;
using Assignment3.Repository;
using Assignment3.Repository.Interfaces;
using Assignment3.Services;
using Assignment3.Services.Interfaces;
using Assignment3.Models.DbModels;
using Assignment3.CustomMiddleware;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System;

namespace Assignment3
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
            services.AddScoped<ActorRepository>();
            services.AddScoped<ProducerRepository>();
            services.AddScoped<GenreRepository>();
            services.AddScoped<ExceptionHandlingMiddleware>();
            services.AddScoped<MovieRepository>();
            services.AddScoped<ReviewRepository>(); 
            services.AddScoped<IActorServices, ActorServices>();
            services.AddScoped<IProducerServices, ProducerServices>();
            services.AddScoped<IMovieServices, MovieServices>();
            services.AddScoped<IGenreServices, GenreServices>();
            services.AddScoped<IReviewServices, ReviewServices>();
            services.AddScoped<IDataHelper , DataHelper>();
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
