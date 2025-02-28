using DotNetEnv;
using Microsoft.Extensions.FileProviders;
using MonApi.API.Middleware;
using MonApi.Shared.Extensions;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.InjectDependencies();

var app = builder.Build();

app.UseCors("AllowReactApp");
app.UseCustomExceptionHandler();
app.UseApiKeyMiddleware();

// Configurer le pipeline HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Environment.CurrentDirectory, "Uploads")),
    RequestPath = "/Uploads"
});
//Enable directory browsing
app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Environment.CurrentDirectory, "Uploads")),
    RequestPath = "/Uploads"
});

// Activation de l'authentification et de l'autorisation
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();