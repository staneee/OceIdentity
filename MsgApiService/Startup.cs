using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MsgApiService
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            // 如果不需要获取当前登陆用户信息或者复杂校验权限，那么不需要这些，配置 Ocelot 网关处进行校验
            //services.AddMvcCore()
            //    .AddAuthorization()
            //    .AddJsonFormatters();

            //services.AddAuthentication(IdentityServerConfig.Scheme)
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.ApiName = IdentityServerConfig.Api_Msg;

            //        options.Authority = IdentityServerConfig.Authority;
            //        options.RequireHttpsMetadata = false;
            //    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // 如果不需要获取当前登陆用户信息或者复杂校验权限，那么不需要这些，配置 Ocelot 网关处进行校验
            //app.UseAuthentication();

            app.UseMvc();
        }
    }
}
