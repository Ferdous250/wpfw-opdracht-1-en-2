using PortfolioApi.Features.Projects;
using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;
using PortfolioApi.Features.Blogposts;

var builder = WebApplication.CreateBuilder(args);

//hier voeg ik services toe aan de container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidation();

//deze regel voor de databse toegevoegd
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//hier registreert het de services
builder.Services.AddScoped<IProjectService, ProjectService>();


//hier de blogpost
builder.Services.AddScoped<IBlogpostService, BlogpostService>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});



var app = builder.Build();

// HTTP request instellen
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Activeer Swagger UI
}


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate(); // Zorgt ervoor dat migraties automatisch gedriaid worden bij opstarten
}

app.MapBlogpostEndpoints();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.MapProjects();

app.Run();

