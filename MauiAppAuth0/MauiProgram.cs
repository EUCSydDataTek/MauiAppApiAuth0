using Auth0.OidcClient;
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
        builder.Services.AddSingleton<TokenHandler>();
        builder.Services.AddHttpClient<MainPage>(client => client.BaseAddress = new Uri("https://???.euw.devtunnels.ms"))
            .AddHttpMessageHandler<TokenHandler>();

        builder.Services.AddSingleton(new Auth0Client(new()
        {
            Domain = "eucsyd.eu.auth0.com",
            ClientId = "UEsF5peOE4UX6ItrIx4b6MZxBc7GVS20",
            RedirectUri = "myapp://callback/",
            PostLogoutRedirectUri = "myapp://callback/",
            Scope = "openid profile email"
        }));

        return builder.Build();
    }
}
