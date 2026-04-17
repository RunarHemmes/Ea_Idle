using MyAPI.IoC;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepositories();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
