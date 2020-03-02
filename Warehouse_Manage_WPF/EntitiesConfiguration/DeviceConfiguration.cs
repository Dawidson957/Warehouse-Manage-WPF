using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Warehouse_Manage_WPF.Entities;

namespace Warehouse_Manage_WPF.EntitiesConfiguration
{
    public class DeviceConfiguration : EntityTypeConfiguration<Device>
    {
        public DeviceConfiguration()
        {
            HasKey(c => c.Id);

            Property(c => c.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar")
            .HasColumnOrder(2)
            .IsRequired()
            .HasMaxLength(255);

            Property(c => c.ArticleNumber)
            .HasColumnName("ArticleNumber")
            .HasColumnType("nvarchar")
            .HasColumnOrder(3)
            .IsRequired()
            .HasMaxLength(64);

            Property(c => c.Location)
            .HasColumnName("Location")
            .HasColumnType("varchar")
            .HasColumnOrder(4)
            .IsRequired()
            .HasMaxLength(4);

            Property(c => c.Quantity)
            .HasColumnName("Quantity")
            .HasColumnType("int")
            .HasColumnOrder(5)
            .IsOptional();
        }
    }
}
