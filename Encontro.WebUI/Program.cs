using Encontro.Application.Interfaces;
using Encontro.Application.Services;
using Encontro.Domain.Interfaces;
using Encontro.Infrastructure;
using Encontro.Infrastructure.Repositories;
using Encontro.Infrastructure.Services;
using Encontro.WebUI;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages()
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) => 
            new CustomDataAnnotationLocalizer();
    });

// Configure DbContext to use SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories from Infrastructure layer
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventParticipantRepository, EventParticipantRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

// Register storage service (Local File Storage)
// To migrate to S3/Blob Storage, just change the implementation here
builder.Services.AddScoped<IStorageService>(sp =>
{
    var environment = sp.GetRequiredService<IWebHostEnvironment>();
    return new LocalFileStorageService(environment.WebRootPath);
});

// Register services from Application layer
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IPersonService>(sp =>
{
    var repository = sp.GetRequiredService<IPersonRepository>();
    var imageService = sp.GetRequiredService<IImageService>();
    var environment = sp.GetRequiredService<IWebHostEnvironment>();
    return new PersonService(repository, imageService, environment.WebRootPath);
});
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventParticipantService, EventParticipantService>();

// Configure Identity with Roles
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// Configure default UI pages for Identity
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

var app = builder.Build();

// Seed roles and admin user
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        Encontro.WebUI.Data.DbInitializer.Initialize(services).Wait();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Erro ao inicializar roles e usuário administrador.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
