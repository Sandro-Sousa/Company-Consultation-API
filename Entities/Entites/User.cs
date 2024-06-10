using Entities.ValueObjects;

namespace Entities.Entites
{
    public class User : Entity
    {
        public string Name { get; private set; } = string.Empty;
        public Email Email { get; private set; } = null!;
        public Password Password { get; private set; } = null!;

        public User()
        {
        }

        public User(string name, Email email, Password password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public User(string email, string? password = null)
        {
            Email = email;
            Password = new Password(password);
        }

    }
}
