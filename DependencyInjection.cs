﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sabio.Data;
using Sabio.Models.Domain;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Services.Schedules;
using Sabio.Web.Api.StartUp.DependencyInjection;
using Sabio.Web.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sabio.Web.StartUp
{
    public class DependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration is IConfigurationRoot)
            {
                services.AddSingleton<IConfigurationRoot>(configuration as IConfigurationRoot);   // IConfigurationRoot
            }

            services.AddSingleton<IConfiguration>(configuration);   // IConfiguration explicitly

            string connString = configuration.GetConnectionString("Default");
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2
            // The are a number of differe Add* methods you can use. Please verify which one you
            // should be using services.AddScoped<IMyDependency, MyDependency>();

            // services.AddTransient<IOperationTransient, Operation>();

            // services.AddScoped<IOperationScoped, Operation>();

            // services.AddSingleton<IOperationSingleton, Operation>();

            services.AddSingleton<IAuthenticationService<int>, WebAuthenticationService>();

            services.AddSingleton<Sabio.Data.Providers.IDataProvider, SqlDataProvider>(delegate (IServiceProvider provider)
            {
                return new SqlDataProvider(connString);
            }
            );

            services.AddSingleton<IAdvertisementsService, AdvertisementService>();

            services.AddSingleton<IBusinessProfilesServices, BusinessProfilesServices>();

            services.AddSingleton<IBlogService, BlogService>();
            services.AddSingleton<IBusinessTypeService, BusinessTypeService>();
            services.AddSingleton<ICommentsService, CommentsService>();

            services.AddSingleton<IEmailService, EmailService>();

            services.AddSingleton<IEventsService, EventsService>();

            services.AddSingleton<IFaqService, FaqService>();

            services.AddSingleton<IFileService, FileService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IInventoryService, InventoryService>();

            services.AddSingleton<ILocationService, LocationService>();

            services.AddSingleton<IMessagesAdminService, MessagesService>();

            services.AddSingleton<IMessagesService, MessagesService>();

            services.AddSingleton<IPrivacyPolicyService, PrivacyPolicyService>();

            services.AddSingleton<IProductService, ProductService>();

            services.AddSingleton<IRatingsService, RatingsService>();

            services.AddSingleton<ISchedulesService, SchedulesService>();

            services.AddSingleton<IStatesServices, StatesServices>();
            services.AddSingleton<IShoppingCartService, ShoppingCartService>();

            services.AddSingleton<IScheduleAvailabilityService, ScheduleAvailabilityService>();

            services.AddSingleton<IUserDashboardService, UserDashboardService>();

            services.AddSingleton<IUserProfilesServices, UserProfilesService>();

            services.AddSingleton<IUserService, UserService>();

            services.AddSingleton<IVendorService, VendorService>();

            services.AddSingleton<IVendorTypeService, VendorTypeService>();

            services.AddSingleton<IMapVendors, VendorService>();

            services.AddSingleton<IIdentityProvider<int>, WebAuthenticationService>();


            GetAllEntities().ForEach(tt =>
            {
                IConfigureDI d = Activator.CreateInstance(tt) as IConfigureDI;

                //This will not error by way of being null. BUT if the code within the method does
                // then we would rather have the error loadly on startup then worry about debuging the issues as it runs
                d.ConfigureServices(services, configuration);
            });
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }

        public static List<Type> GetAllEntities()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                 .Where(x => typeof(IConfigureDI).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                 .ToList();
        }
    }
} 