using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace OcelotGateway
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
            Action<IdentityServerAuthenticationOptions> isaOptMsg = o =>
            {
                o.Authority = IdentityServerConfig.Authority;
                o.ApiName = IdentityServerConfig.Api_Msg;//要连接的应用的名字
                o.RequireHttpsMetadata = false;
                o.SupportedTokens = SupportedTokens.Both;
                o.ApiSecret = IdentityServerConfig.Secret;//秘钥
            };
            Action<IdentityServerAuthenticationOptions> isaOptPhone = o =>
            {
                o.Authority = IdentityServerConfig.Authority;
                o.ApiName = IdentityServerConfig.Api_Phone;//要连接的应用的名字
                o.RequireHttpsMetadata = false;
                o.SupportedTokens = SupportedTokens.Both;
                o.ApiSecret = IdentityServerConfig.Secret;//秘钥         
            };


            services.AddAuthentication()
               //对配置文件中使用ChatKey配置了 AuthenticationProviderKey 的路由规则使用如下的验证方式
               .AddIdentityServerAuthentication(IdentityServerConfig.Api_Msg, isaOptMsg)
               .AddIdentityServerAuthentication(IdentityServerConfig.Api_Phone, isaOptPhone);

            // Ocelot
            services.AddOcelot();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseMvc(); -- no need MVC
            // Ocelot
            app.UseOcelot().Wait();
        }
    }
}
