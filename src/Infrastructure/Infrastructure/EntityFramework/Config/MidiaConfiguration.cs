using Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Config;
public class MidiaConfiguration : IEntityTypeConfiguration<Midia>
{
    public void Configure(EntityTypeBuilder<Midia> builder)
    {
        builder.ToTable("Midia");

        builder.Property(p => p.Id)
            .HasColumnName("Id")
            .UseIdentityColumn()
            .ValueGeneratedOnAdd();

        builder.Property(p => p.CreationDate).HasColumnName("CreationDate");
        builder.Property(p => p.FilePath).HasColumnName("FilePath");
    }
}
