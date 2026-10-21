using PortfolioApi.Features.Projects;
using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//deze regel voor de databse toegevoegd
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IProjectService, ProjectService>();

var app = builder.Build();

// HTTP request instellen
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Activeer Swagger UI
}

app.UseHttpsRedirection();

app.MapProjects();


app.Run();

