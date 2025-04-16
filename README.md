# MauiAppAuth0

Et tilpasset .NET 9 MAUI projekt med Auth0 authentication.

Ref: [Add Authentication to .NET MAUI Apps with Auth0](https://auth0.com/blog/add-authentication-to-dotnet-maui-apps-with-auth0/) (Benytter .NET 8)

Auth0-MAUI eksempel i GitHub: https://github.com/frederikprijck/auth0-maui (Benytter .NET 6))

Se ogs�: [Add Login to Your MAUI Application](https://auth0.com/docs/quickstart/native/maui/interactive)

# Windows
Her skal �ndres 2 ting i .csproj filen:

1. �ndre Windows-version til 10.0.20348.0:
```xml
<TargetFrameworks Condition="$([MSBuild]::IsOSPlatform('windows'))">$(TargetFrameworks);net9.0-windows10.0.20348.0</TargetFrameworks>
```

2. Tilf�j f�lgende: 
```xml
<WindowsPackageType>MSIX</WindowsPackageType>
```
[Convert an unpackaged .NET MAUI Windows app to packaged](https://learn.microsoft.com/en-us/dotnet/maui/windows/setup)