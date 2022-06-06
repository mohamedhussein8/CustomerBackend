using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DB.Config
{
    internal class AddressConfig : IEntityTypeConfiguration<Addresses>
    {
        public void Configure(EntityTypeBuilder<Addresses> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(300);
            builder.HasOne(x =>x.Customer).WithMany(x => x.Addresses).HasForeignKey(x => x.CustomerId);
        }
    }
}
