using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Warehouse_Manage_WPF.Entities;

namespace Warehouse_Manage_WPF.EntitiesConfiguration
{
    public class ProducerConfiguration : EntityTypeConfiguration<Producer>
    {
        public ProducerConfiguration()
        {
            HasKey(c => c.Id);

            Property(c => c.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar")
            .HasColumnOrder(2)
            .IsRequired()
            .HasMaxLength(64);

            Property(c => c.URL)
            .HasColumnName("URL")
            .HasColumnType("nvarchar")
            .HasColumnOrder(3)
            .IsOptional()
            .HasMaxLength(512);

            HasMany(c => c.Devices)
            .WithRequired(c => c.Producer)
            .HasForeignKey(c => c.ProducerID);
        }
    }
}
