using System;
using Airport.API.Filters;
using Airport.BL;
using Airport.BL.Abstractions;
using Airport.BL.Services;
using Airport.DAL;
using Airport.DAL.Abstractions;
using Airport.DAL.EntityFramework;
using Airport.DAL.Models;
using Airport.DAL.Repositories.EntityFramework;
using Airport.DAL.Repositories.Seeds;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services
                .AddMvc(opt => opt.Filters.Add(typeof(ValidatorActionFilter)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
            ConfigureDependencyInjection(services);
        }

        private void ConfigureDependencyInjection(IServiceCollection services)
        {
            // ConfigureSeedDataSource(services); fake generated data
            ConfigureEfDataSource(services);
            
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IStewardessService, StewardessService>();
            services.AddScoped<IPilotService, PilotService>();
            services.AddScoped<IPlaneTypeService, PlaneTypeService>();
            services.AddScoped<ICrewService, CrewService>();
            services.AddScoped<IDepartureService, DepartureService>();
        }

        private void ConfigureEfDataSource(IServiceCollection services)
        {
            string connectionStr = Configuration.GetConnectionString("AirportDbString") ?? "Data Source=.\\SQLEXPRESS;Initial Catalog=AirportDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // hack for integration test
            services.AddDbContext<DataContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connectionStr, b => b.UseRowNumberForPaging()));
            services.AddScoped(typeof(DbContext), typeof(DataContext));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void ConfigureSeedDataSource(IServiceCollection services)
        {
            services.AddSingleton<IRepository<Flight>, FlightRepository>();  
            services.AddSingleton<IRepository<Crew>, CrewRepository>();  
            services.AddSingleton<IRepository<Departure>, DepartureRepository>();  
            services.AddSingleton<IRepository<Pilot>, PilotRepository>();  
            services.AddSingleton<IRepository<Stewardess>, StewardessRepository>();  
            services.AddSingleton<IRepository<Ticket>, TicketRepository>();  
            services.AddSingleton<IRepository<Plane>, PlaneRepository>();  
            services.AddSingleton<IRepository<PlaneType>, PlaneTypeRepository>();

            services.AddSingleton<IUnitOfWork, UnitOfWork>();
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
