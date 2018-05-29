using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Platform.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Common;
using Exceptionless;

namespace LogDash.Admin
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}
        public Startup(IHostingEnvironment env)
        {
            //这里创建ConfigurationBuilder，其作用就是加载Congfig等配置文件
            var builder = new ConfigurationBuilder()
            //env.ContentRootPath：获取当前项目的跟路径
            .SetBasePath(env.ContentRootPath)
            ////这里关注的是$"{param}"的这种写法，有点类似于string.Format()
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();
            var IControllerType = typeof(ControllerBase);

            Configuration = builder.Build();

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(
               opt =>
               {
                   opt.Filters.Add(new ErrorHandlingFilter());
               }
           );
            //IOCRegister ioc = new IOCRegister();
            //services.AddAutofac();
            services.AddSingleton<IMemoryCache, MemoryCache>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure<Appsettings>(Configuration);
            //services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider svp)
        {
            MyHttpContext.ServiceProvider = svp;
            //使用Exceptionles网络日志系统
            app.UseExceptionless(Configuration);
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
