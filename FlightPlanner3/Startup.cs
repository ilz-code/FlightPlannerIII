using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlanner.Services;
using FlightPlanner.Services.Validators;
using FlightPlanner3.Controllers.Authentication;
using FlightPlanner3.Mappings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner3
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FlightPlannerWeb", Version = "v1" });
            });
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            services.AddDbContext<FlightPlannerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("flight-planner")));
            services.AddScoped<IFlightPlannerDBContext, FlightPlannerDbContext>();
            services.AddScoped<IDbService, DbService>();
            services.AddScoped<IEntityService<Flight>, EntityService<Flight>>();
            services.AddScoped<IEntityService<Airport>, EntityService<Airport>>();
            services.AddScoped<IDbServiceExtended, DbServiceExtended>();
            var cfg = AutoMapperConfiguration.GetConfig();
            services.AddSingleton(typeof(IMapper), cfg);
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IFlightServiceSearch, FlightServiceSearch>();
            services.AddScoped<IAirportService, AirportService>();
            services.AddScoped<IValidator, AirportCodesEqualityValidator>();
            services.AddScoped<IValidator, AirportCodeValidator>();
            services.AddScoped<IValidator, ArrivalTimeValidator>();
            services.AddScoped<IValidator, CarrierValidator>();
            services.AddScoped<IValidator, CityValidator>();
            services.AddScoped<IValidator, CountryValidator>();
            services.AddScoped<IValidator, DepartureTimeValidator>();
            services.AddScoped<IValidator, TimeFrameValidator>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlightPlannerWeb v1"));
            }

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
