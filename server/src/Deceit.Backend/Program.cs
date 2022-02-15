using Deceit.Backend.Hubs;
using Deceit.Domain.Lobbies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<LobbyService>();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(builder =>
    builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins("http://localhost:3000")
);

app.MapHub<GameLobbyHub>("/gamelobby");
app.MapRazorPages();

app.MapPost("/lobby", (LobbyService lobbyService) =>
{
    Lobby lobby = new(Guid.NewGuid().ToString());
    lobbyService.AddLobby(lobby);
    return lobby;
});

app.Run();
