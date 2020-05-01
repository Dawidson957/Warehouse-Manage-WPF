using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using DataAccess.Entities;

namespace DataAccess.EntitiesConfiguration
{
    public class ProjectConfiguration : EntityTypeConfiguration<Project>
    {
        public ProjectConfiguration()
        {
            HasKey(c => c.Id);

            Property(c => c.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar")
            .HasColumnOrder(2)
            .IsRequired()
            .HasMaxLength(255);

            Property(c => c.Status)
                .HasColumnName("Status")
                .HasColumnType("varchar")
                .HasColumnOrder(3)
                .IsOptional()
                .HasMaxLength(64);

            Property(c => c.Comment)
                .HasColumnName("Comment")
                .HasColumnType("varchar")
                .HasColumnOrder(4)
                .IsOptional()
                .HasMaxLength(1024);

            HasMany(c => c.Devices)
                .WithRequired(c => c.Project)
                .HasForeignKey(c => c.ProjectID);
        }
    }
}
