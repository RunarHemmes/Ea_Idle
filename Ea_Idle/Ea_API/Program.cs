using Microsoft.EntityFrameworkCore;
using Ea_API.Data;
using Ea_API.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EaIdleDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("APIDbCS")));
builder.Services.AddRepositories();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Add JWT authentication.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) //Warning can be ignored, this (should) always get a string, not null.
    };
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSPA",
        policy => policy.WithOrigins("https://localhost:7020") // Replace with your SPA's URL
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowSPA");

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
