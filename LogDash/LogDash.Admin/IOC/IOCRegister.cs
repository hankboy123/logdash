using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Framework.Platform.Core.IOC
{
    public  class IOCRegister
    {
        public IContainer ApplicationContainer { get;  set; }
        /// <summary>
        /// 注入接口必须继承于IDependency的接口（例如：IUserService:IDependency 实现类名为UserService）
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyNames">当前包含的程序集名称</param>
        /// <returns></returns>
        public AutofacServiceProvider GetAutofacServiceProvider(IServiceCollection services, params string[] assemblyNames)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.Populate(services);
        
            var assemblies = new List<Assembly>();
            foreach (var item in assemblyNames)
            {
                assemblies.Add(Assembly.Load(new AssemblyName(item)));
            }
            //assemblies.Add(Assembly.Load(new AssemblyName("Project.Respository.Collection")));
            //assemblies.Add(Assembly.Load(new AssemblyName("Project.Service.Collection")));
            builder.RegisterAssemblyTypes(assemblies.ToArray())
            .Where(type =>
            typeof(IDependency).IsAssignableFrom(type) && !type.GetTypeInfo().IsAbstract)
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
            return new AutofacServiceProvider(builder.Build());
        }

    }
}
