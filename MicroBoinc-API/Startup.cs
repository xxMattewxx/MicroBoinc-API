using MicroBoincAPI.Authentication;
using MicroBoincAPI.Data;
using MicroBoincAPI.Data.Accounts;
using MicroBoincAPI.Data.Assignments;
using MicroBoincAPI.Data.Binaries;
using MicroBoincAPI.Data.Groups;
using MicroBoincAPI.Data.Leaderboards;
using MicroBoincAPI.Data.Platforms;
using MicroBoincAPI.Data.Projects;
using MicroBoincAPI.Data.Results;
using MicroBoincAPI.Data.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _environment;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (_environment.IsDevelopment())
                DotEnv.Load("debug.env");

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddCors();

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            //Auth
            services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                    options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                })
                .AddApiKeySupport(options => { });

            //Database and repositories
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(new NpgsqlConnectionStringBuilder()
                {
                    Host = Environment.GetEnvironmentVariable("DB_SERVER"),
                    Username = Environment.GetEnvironmentVariable("DB_USER"),
                    Password = Environment.GetEnvironmentVariable("DB_PASS"),
                    Database = Environment.GetEnvironmentVariable("DB_NAME"),
                    Pooling = true
                }.ConnectionString);
            });
            services.AddScoped<IAccountsRepo, AccountsRepo>();
            services.AddScoped<IAssignmentsRepo, AssignmentsRepo>();
            services.AddScoped<IBinariesRepo, BinariesRepo>();
            services.AddScoped<IGroupsRepo, GroupsRepo>();
            services.AddScoped<ILeaderboardsRepo, LeaderboardsRepo>();
            services.AddScoped<IPlatformsRepo, PlatformsRepo>();
            services.AddScoped<IProjectsRepo, ProjectsRepo>();
            services.AddScoped<IResultsRepo, ResultsRepo>();
            services.AddScoped<ITasksRepo, TasksRepo>();

            //APIs
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MicroBoincAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MicroBoincAPI v1"));
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
                builder.AllowAnyOrigin();
            });

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
