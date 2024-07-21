using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

namespace Backend.App_Lib.Configuration.Services;

public static class _Init
{
    public static IServiceCollection _InitServices(this IServiceCollection services)
    {
        // Action<Microsoft.AspNetCore.Mvc.JsonOptions> options = (opt) => { };

        // if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")

        services.AddControllers()
         // This is typically used when you want to customize the default JSON serialization behavior provided by System.Text.Json.
         // System.Text.Json is built into .NET Core, is generally faster, and has a smaller memory footprint compared to Newtonsoft.Json.
         // Effective if you return Ok(object), BadResult(object) or new JsonResult(data, settings); where settings are of type new JsonSerializerOptions
         .AddJsonOptions(options =>
         {
             options.JsonSerializerOptions.PropertyNamingPolicy = null;
             options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
             options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
             options.JsonSerializerOptions.WriteIndented = true;

         })
        // dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
        // Configures JSON serialization settings using the Newtonsoft.Json library (also known as Json.NET).
        // Effective if you return new JsonResult(data, settings); where settings are of type new JsonSerializerSettings
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.Formatting = Formatting.Indented;
            options.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
        })
        ;

        // Configure JSON serialization settings for the System.Text.Json serializer. 
        // It is used to configure JSON serialization for minimal APIs, HTTP extensions, and other components that use JSON serialization.
        // Effective if you return Results.Json(object);
        services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
        {
            options.SerializerOptions.PropertyNameCaseInsensitive = false;
            options.SerializerOptions.PropertyNamingPolicy = null;
            options.SerializerOptions.WriteIndented = true;
        });

        // To configure Cross-Origin Resource Sharing (CORS) policies. 
        // CORS is a security feature implemented by browsers to prevent web pages from making requests to a different domain than the one that served the web page.
        // services.AddCors(options =>
        // {
        //     options.AddPolicy("AppCorsPolicy", builder =>
        //     {
        //         builder.WithOrigins("https://localhost", "https://localhost")
        //             .AllowAnyHeader()
        //             .AllowAnyMethod()
        //             .AllowCredentials(); // Allows cookies/auth headers to be sent
        //     });
        // });

        /*
        CORS Policy Options: When configuring a CORS policy, you can specify several options:
        --------------------
        WithOrigins: Specifies the allowed origins.
        AllowAnyOrigin: Allows any origin to access the resource (not recommended for production).
        WithMethods: Specifies the allowed HTTP methods (GET, POST, PUT, DELETE, etc.).
        AllowAnyMethod: Allows any HTTP method.
        WithHeaders: Specifies the allowed headers.
        AllowAnyHeader: Allows any headers.
        AllowCredentials: Indicates whether the browser should send credentials (cookies, authorization headers) with the request.
        SetPreflightMaxAge: Specifies how long the results of a preflight request can be cached.
        */
        services.AddCors();

        return services;
    }

    public static IApplicationBuilder _UseServices(this WebApplication app)
    {
        /*
        Key Differences Between UseHsts and UseHttpsRedirection
        -------------------------------------------------------
        Primary Function:
        UseHsts: Instructs browsers to use HTTPS for future requests, enhancing security by preventing protocol downgrades.
        UseHttpsRedirection: Redirects current HTTP requests to HTTPS, ensuring secure communication immediately.

        Scope:
        UseHsts: Operates by setting headers that inform the browser about security policies.
        UseHttpsRedirection: Operates by modifying the behavior of the server to redirect requests.

        Implementation Environment:
        UseHsts: Typically used only in production environments due to the potential impact on development and testing.
        UseHttpsRedirection: Can be used in both development and production environments.

        Browser Behavior:
        UseHsts: Relies on the browser to remember and enforce the security policy.
        UseHttpsRedirection: Ensures secure communication immediately by redirecting the request on the server side.        
        */

        // HSTS is a security feature that tells browsers to always use HTTPS to access the site and never use HTTP. 
        // Enforces HTTP Strict Transport Security (HSTS) by instructing browsers to only communicate with the server over HTTPS.
        // Adds the Strict-Transport-Security header to responses.
        // This helps prevent man-in-the-middle attacks by ensuring that communications between the browser and the server are always encrypted.
        // Enhances security, Prevents downgrade attacks.
        // Not typically used in development environments.         
        if (app.Environment.IsProduction()) app.UseHsts();

        // Redirects HTTP requests to HTTPS, ensuring that clients always connect securely.
        // Uses a 307 Temporary Redirect or 308 Permanent Redirect status code for redirection.
        // Ensures that even if users or links attempt to access the site via HTTP, they are automatically redirected to HTTPS.
        // Used in both development and production environments to enforce HTTPS.
        // Ensures that all HTTP requests are automatically redirected to HTTPS.
        app.UseHttpsRedirection();

        return app;
    }
}


/*
public static class _Init
{
    public static IServiceCollection _InitServices(this IServiceCollection services) 
    {
        return services;
    }

    public static IApplicationBuilder _UseServices(this WebApplication app)
    {
        return app;
    }
}
*/