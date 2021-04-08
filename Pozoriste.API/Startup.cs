using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pozoriste.API.TokenServiceExtensions;
using Pozoriste.Data.Context;
using Pozoriste.Domain.Interfaces;
using Pozoriste.Domain.Services;
using Pozoriste.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pozoriste.API
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
            services.AddDbContext<TheatreContext>(options =>
            {
                options
                .UseSqlServer(Configuration.GetConnectionString("TheatreConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddControllers();

            services.AddOpenApi();

            // Repositories
            services.AddTransient<ISeatsRepository, SeatsRepository>();
            services.AddTransient<IReservationsRepository, ReservationsRepository>();
            services.AddTransient<IPiecesRepository, PiecesRepository>();
            services.AddTransient<IAuditoriumsRepository, AuditoriumsRepository>();
            services.AddTransient<IAddressesRepository, AddressesRepository>();
            services.AddTransient<IActorsRepository, ActorsRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IShowsRepository, ShowsRepository>();
            services.AddTransient<ITheatreRepository, TheatresRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();

            services.AddTransient<IReservationsRepository, ReservationsRepository>();
            // Services
            services.AddTransient<IPieceService, PieceService>();
            services.AddTransient<IShowService, ShowService>();
            services.AddTransient<IActorService, ActorService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<ITheatreService, TheatreService>();
            services.AddTransient<ISeatService, SeatService>();
            services.AddTransient<IAuditoriumService, AuditoriumService>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IReservationService, ReservationService>();

            // Allow Cors for client app
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
                    corsBuilder => corsBuilder.WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
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

            app.UseOpenApi();

            app.UseSwaggerUi3();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
