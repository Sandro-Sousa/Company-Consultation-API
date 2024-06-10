
namespace Service.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public UserDTO()
        {
        }

        public UserDTO(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
    }

    public class UserLoginDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public UserLoginDTO()
        {
        }

        public UserLoginDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
