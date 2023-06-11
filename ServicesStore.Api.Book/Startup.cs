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
using ServicesStore.Api.Book.Application;
using ServicesStore.Api.Book.Persistence;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesStore.Api.Book
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
            services.AddControllers().AddNewtonsoftJson().AddFluentValidation(cfg => {
                cfg.RegisterValidatorsFromAssemblyContaining<Creator>();
                ValidatorOptions.LanguageManager.Culture = new CultureInfo("en-US");
            }); 

            services.AddDbContext<BookShopContext>(opt => {
                opt.UseSqlServer(Configuration.GetConnectionString("BookShopDB"));
            });
            services.AddMediatR(typeof(Creator.Handler).Assembly);

            var config = new TypeAdapterConfig();
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
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
