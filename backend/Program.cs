using HelpDeskAPI.Data;
using HelpDeskAPI.Swagger;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

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

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HelpDeskContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "http://localhost:5174", "http://localhost:3000")
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
        Description = @"REST API for IT Help Desk System - Supporting SFWP (Sort, Filter, Search, Pagination)
        
**Features:**
- ✅ Full CRUD operations for tickets
- ✅ Advanced filtering (Status, Priority, Category, Assignment)
- ✅ Full-text search (Title, Description, User names)
- ✅ Flexible sorting (Multiple fields, ASC/DESC)
- ✅ Pagination with validation (1-100 items per page)
- ✅ Comments system (Public & Internal)
- ✅ Dashboard statistics
- ✅ User management

**Validation:**
- All query parameters are validated
- Invalid page numbers return 400 Bad Request
- Invalid user IDs return 400 Bad Request
- PageSize limited to 1-100 items",
        Contact = new OpenApiContact
        {
            Name = "Dawid Olko",
            Email = "do125148@stud.ur.edu.pl"
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
});

app.UseCors("AllowVueApp");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<HelpDeskContext>();
    
    try
    {
        context.Database.Migrate();
        DbSeeder.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();
