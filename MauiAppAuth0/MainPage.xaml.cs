using Auth0.OidcClient;

namespace MauiAppAuth0;

public partial class MainPage : ContentPage
{
    int count = 0;
    private readonly Auth0Client auth0Client;

    public MainPage(Auth0Client client)
    {
        InitializeComponent();
        auth0Client = client;
    }


    private async void OnLoginClicked(object sender, EventArgs e)
    {
        IdentityModel.OidcClient.LoginResult loginResult = await auth0Client.LoginAsync();

        if (!loginResult.IsError)
        {
            UsernameLbl.Text = loginResult.User.Identity!.Name;
            UserPictureImg.Source = loginResult.User
              .Claims.FirstOrDefault(c => c.Type == "picture")?.Value;

            LoginView.IsVisible = false;
            HomeView.IsVisible = true;
        }
        else
        {
            await DisplayAlert("Error", loginResult.ErrorDescription, "OK");
        }
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        IdentityModel.OidcClient.Browser.BrowserResultType logoutResult = await auth0Client.LogoutAsync();

        HomeView.IsVisible = false;
        LoginView.IsVisible = true;
    }
}
