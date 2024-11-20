# MauiAppAuth0

Et tilpasset .NET 9 MAUI projekt med Auth0 authentication.

Ref: [Add Authentication to .NET MAUI Apps with Auth0](https://auth0.com/blog/add-authentication-to-dotnet-maui-apps-with-auth0/) (Benytter .NET 8)

Auth0-MAUI eksempel i GitHub: https://github.com/frederikprijck/auth0-maui (Benytter .NET 6))

Se ogs�: [Add Login to Your MAUI Application](https://auth0.com/docs/quickstart/native/maui/interactive)

### Nugets

- Microsoft.Extensions.Http

### Windows deployment
Windows Machine kr�ver at app'en k�rer som en Packed app, der benytter MsixPackage. Kontroll�r evt. i Properties | launchSettings.json.

[Convert an unpackaged .NET MAUI Windows app to packaged](https://learn.microsoft.com/en-us/dotnet/maui/windows/setup)

&nbsp;

# HelloWorldApi

Ref: [Call a Protected API from a .NET MAUI App](https://auth0.com/blog/call-protected-api-from-dotnet-maui-application/)

Benytter et Minimal Web API.

### Nugets

- Microsoft.AspNetCore.Authentication.JwtBearer

### Konfiguration
Foreg�r i appsettings.json, hvor man kan v�lge den simple udgave: 
```csharp
builder.Services.AddAuthentication().AddJwtBearer();
```

som l�ser en default konfiguration:

```json
"Authentication": {
    "Schemes": {
      "Bearer": {
        "ValidAudiences": [ "your-api-audience" ],
        "Authority": "https://your-auth0-domain",
        "ValidIssuer": "your-auth0-domain"
      }
    }
  }
```

eller den mere avancerede udgave:
```csharp
builder.Services.AddAuthentication()
    .AddJwtBearer((options =>
    {
        options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
        options.Audience = builder.Configuration["Auth0:Audience"];
    }));
 ```

hvor man selv angiver konfigurationen:

```json
{
  "Auth0": {
	"Domain": "your-auth0-domain",
	"Audience": "your-api-audience"
  }
}
```


### Debug
N�r der deployeres til Android Emulator skal WebApi deployes til DevTools. Husk at rette f�lgende steder:

- HelloWorldApi | Properties | launchSettings.json og `"applicationUrl": "https://???.euw.devtunnels.ms"`
- MauiAppAuth0 | MainProgram.cs og `builder.Services.AddHttpClient<MainPage>(client => client.BaseAddress = new Uri("https://???.euw.devtunnels.ms"));`