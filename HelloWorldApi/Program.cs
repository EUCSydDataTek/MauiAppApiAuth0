var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//builder.Services.AddAuthentication()
//    .AddJwtBearer((options =>
//    {
//        options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
//        options.Audience = builder.Configuration["Auth0:Audience"];
//    }));
builder.Services.AddAuthentication().AddJwtBearer();    // Benytter default configuration


//builder.Services.AddAuthorizationBuilder()
//  .AddPolicy("ReadPolicy", p => p.RequireAuthenticatedUser().RequireClaim("permissions", "read:numbers"))
//  .AddPolicy("WritePolicy", p => p.RequireAuthenticatedUser().RequireClaim("permissions", "write:numbers"));
builder.Services.AddAuthorization();  // Benyt kun default policy

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/messages/public", () => "The API doesn't require an access token to share this message.");

app.MapGet("/messages/protected", () => "The API successfully validated your access token.").RequireAuthorization();

app.MapGet("/messages/admin", () => "The API successfully recognized you as an admin.");

app.Run();
