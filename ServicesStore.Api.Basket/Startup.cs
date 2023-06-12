using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServicesStore.Api.Basket.Application;
using ServicesStore.Api.Basket.Persistence;
using ServicesStore.Api.Basket.RemoteInterface;
using ServicesStore.Api.Basket.RemoteServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.Basket
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
            services.AddScoped<IBooksService, BooksService>();

            services.AddDbContext<BasketContext>(opt => {
                opt.UseMySQL(Configuration.GetConnectionString("BasketDB"));
            });
            services.AddMediatR(typeof(Creator.Handler).Assembly);

            var config = new TypeAdapterConfig();
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            

            services.AddHttpClient("Books", config =>
            {

                config.BaseAddress = new Uri(Configuration["Services:Books"]);
            });

            services.AddControllers().AddNewtonsoftJson().AddFluentValidation(cfg => {
                cfg.RegisterValidatorsFromAssemblyContaining<Creator>();
                ValidatorOptions.LanguageManager.Culture = new CultureInfo("en-US");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
