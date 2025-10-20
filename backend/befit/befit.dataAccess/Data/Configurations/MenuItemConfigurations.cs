using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace befit.dataAccess.Data.Configurations
{
    internal class MenuItemConfigurations : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .UseIdentityColumn();

            builder.Property(m => m.Name)
                .IsUnicode(false)
                .HasMaxLength(50)
                .IsRequired(true);

            builder.Property(m => m.Description)
                .IsUnicode(false)
                .IsRequired(false);

            builder.Property(m => m.Category)
                .IsUnicode(false)
                .HasMaxLength(50)
                .IsRequired(true);

            builder.Property(m => m.Recipe)
                .IsUnicode(false)
                .IsRequired(false);

            builder.Property(m => m.Price)
                .HasPrecision(6, 2)
                .IsRequired(true);

            builder.Property(m => m.Calories)
                .IsRequired(true);

            builder.Property(m => m.Fats)
                .IsRequired(true);
                        
            builder.Property(m => m.Protein)
                .IsRequired(true);
                        
            builder.Property(m => m.Carbohydrates)
                .IsRequired(true);

            builder.Property(m => m.Picture)
                .IsFixedLength()
                .HasMaxLength(40);

            builder.Property(m => m.Video)
                .IsFixedLength()
                .HasMaxLength(40);

            builder.HasOne(m => m.Category)
                .WithMany(c => c.menuItems)
                .HasForeignKey(m => m.CategoryId);
        }
    }
}
