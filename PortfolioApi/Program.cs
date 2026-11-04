//importeert zodat het na het starten weet waar de klassen te vinden zijnn
using PortfolioApi.Features.Projects;
using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;
using PortfolioApi.Features.Blogposts;

//startpunt applicatie
var builder = WebApplication.CreateBuilder(args);

//hier voeg ik services toe aan de container, bereid API voor maken van swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//zet validatie voor minimal API aan. zonder dit negeert het de [required] en [maxlength]
builder.Services.AddValidation();

//deze regel voor de databse toegevoegd
builder.Services.AddDbContext<AppDbContext>(options =>  //databaseklasse in de DI-container registreren
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); //ik gebruik sqlserver en haalt verbinding uit appsettings.json


//hier koppel ik de interface aan de klasse. als endpoint vraag om Iproject/blogservicee geeft ASP.NEt projectservicer mee 
builder.Services.AddScoped<IProjectService, ProjectService>();

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


//hier werk de applicatie en kan ik alleen instellen hoe de request word behandeld.
var app = builder.Build();

// HTTP request instellen
if (app.Environment.IsDevelopment()) 
{
    app.UseSwagger();  //zet de JSON beschrijving van APi online
    app.UseSwaggerUI(); // Activeer Swagger UI
}


using (var scope = app.Services.CreateScope()) // hier een scope gemaakt. nodig omdat AppDBcontext ook scoped is, en er buiten HTTP request geen scope bestaat
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>(); //hier haalt het database gegeven uit de DI-container op. geeft fout aan met REQUIRED     
    context.Database.Migrate(); // Zorgt ervoor dat migraties automatisch gedriaid worden bij opstarten.
}

//STUURT HTTP-VERZOEK DOOR NAAR HTTPS.
app.UseHttpsRedirection();

//hie past het de gemaakte CORS toe. heb het voor de endpoint gezet anders werkt het niet.
app.UseCors("AllowAll");

//deze twee registreren de /api/blogpost en /api/project met hun endpoints (GET, POSt, PUT DElete)
app.MapBlogpostEndpoints();
app.MapProjects();

app.Run();  //hier start de webserver 

