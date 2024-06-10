using Entities.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired();

            // Configuração do Email como tipo de navegação própria
            builder.OwnsOne(x => x.Email, email =>
            {
                email.Property(e => e.Address)
                    .HasColumnName("Email")
                    .IsRequired();

                email.OwnsOne(e => e.Verification, verification =>
                {
                    verification.Property(v => v.Code)
                        .HasColumnName("EmailVerificationCode")
                        .IsRequired();

                    verification.Property(v => v.ExpiresAt)
                        .HasColumnName("EmailVerificationExpiresAt");

                    verification.Property(v => v.VerifiedAt)
                        .HasColumnName("EmailVerificationVerifiedAt");

                    verification.Ignore(v => v.IsActive);
                });
            });

            // Configuração do Password como tipo de navegação própria
            builder.OwnsOne(x => x.Password, password =>
            {
                password.Property(p => p.Hash)
                    .HasColumnName("PasswordHash")
                    .IsRequired();

                password.Property(p => p.ResetCode)
                    .HasColumnName("PasswordResetCode")
                    .IsRequired();
            });
        }
    }
}
