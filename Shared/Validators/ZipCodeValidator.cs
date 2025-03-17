using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MonApi.Shared.Validators;

public class ZipCodeValidator : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        var input = value?.ToString();

        ErrorMessage = string.Empty;

        if (string.IsNullOrEmpty(input))
        {
            ErrorMessage = "Le code postal doit être renseigné";
            return false;
        }

        var regex = new Regex(@"[0-9]{5}");

        if (!regex.IsMatch(input))
        {
            ErrorMessage = "Le code postal n'est pas valide";
            return false;
        }

        return string.IsNullOrEmpty(ErrorMessage);
    }
}