using Microsoft.EntityFrameworkCore;
using Reboost;
using Reboost.Models;
using Reboost.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ReboostDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<BatteryService>();
builder.Services.AddScoped<CabinetService>();
builder.Services.AddScoped<RentService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<CabinetBatteryService>();

// Uso de Singleton para rodar continuamente
builder.Services.AddSingleton<TokenCleanupService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

// Garantir que o TokenCleanupService seja inicializado
app.Services.GetService<TokenCleanupService>();

app.Run();
