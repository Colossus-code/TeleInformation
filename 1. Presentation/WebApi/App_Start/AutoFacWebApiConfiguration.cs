using Autofac;
using Autofac.Integration.WebApi;
using Contracts;
using Contracts.RepositoryContracts;
using Domain;
using Infrastructure;
using Infrastructure.RepositoryImplementations;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using System;
using System.IO;
using System.Reflection;
using System.Web.Http;


namespace WebApi.App_Start
{
    /// <summary>
    /// Registry class and dependenty injetcion 
    /// </summary>
    public class AutoFacWebApiConfiguration
    {
        public static IContainer Container { get; set; }

        private static string _logPathFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Log.txt");

        public static void Initialize(HttpConfiguration configuration)
        {
            Initialize(configuration, RegisterServices(new ContainerBuilder()));
        }
        private static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = (new AutofacWebApiDependencyResolver(container));
        }
        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(_logPathFile)
                .CreateLogger();

            builder.RegisterInstance(Log.Logger)
                .As<ILogger>()
                .SingleInstance();
            builder.Register(ctx => new MemoryCache(new MemoryCacheOptions()))
              .As<IMemoryCache>()
              .SingleInstance();

            //Dependency injection
            builder.RegisterType<PhoneInformationService>().As<IPhoneInformationService>();
            builder.RegisterType<RepositoryCache>().As<IRepositoryCache>();
            builder.RegisterType<RepositoryPhones>().As<IRepositoryPhones>();

            Container = builder.Build();

            return Container;
        }
    }
}