using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport.API.Filters;
using Airport.BL;
using Airport.BL.Abstractions;
using Airport.BL.Services;
using Airport.DAL;
using Airport.DAL.Abstractions;
using Airport.DAL.Models;
using Airport.DAL.Repositories.Seeds;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Airport.API
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
            services.AddMvc(opt => opt.Filters.Add(typeof(ValidatorActionFilter))).SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddFluentValidation();
            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
            ConfigureDependencyInjection(services);
        }

        private static void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddSingleton<IRepository<Flight>, FlightRepository>(); //TODO: rewrite to scoped when DB is present
            services.AddSingleton<IRepository<Crew>, CrewRepository>(); //TODO: rewrite to scoped when DB is present
            services.AddSingleton<IRepository<Departure>, DepartureRepository>(); //TODO: rewrite to scoped when DB is present
            services.AddSingleton<IRepository<Pilot>, PilotRepository>(); //TODO: rewrite to scoped when DB is present
            services.AddSingleton<IRepository<Stewardess>, StewardessRepository>(); //TODO: rewrite to scoped when DB is present
            services.AddSingleton<IRepository<Ticket>, TicketRepository>(); //TODO: rewrite to scoped when DB is present
            services.AddSingleton<IRepository<Plane>, PlaneRepository>(); //TODO: rewrite to scoped when DB is present
            services.AddSingleton<IRepository<PlaneType>, PlaneTypeRepository>(); //TODO: rewrite to scoped when DB is present

            services.AddSingleton<IUnitOfWork, SeedUnitOfWork>();
            services.AddScoped<IFlightService, FlightService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
