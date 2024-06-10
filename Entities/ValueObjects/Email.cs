using System.Text.RegularExpressions;

namespace Entities.ValueObjects
{
    public partial class Email
    {
        private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        protected Email()
        {
            Address = string.Empty;
        }

        public Email(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new Exception("E-mail inválido");

            Address = address.Trim().ToLower();

            if (Address.Length < 5)
                throw new Exception("E-mail inválido");

            if (!ValidateEmail(Address))
                throw new Exception("E-mail inválido");
        }

        public string Address { get; }
        //public string Hash => Address.ToBase64();
        public Verification Verification { get; private set; } = new();

        public void ResendVerification()
            => Verification = new Verification();

        public static implicit operator string(Email email)
            => email.ToString();

        public static implicit operator Email(string address)
            => new(address);

        public override string ToString()
            => Address;

        private static bool ValidateEmail(string email)
        {
            Regex regex = new Regex(Pattern);
            return regex.IsMatch(email);
        }
    }
}
