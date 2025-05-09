﻿using Auth0.OidcClient;
using Microsoft.Extensions.Logging;

namespace MauiAppAuth0;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddSingleton(new Auth0Client(new()
        {
            Domain = "<YOUR_AUTH0_DOMAIN>",
            ClientId = "<YOUR_CLIENT_ID>",
            RedirectUri = "myapp://callback/",
            PostLogoutRedirectUri = "myapp://callback/",
            Scope = "openid profile email"
        }));

        return builder.Build();
    }
}
