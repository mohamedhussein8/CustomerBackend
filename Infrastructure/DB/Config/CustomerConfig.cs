using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DB.Config
{
    internal class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
           builder.HasKey(x => x.Id);
            builder.Property(x => x.CustomerFirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CustomerLastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CustomerEmail).IsRequired();
            builder.Property(x => x.CustomerGender).IsRequired().HasMaxLength(1);
            builder.Property(x => x.CustomerDOB).IsRequired();

        }
    }
}
