using Domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.configuration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product));
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(80)
                .IsRequired();

            builder.Property(p => p.StatusName)
                .HasMaxLength(8)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(100);

            builder.Property(p => p.Stock)
                .HasMaxLength(4)
                .IsRequired();

            builder.Property(p => p.Price)
                .IsRequired();

            builder.Property(p => p.Discount);

            builder.Property(p => p.FinalPrice);

            builder.Property(p => p.CreatedBy)
               .HasMaxLength(30);

            builder.Property(p => p.LastModifiedBy)
              .HasMaxLength(30);



        }
    }
}
