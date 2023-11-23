using Etehadie.Registration;
using Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterServices(builder.Configuration);

var app = builder.Build();

app.SeedDatabases();

app.UseCustomMiddlewares();

app.Run();
