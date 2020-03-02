using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Warehouse_Manage_WPF.Entities;

namespace Warehouse_Manage_WPF.EntitiesConfiguration
{
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            HasKey(c => c.Id);

            Property(c => c.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar")
            .HasColumnOrder(2)
            .IsRequired()
            .HasMaxLength(255);

            Property(c => c.Address)
            .HasColumnName("Address")
            .HasColumnType("varchar")
            .HasColumnOrder(3)
            .IsOptional()
            .HasMaxLength(128);

            Property(c => c.City)
            .HasColumnName("City")
            .HasColumnType("varchar")
            .HasColumnOrder(4)
            .IsOptional()
            .HasMaxLength(64);

            HasMany(c => c.Projects)
            .WithRequired(c => c.Customer)
            .HasForeignKey(c => c.CustomerID);
        }
    }
}
