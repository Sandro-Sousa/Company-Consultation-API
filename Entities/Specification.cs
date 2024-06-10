using Entities.ValueObjects;
using Flunt.Notifications;
using Flunt.Validations;

namespace Entities
{
    public static class Specification
    {
        public static Contract<Notification> Ensure(string email, string password) =>
            new Contract<Notification>()
                .Requires()
                .IsLowerThan(password.Length, 40, "Password", "A senha deve conter menos que 40 caracteres")
                .IsGreaterThan(password.Length, 7, "Password", "A senha deve conter mais que 7 caracteres")
                .IsEmail(email, "Email", "E-mail inválido");

        public static Contract<Notification> EnsureCode(string code) =>
            new Contract<Notification>()
        .Requires()
                .IsLowerThan(code.Length, 5, "Código", "Somente 5 caracteres")
                .IsGreaterThan(code.Length, 4, "Código", "o Codigo deve conter mais que 4 caracteres");
    }
}
