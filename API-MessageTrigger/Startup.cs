﻿using API_MessageTrigger.Domain.Entities;
using API_MessageTrigger.Domain.Interfaces;
using API_MessageTrigger.Infra.CrossCutting;
using API_MessageTrigger.Infra.Data.Context;
using API_MessageTrigger.Infra.Data.Repository;
using API_MessageTrigger.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace API_MessageTrigger
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;
        private readonly string OpenCors = "_openCors";
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IServiceMessageTrigger, ServiceMessageTrigger>();
            services.AddTransient<IBaseRepository<MessageTrigger>, BaseRepository<MessageTrigger>>();
            services.AddTransient<IBaseService<MessageTrigger>, BaseService<MessageTrigger>>();
            services.AddTransient<IRequestEvolutionApi, RequestEvolutionApi>();
          
            //services.AddHttpClient();
            services.AddHttpClient("EvolutionAPI", client =>
            {
                client.BaseAddress = new Uri("http://localhost:8080");
            });
            services.AddEndpointsApiExplorer();
            services.AddCors(options =>
            {
                options.AddPolicy(name: OpenCors,
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.WithMethods("PUT", "DELETE", "GET", "POST");
                        builder.AllowAnyHeader();
                    });
            });
            services.AddSwaggerGen();
            services.AddDbContext<MessageTriggerContext>(options =>
            options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                     new MySqlServerVersion(new Version(8, 0, 23))));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(OpenCors);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
