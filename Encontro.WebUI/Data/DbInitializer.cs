using Microsoft.AspNetCore.Identity;

namespace Encontro.WebUI.Data;

public static class DbInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

        // Criar roles se não existirem
        string[] roleNames = { "Administrador", "Visualizador" };
        
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded)
                {
                    logger.LogInformation($"Role '{roleName}' criada com sucesso.");
                }
            }
        }

        // Criar usuário administrador padrão
        var adminEmail = "brunomax18@gmail.com";
        var adminPassword = "=Katano+2007";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        
        if (adminUser == null)
        {
            var newAdminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var createAdmin = await userManager.CreateAsync(newAdminUser, adminPassword);
            
            if (createAdmin.Succeeded)
            {
                await userManager.AddToRoleAsync(newAdminUser, "Administrador");
                logger.LogInformation($"Usuário administrador '{adminEmail}' criado com sucesso.");
            }
            else
            {
                var errors = string.Join(", ", createAdmin.Errors.Select(e => e.Description));
                logger.LogError($"Erro ao criar usuário administrador: {errors}");
            }
        }
        else
        {
            // Atualizar email confirmado e garantir role
            if (!adminUser.EmailConfirmed)
            {
                adminUser.EmailConfirmed = true;
                await userManager.UpdateAsync(adminUser);
                logger.LogInformation($"Email do usuário '{adminEmail}' confirmado.");
            }
            
            if (!await userManager.IsInRoleAsync(adminUser, "Administrador"))
            {
                await userManager.AddToRoleAsync(adminUser, "Administrador");
                logger.LogInformation($"Role 'Administrador' adicionada ao usuário '{adminEmail}'.");
            }
        }
    }
}
