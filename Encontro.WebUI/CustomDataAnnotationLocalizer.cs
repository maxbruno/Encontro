using Microsoft.Extensions.Localization;

namespace Encontro.WebUI;

public class CustomDataAnnotationLocalizer : IStringLocalizer
{
    private readonly Dictionary<string, string> _translations = new()
    {
        // Mensagens padrão de validação
        { "The {0} field is required.", "O campo {0} é obrigatório." },
        { "The field {0} is invalid.", "O campo {0} é inválido." },
        { "The {0} field is not a valid e-mail address.", "O campo {0} não é um endereço de email válido." },
        { "The {0} field is not a valid phone number.", "O campo {0} não é um número de telefone válido." },
        { "The field {0} must be a string with a maximum length of {1}.", "O campo {0} deve ter no máximo {1} caracteres." },
        { "The field {0} must be a string with a minimum length of {2} and a maximum length of {1}.", "O campo {0} deve ter entre {2} e {1} caracteres." },
        { "The field {0} must be between {1} and {2}.", "O campo {0} deve estar entre {1} e {2}." },
        
        // Mensagens adicionais
        { "This field is required.", "Este campo é obrigatório." },
        { "Please enter a valid email address.", "Por favor, insira um endereço de email válido." },
        { "Please enter a valid phone number.", "Por favor, insira um número de telefone válido." }
    };

    public LocalizedString this[string name]
    {
        get
        {
            var value = GetString(name);
            return new LocalizedString(name, value, !_translations.ContainsKey(name));
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var format = GetString(name);
            var value = string.Format(format, arguments);
            return new LocalizedString(name, value, !_translations.ContainsKey(name));
        }
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        return _translations.Select(x => new LocalizedString(x.Key, x.Value, false));
    }

    private string GetString(string name)
    {
        return _translations.TryGetValue(name, out var value) ? value : name;
    }
}
