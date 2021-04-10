using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder().ConfigureWebHostDefaults(web => {
    web.Configure(builder => {
        builder.UseRouting();
        builder.UseEndpoints(endpoints => {
            endpoints.MapReverseProxy();
        });
    }).ConfigureServices((WebHostBuilder, services) => {
        var config = services.BuildServiceProvider().GetService<IConfiguration>();
        services.AddReverseProxy()
            .LoadFromConfig(config.GetSection("ReverseProxy"));
    });
}).Build().RunAsync();
