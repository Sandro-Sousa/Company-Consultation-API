
namespace Entities.ValueObjects
{
    public class Verification
    {
        public Verification()
        {
        }

        public string Code { get; } = Guid.NewGuid().ToString("N")[..6].ToUpper();
        public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(3);
        public DateTime? VerifiedAt { get; private set; } = null;
        public bool IsActive => VerifiedAt != null && ExpiresAt == null;
        public string ErrorValidateCode { get; private set; } = string.Empty;

        public Verification VerifyCode(string code)
        {
            if (IsActive)
            {
                ErrorValidateCode = "Este código já foi ativado";
                return this;
            }

            if (ExpiresAt < DateTime.UtcNow)
            {
                ErrorValidateCode = "Este código já expirou";
                return this;
            }

            if (!string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase))
            {
                ErrorValidateCode = "Código de verificação inválido";
                return this;
            }

            ExpiresAt = null;
            VerifiedAt = DateTime.UtcNow;

            return this;
        }
    }
}
