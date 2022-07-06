using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyDebtsApi.Models;

namespace MyDebtsApi.Data.Mappings;

public class UsuarioMap : IEntityTypeConfiguration<UsuarioModel>
{
    public void Configure(EntityTypeBuilder<UsuarioModel> builder)
    {
        builder.ToTable("Usuario");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnName("Nome")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100);
        
        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnName("Email")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120);
        
        builder.Property(x => x.Password)
            .IsRequired()
            .HasColumnName("Password")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100);

        builder.HasMany(x => x.Dividas)
            .WithOne(x=> x.Autor)
            .HasConstraintName("FK_Author_Divida")
            .OnDelete(DeleteBehavior.Cascade);
    }
}