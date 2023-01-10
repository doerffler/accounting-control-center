using DoePaAdmin.ViewModel.Services;
using DoePaAdmin.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using DoePaAdmin.ViewModel.Model;
using Microsoft.Extensions.Configuration;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.ConfigureAppConfiguration((context, builder) =>
{
    builder.AddJsonFile("appsettings.Development.json", true);
}).ConfigureServices((context, services) =>
{
    services.Configure<DoePaAdminConnectionSettings>(context.Configuration.GetSection(nameof(DoePaAdminConnectionSettings)));
    services.AddScoped<IDoePaAdminService, DoePaAdminService>();
    services.AddScoped<ReceiveMitarbeiterPerformanceViewModel>();
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
