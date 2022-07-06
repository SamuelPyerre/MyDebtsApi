using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyDebtsApi.Models;

namespace MyDebtsApi.Data.Mappings
{
    public class DividaMap : IEntityTypeConfiguration<DividaModel>
    {
        public void Configure(EntityTypeBuilder<DividaModel> builder)
        {
            builder.ToTable("Divida");

            //Chave primaria
            builder.HasKey(x => x.Id);

            // Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            // Propriedades
            builder
                .HasOne(x => x.Autor)
                .WithMany(x=> x.Dividas)
                .HasConstraintName("FK_Divida_Autor")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Titulo)
                .IsRequired()
                .HasColumnName("Titulo")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasColumnName("Descricao")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.DataCriacao)
                .IsRequired()
                .HasColumnName("DataCriacao")
                .HasColumnType("SMALLDATETIME")
                .HasMaxLength(60)
                .HasDefaultValueSql("GETDATE()");
            // .HasDefaultValue(DateTime.Now.ToUniversalTime());
        }

        
    }
}
