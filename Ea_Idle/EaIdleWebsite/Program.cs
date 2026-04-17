var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => Results.File("index.html", "text/html"));

app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();
