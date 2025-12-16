using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Encontro.WebUI.Areas.Admin.Pages.Users;

[Authorize(Roles = "Administrador")]
public class IndexModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;

    public IndexModel(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public List<UserViewModel> Users { get; set; } = new();
    public int TotalUsers { get; set; }
    public int TotalAdmins { get; set; }
    public int TotalViewers { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        
        Users = new List<UserViewModel>();
        
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            
            Users.Add(new UserViewModel
            {
                Id = user.Id,
                Email = user.Email ?? "",
                EmailConfirmed = user.EmailConfirmed,
                Roles = roles.ToList()
            });
        }

        TotalUsers = Users.Count;
        TotalAdmins = Users.Count(u => u.Roles.Contains("Administrador"));
        TotalViewers = Users.Count(u => u.Roles.Contains("Visualizador"));

        return Page();
    }

    public async Task<IActionResult> OnPostToggleRoleAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        // Não permitir que o usuário mude sua própria role
        if (user.Email == User.Identity?.Name)
        {
            TempData["ErrorMessage"] = "Você não pode alterar sua própria role.";
            return RedirectToPage();
        }

        var currentRoles = await _userManager.GetRolesAsync(user);
        
        // Remover todas as roles atuais
        if (currentRoles.Any())
        {
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
        }

        // Adicionar a nova role (toggle entre Administrador e Visualizador)
        if (currentRoles.Contains("Administrador"))
        {
            await _userManager.AddToRoleAsync(user, "Visualizador");
            TempData["SuccessMessage"] = $"Usuário {user.Email} alterado para Visualizador.";
        }
        else
        {
            await _userManager.AddToRoleAsync(user, "Administrador");
            TempData["SuccessMessage"] = $"Usuário {user.Email} alterado para Administrador.";
        }

        return RedirectToPage();
    }
}

public class UserViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; }
    public List<string> Roles { get; set; } = new();
}
