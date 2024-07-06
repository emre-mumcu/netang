﻿using System.Text.Json.Serialization;
using Backend.App_Data;
using Backend.App_Lib.Configuration.Services;

namespace Backend.App_Lib.Configuration.Extensions;

public static class ConfigureServices
{
    public async static Task<WebApplicationBuilder> _ConfigureServicesAsync(this WebApplicationBuilder builder)
    {
        builder.Services._InitServices();

        builder.Services._AddAppServices();

        builder.Services._AddDbContext<AppDbContext>();

        builder._AddLog4Net();

        return builder;
    }
}
