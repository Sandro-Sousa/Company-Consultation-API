
using Entities.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Maps
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.Cnpj)
                .HasColumnName("Cnpj")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(14)
                .IsRequired(true);

            builder.Property(x => x.Result)
                .HasColumnName("Resultado")
                .HasColumnType("NVARCHAR(MAX)")
                .IsRequired(true);
        }
    }
}
