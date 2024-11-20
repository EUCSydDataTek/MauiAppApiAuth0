using Auth0.OidcClient;

namespace MauiAppAuth0;

public partial class MainPage : ContentPage
{
    private readonly Auth0Client _auth0Client;
    private HttpClient _httpClient;

    public MainPage(Auth0Client client, HttpClient httpClient)
    {
        InitializeComponent();
        _auth0Client = client;
        _httpClient = httpClient;
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var loginResult = await _auth0Client.LoginAsync(new { audience = "https://todowebapi" });

        if (!loginResult.IsError)
        {
            UsernameLbl.Text = loginResult.User.Identity!.Name;
            UserPictureImg.Source = loginResult.User
              .Claims.FirstOrDefault(c => c.Type == "picture")?.Value;

            LoginView.IsVisible = false;
            HomeView.IsVisible = true;

            TokenHolder.AccessToken = loginResult.AccessToken;
        }
        else
        {
            await DisplayAlert("Error", loginResult.ErrorDescription, "OK");
        }
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        var logoutResult = await _auth0Client.LogoutAsync();

        HomeView.IsVisible = false;
        LoginView.IsVisible = true;
    }

    private async void OnApiCallClicked(object sender, EventArgs e)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync("messages/protected");
            {
                string content = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Info", content, "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}
