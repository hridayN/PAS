using Microsoft.EntityFrameworkCore;
using PAS.API.Constants;
using PAS.API.Infrastructure;
using PAS.API.Infrastructure.Contracts;
using PAS.API.Infrastructure.Repositories.Base;
using PAS.API.Services.Contract;
using PAS.API.Services.Core;
using PAS.API.Settings;
using PAS.API.Utilites;
using PAS.API.Utilites.APIExtension;

var builder = WebApplication.CreateBuilder(args);
builder.Services.SetUpApi();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDbContext<PASDbContext>(ServiceLifetime.Scoped);
builder.Services.SetUpDatabase<PASDbContext>(builder.Configuration, DbConstant.MigrationTable, DbConstant.PolicyAdministrationSystemSchema);
builder.Services.Configure<PasOptions>(builder.Configuration.GetSection(PasOptions.PolicyAdministration));
PasOptions schemaOptions = new PasOptions();
builder.Configuration.Bind(PasOptions.PolicyAdministration, schemaOptions);
builder.Services.AddSingleton(schemaOptions);

// Add services to the container.
builder.Services.AddScoped<ICodeListService, CodeListService>();
builder.Services.AddScoped<IExpressionFilter, ExpressionFilter>();
builder.Services.AddScoped<IRepositoryService, RepositoryService>();
// Add Infrastructure layer
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//app.UsePathBase("/passervice");
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<PASDbContext>();
    // context.Database.EnsureDeleted();
    context.Database.Migrate();
}
// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

// app.UseAuthorization();
app.ConfigureApi();
app.Run();
