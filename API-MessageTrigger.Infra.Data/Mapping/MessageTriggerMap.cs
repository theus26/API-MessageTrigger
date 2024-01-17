using API_MessageTrigger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_MessageTrigger.Infra.Data.Mapping
{
    public class MessageTriggerMap : IEntityTypeConfiguration<MessageTrigger>
    {
        public void Configure(EntityTypeBuilder<MessageTrigger> builder)
        {
            builder.ToTable("MessageTrigger");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.PhoneNumber)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("PhoneNumber")
                .HasColumnType("varchar(100)");

            builder.Property(prop => prop.NameInstance)
              .HasConversion(prop => prop.ToString(), prop => prop)
              .IsRequired()
              .HasColumnName("NameInstance")
              .HasColumnType("varchar(100)");

            builder.Property(prop => prop.Token)
             .HasConversion(prop => prop.ToString(), prop => prop)
             .IsRequired()
             .HasColumnName("Token")
             .HasColumnType("varchar(100)");
        }
    }
}
