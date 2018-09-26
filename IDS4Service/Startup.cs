using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using IDS4Service.Service.Profiles;
using IDS4Service.Validator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IDS4Service
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
              // configure identity server with in-memory stores, keys, clients and scopes
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())// 身份资源
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())// api资源
                .AddInMemoryClients(IdentityServerConfig.GetClients())      // Client资源
                //.AddTestUsers(IdentityServerConfig.GetUsers())            // 测试用户数据
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()// 校验器
                .AddProfileService<ProfileService>();                       // 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
        }
    }
}
