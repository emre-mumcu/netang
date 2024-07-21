using Backend.App_Lib.Common;
using Backend.App_Lib.Configuration.Services;

namespace Backend.App_Lib.Configuration.Extensions;

// // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-8.0
public static class Configure
{    
    public async static Task<WebApplication> _ConfigureAsync(this WebApplication app)
    {
        AppConfig.Instance.WebHostEnvironment = app.Services.GetRequiredService<IWebHostEnvironment>();

        app._UseServices();

        app._UseAppServices();

        app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        app._UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();        

        app.MapGet("/", async (context) => await context.Response.WriteAsJsonAsync(ApiResponses.Success("Welcome to NetAng Service")));

        await Task.FromResult(0);

        return app;
    }
}


/*
Middleware Order
----------------
The order of middleware in ASP.NET Core is crucial because each middleware component in the pipeline can either handle the request and response directly or pass them to the next middleware in the pipeline. Here’s a general guideline for ordering middleware in an ASP.NET Core application:


1) Exception Handling Middleware:
UseDeveloperExceptionPage or UseExceptionHandler
This should be the first middleware to catch and handle exceptions.

2) HTTPS Redirection Middleware:
UseHttpsRedirection
Redirects HTTP requests to HTTPS.

3) HTTP Strict Transport Security (HSTS) Middleware:
UseHsts
Enforces strict transport security policies.

4) Static File Middleware:
UseStaticFiles
Serves static files and short-circuits the pipeline for static file requests.

5) Routing Middleware:
UseRouting
Adds route matching to the middleware pipeline.

6) CORS Middleware:
UseCors
Configures Cross-Origin Resource Sharing (CORS) policies.

7) Authentication Middleware:
UseAuthentication
Adds authentication to the request pipeline.

8) Authorization Middleware:
UseAuthorization
Adds authorization to the request pipeline.

9) Session Middleware:
UseSession
Enables session management.

10) Response Compression Middleware:
UseResponseCompression
Compresses the response.

11)Response Caching Middleware:
UseResponseCaching
Adds response caching.

12) Endpoints Middleware:
UseEndpoints
Maps endpoints for controllers, Razor Pages, and other request handlers.

Example Middleware Configuration
--------------------------------

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddRazorPages();
        services.AddCors();
        services.AddAuthentication();
        services.AddAuthorization();
        services.AddResponseCompression();
        services.AddResponseCaching();
        services.AddSession();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        
        app.UseRouting();

        app.UseCors(builder => 
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader());

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();

        app.UseResponseCompression();
        app.UseResponseCaching();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
        });
    }

The order in which middleware is added to the pipeline is critical because each middleware can pass the request to the next middleware.

Exception handling, HTTPS redirection, HSTS, and static files should be configured early in the pipeline.

Routing, CORS, authentication, and authorization should be configured after static files but before the endpoints.

Session, response compression, response caching, and endpoints should be configured towards the end.

*/