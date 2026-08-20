using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
 
using MyPractice4.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ========================================
// SERVICES
// ========================================

builder.Services.AddControllers();

// Database
builder.Services.AddDbContext<APIDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// ========================================
// JWT AUTHENTICATION
// ========================================

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme
)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],

        ValidAudience = builder.Configuration["Jwt:Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                builder.Configuration["Jwt:Key"]!
            )
        )
    };
});

// IMPORTANT: This must be BEFORE builder.Build()
builder.Services.AddAuthorization();

// ========================================
// BUILD APPLICATION
// ========================================

var app = builder.Build();

// ========================================
// MIDDLEWARE
// ========================================

app.UseHttpsRedirection();

// JWT authentication
app.UseAuthentication();

// Authorization
app.UseAuthorization();

app.MapControllers();

app.Run();