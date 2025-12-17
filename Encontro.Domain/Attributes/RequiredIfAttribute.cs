using System.ComponentModel.DataAnnotations;

namespace Encontro.Domain.Attributes;

/// <summary>
/// Validation attribute that makes a property required based on another property's value
/// </summary>
public class RequiredIfAttribute : ValidationAttribute
{
    private readonly string _dependentProperty;
    private readonly object _targetValue;

    public RequiredIfAttribute(string dependentProperty, object targetValue)
    {
        _dependentProperty = dependentProperty;
        _targetValue = targetValue;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var propertyInfo = validationContext.ObjectType.GetProperty(_dependentProperty);
        
        if (propertyInfo == null)
        {
            return new ValidationResult($"Property {_dependentProperty} not found");
        }

        var dependentValue = propertyInfo.GetValue(validationContext.ObjectInstance);

        // Check if the dependent property has the target value
        if (dependentValue != null && dependentValue.Equals(_targetValue))
        {
            // If it does, validate that the current property has a value
            if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
            {
                return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} é obrigatório quando {_dependentProperty} é {_targetValue}");
            }
        }

        return ValidationResult.Success;
    }
}
