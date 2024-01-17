﻿using API_MessageTrigger.Domain.Entities;
using API_MessageTrigger.Domain.Interfaces;
using API_MessageTrigger.Infra.CrossCutting;
using API_MessageTrigger.Infra.Data.Context;
using API_MessageTrigger.Infra.Data.Repository;
using API_MessageTrigger.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace API_MessageTrigger
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            IServiceCollection serviceCollection = services.AddDbContext<MessageTriggerContext>(options =>
            {
                services.AddDbContext<MessageTriggerContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            });

            services.AddTransient<IServiceMessageTrigger, ServiceMessageTrigger>();
            services.AddTransient<IBaseRepository<MessageTrigger>, BaseRepository<MessageTrigger>>();
            services.AddTransient<IBaseService<MessageTrigger>, BaseService<MessageTrigger>>();
            services.AddTransient<IRequestEvolutionApi, RequestEvolutionApi>();
            services.AddHttpClient();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}