using Encontro.Application.Interfaces;
using Encontro.Application.Services;
using Encontro.Domain.Interfaces;
using Encontro.Infrastructure;
using Encontro.Infrastructure.Repositories;
using Encontro.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Configura o DbContext para usar SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registra os repositórios da camada Infrastructure
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

// Registra o serviço de armazenamento (Local File Storage)
// Para migrar para S3/Blob Storage, basta trocar a implementação aqui
builder.Services.AddScoped<IStorageService>(sp =>
{
    var environment = sp.GetRequiredService<IWebHostEnvironment>();
    return new LocalFileStorageService(environment.WebRootPath);
});

// Registra os serviços da camada Application
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IPersonService>(sp =>
{
    var repository = sp.GetRequiredService<IPersonRepository>();
    var imageService = sp.GetRequiredService<IImageService>();
    var environment = sp.GetRequiredService<IWebHostEnvironment>();
    return new PersonService(repository, imageService, environment.WebRootPath);
});

// Configura o Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

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
