using Encontro.Domain.Entities;
using Encontro.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Encontro.WebUI.Data;

public static class DbInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        var context = serviceProvider.GetRequiredService<AppDbContext>();

        // Seed Teams e Roles do domínio PRIMEIRO
        await SeedTeamsAsync(context, logger);
        await SeedRolesAsync(context, logger);

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

    private static async Task SeedTeamsAsync(AppDbContext context, ILogger logger)
    {
        if (await context.Teams.AnyAsync())
        {
            logger.LogInformation("Teams já existem. Skipping seed.");
            return;
        }

        var teams = new List<Team>
        {
            new Team { Order = "00", Name = "Círculo", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Team { Order = "01a", Name = "Conselho Arquidiocesano", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Team { Order = "01b", Name = "Conselho Regional Centro Oeste", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Team { Order = "01c", Name = "Equipe Dirigente", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Team { Order = "02", Name = "Casal Coordenador", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Team { Order = "03", Name = "Casal Espiritualizador", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Team { Order = "04", Name = "Equipe de Círculos", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Team { Order = "05", Name = "Equipe de Sala", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Team { Order = "06", Name = "Equipe de Compras", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Team { Order = "07", Name = "Equipe de Café e Minimercado", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Team { Order = "08", Name = "Equipe de Ordem e Limpeza", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Team { Order = "09", Name = "Equipe de Liturgia e Vigília", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Team { Order = "10", Name = "Equipe de Secretaria", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Team { Order = "11", Name = "Equipe de Cozinha", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Team { Order = "12", Name = "Equipe de Visitação", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Team { Order = "13", Name = "Equipe de Acolhida", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
        };

        context.Teams.AddRange(teams);
        await context.SaveChangesAsync();
        logger.LogInformation($"✅ {teams.Count} Teams criados com sucesso.");
    }

    private static async Task SeedRolesAsync(AppDbContext context, ILogger logger)
    {
        if (await context.Roles.AnyAsync())
        {
            logger.LogInformation("Roles do domínio já existem. Skipping seed.");
            return;
        }

        var roles = new List<Role>
        {
            new Role { Order = "00", Name = "Diretor Espiritual", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Role { Order = "01", Name = "Casal Montagem", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Role { Order = "02", Name = "Casal Fichas", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Role { Order = "03", Name = "Casal Finanças", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Role { Order = "04", Name = "Casal Palestras", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Role { Order = "05", Name = "Casal Pós Encontro", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Role { Order = "06", Name = "Coordenador(es) Geral", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Role { Order = "07", Name = "Coordenador(es)", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Role { Order = "08", Name = "Membro", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Role { Order = "09", Name = "Círculo Amarelo", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Role { Order = "10", Name = "Círculo Azul", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Role { Order = "11", Name = "Círculo Vermelho", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Role { Order = "12", Name = "Círculo Verde", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            new Role { Order = "13", Name = "Círculo Laranja", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
        };

        context.Roles.AddRange(roles);
        await context.SaveChangesAsync();
        logger.LogInformation($"✅ {roles.Count} Roles do domínio criados com sucesso.");
    }
}
