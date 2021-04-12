using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using Microsoft.AspNetCore.Http;
using System.Net;

namespace ReverseProxy
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
            services.AddReverseProxy().LoadFromConfig(Configuration.GetSection("ProxyReverso"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            /*
            app.Use(async (ctx, next) =>
            {
                if (ctx.Request.Headers.Any(x => x.Key.Equals("api-key")))
                {
                    var authHeader = ctx.Request.Headers["api-key"];
                    if (authHeader.Equals("reverse-proxy"))
                    {
                        await next.Invoke();
                    }else{
                        ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        await ctx.Response.WriteAsync("invalid apikey");
                    }                   
                }else{
                    ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await ctx.Response.WriteAsync("no apikey present");
                }                               
            });*/

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapReverseProxy();
            });
        }
    }
}
