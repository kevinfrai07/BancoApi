using Banco.Service;
using Banco.Infrastructure.Extension;

var builder = WebApplication.CreateBuilder(args);


var SpecificOrigins = "_SpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: SpecificOrigins,
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();  
        });
});
IConfigurationRoot configRoot = builder.Configuration;
IConfigurationBuilder root = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
configRoot = root.Build();

builder.Services.AddDbContext(builder.Configuration, configRoot);

builder.Services.AddScopedServices();

builder.Services.AddTransientServices();

builder.Services.AddServiceLayer();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
});
builder.Services.AddControllers();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseCors(SpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();