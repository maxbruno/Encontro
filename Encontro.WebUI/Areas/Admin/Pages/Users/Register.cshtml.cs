using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Encontro.WebUI.Areas.Admin.Pages.Users;

[Authorize(Roles = "Administrador")]
public class RegisterModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;

    public RegisterModel(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public class InputModel
    {
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Digite um email válido.")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "A senha e a confirmação não coincidem.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecione um nível de acesso.")]
        [Display(Name = "Nível de Acesso")]
        public string Role { get; set; } = "Visualizador";
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = new IdentityUser 
        { 
            UserName = Input.Email, 
            Email = Input.Email,
            EmailConfirmed = true // Admin confirma o email automaticamente
        };

        var result = await _userManager.CreateAsync(user, Input.Password);

        if (result.Succeeded)
        {
            // Adicionar role selecionada
            await _userManager.AddToRoleAsync(user, Input.Role);
            
            TempData["SuccessMessage"] = $"Usuário {Input.Email} criado com sucesso como {Input.Role}.";
            return RedirectToPage("Index");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return Page();
    }
}
