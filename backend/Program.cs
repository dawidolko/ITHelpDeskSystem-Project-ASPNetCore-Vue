using HelpDeskAPI.Data;
using HelpDeskAPI.Swagger;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = false;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HelpDeskContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "http://localhost:5174", "http://localhost:3000", "http://localhost:8080")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "IT Help Desk API",
        Version = "v1",
        Description = @"REST API for IT Help Desk System - Supporting SFWP (Sort, Filter, Search, Pagination) + JWT Authentication

**Authentication:**
1. Register at POST /api/auth/register or login at POST /api/auth/login
2. Copy the token from response
3. Click 'Authorize' button (🔓) at the top
4. Enter: Bearer {your_token}
5. Click 'Authorize'

**Test Accounts:**
- Admin: admin@firma.pl / Admin123!
- Technician: tech@firma.pl / Tech123!
- User: user@firma.pl / User123!",
        Contact = new OpenApiContact
        {
            Name = "Dawid Olko",
            Email = "do125148@stud.ur.edu.pl"
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = @"JWT Authorization header using the Bearer scheme.

**How to use:**
1. Login via POST /api/auth/login
2. Copy the 'token' value from response
3. Paste ONLY the token here (without 'Bearer' prefix)
4. Click 'Authorize' button

**Example token:** eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    
    c.EnableAnnotations();
    
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
    
    c.SchemaFilter<EnumSchemaFilter>();
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "IT Help Desk API v1");
    c.RoutePrefix = "swagger";
    c.DocumentTitle = "IT Help Desk API - Swagger UI";
    c.DefaultModelsExpandDepth(-1);
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    c.DisplayRequestDuration();
});

app.UseCors("AllowVueApp");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Health check endpoint for Docker
app.MapGet("/api/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }))
    .WithName("HealthCheck")
    .WithTags("Health")
    .Produces(200);

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<HelpDeskContext>();
    var logger = services.GetRequiredService<ILogger<Program>>();
    
    // Retry logic for database connection
    var maxRetries = 15;
    var retryCount = 0;
    var retryDelay = TimeSpan.FromSeconds(5);
    
    while (retryCount < maxRetries)
    {
        try
        {
            logger.LogInformation("Attempting to connect to database and run migrations... (attempt {Count}/{Max})", retryCount + 1, maxRetries);
            
            // Apply pending migrations (creates database and all tables)
            context.Database.Migrate();
            logger.LogInformation("Database migrations applied successfully.");
            
            // Seed the database with initial data
            DbSeeder.Initialize(context);
            logger.LogInformation("Database seeding completed successfully.");
            break;
        }
        catch (Exception ex)
        {
            retryCount++;
            if (retryCount >= maxRetries)
            {
                logger.LogError(ex, "Failed to connect to database after {Count} attempts. Application will exit.", maxRetries);
                throw;
            }
            logger.LogWarning(ex, "Database connection failed. Retrying in {Delay} seconds... (attempt {Count}/{Max})", retryDelay.TotalSeconds, retryCount, maxRetries);
            Thread.Sleep(retryDelay);
        }
    }
}

app.Run();
