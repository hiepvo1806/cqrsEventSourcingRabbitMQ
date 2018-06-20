using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using InstratructureLayer;
using ApplicationLayer;
using AutoMapper;
using System.Reflection;
using CommandStack;
using ReadStack;
using CommandStack.EventSourceManagement;
using ApplicationLayer.Services;
using InstratructureLayer.Repositories;
using InstratructureLayer.EventSourcing.Bus;
using InstratructureLayer.Bus;
using MediatR;
using CommandStack.CommandHandlers;
using CommandStack.Commands;
using InstratructureLayer.MessageService;
using CommandStack.FoodStoreEvent;
using CommandStack.FoodStoreEventHandler;

namespace PresentationLayer
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public MapperConfiguration MapperConfiguration { get; set; }
        public IConfiguration Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            _hostingEnvironment = env;
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(options => {
                Environment.SetEnvironmentVariable("EnvironmentName", _hostingEnvironment.EnvironmentName);
                Environment.SetEnvironmentVariable("ContentRootPath", _hostingEnvironment.ContentRootPath);
                options.UseSqlServer(connectionString);
            });
            
            services.AddScoped<DbContext, DataContext>();
            services.AddAutoMapper();
            ConfigureAutoMapper(services);
            services.AddMvc();
            services.AddMediatR(typeof(Startup));
            RegisterServices(services);

        }

        public void RegisterServices(IServiceCollection services)
        {
            //Intrastructure
            services.AddScoped(typeof(IRepository<,>),typeof(Repository<,>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddSingleton<IMessageNotify<string>, MessageNotify>();

            //Application
            services.AddScoped<IFoodStoreApplicationService, FoodStoreApplicationService>();
            
            //ReadStack
            services.AddScoped(typeof(IReadService<,>), typeof(BaseReadService<,>));
            services.AddScoped<IFoodStoreEventSourceManager, FoodStoreEventSourceManager>();
            services.AddScoped<IFoodStoreRepository, FoodStoreRepository>();
            
            //CommandStack
            services.AddScoped<IRequestHandler<CreateFoodStoreCommand, Unit>, CreateFoodStoreHandler>();
            services.AddScoped<IRequestHandler<UpdateFoodStoreCommand, Unit>, UpdateFoodStoreHandler>();
            services.AddScoped<IRequestHandler<DeleteFoodStoreCommand, Unit>, DeletedFoodStoreHandler>();

            services.AddScoped<INotificationHandler<StoreCreatedEvent>, FoodStoreEventHandler>();
            services.AddScoped<INotificationHandler<StoreUpdatedEvent>, FoodStoreEventHandler>();
            services.AddScoped<INotificationHandler<StoreDeletedEvent>, FoodStoreEventHandler>();
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
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseStaticFiles();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});

            app.MapWhen(
                context => context.Request.Path.Value.ToLower().StartsWith("/api"),
                builder => builder.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
                }));

            // // Server route for else
            app.MapWhen(
                context => !context.Request.Path.Value.ToLower().StartsWith("/api"),
                builder => builder.UseMvc(routes =>
                {
                    routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
                    routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });
                })
                );

        }

        private void ConfigureAutoMapper(IServiceCollection services)
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(
                    GetType().GetTypeInfo().Assembly,
                     typeof(ApplicationMapperProfile).GetTypeInfo().Assembly,
                     typeof(CommandStackMapperProfile).GetTypeInfo().Assembly,
                     typeof(IntratructureMapperProfile).GetTypeInfo().Assembly
                     );
                  
            });

            services.AddSingleton(sp => MapperConfiguration.CreateMapper());
        }


    }
}
