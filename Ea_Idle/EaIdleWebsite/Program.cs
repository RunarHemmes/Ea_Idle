var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Make sure the browser doesn't get a 404 on page refresh:
app.MapGet("/", () => Results.File("index.html", "text/html"));
app.MapGet("/Mining", () => Results.File("index.html", "text/html"));
app.MapGet("/Tharni", () => Results.File("index.html", "text/html"));
app.MapGet("/Castar", () => Results.File("index.html", "text/html"));
app.MapGet("/Silmaril", () => Results.File("index.html", "text/html"));
app.MapGet("/Help", () => Results.File("index.html", "text/html"));
app.MapGet("/Settings", () => Results.File("index.html", "text/html"));
app.MapGet("/Credits", () => Results.File("index.html", "text/html"));

app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();
