using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCECommerce.Domain;

public class ProductSpecification
{
    // public Guid Id { get; set; } bazı tablolarda Id gerekmiyor. içinde ki alan zaten UNIQUE olduğu için o alanı primary key olarak kullanabiliriz.
    public Guid ProductId { get; set; } // Ürünün Id'si (Foreign Key)
    public Guid SpecificationId { get; set; } // Özelliğin Id'si (Foreign Key)
    public string? Value { get; set; } // Özelliğin değeri (örneğin: "Renk: Mavi", "Boyut: L", vb.)

}

public class ProductSpecificationConfiguration : IEntityTypeConfiguration<ProductSpecification>
{
    public void Configure(EntityTypeBuilder<ProductSpecification> builder)
    {
        builder
            .ToTable("ProductSpecifications"); // Microsoft.EntityFrameworkCore.Relational paketini kurmamız gerekiyor.
        
        builder
            .HasKey(p => new { p.ProductId, p.SpecificationId }); // Composite key (bileşik anahtar) olarak ProductId ve SpecificationId kullanıyoruz. böylece aynı özellik aynı üründe iki kez olamıyor.
        builder
            .Property(p => p.Value)
            .IsRequired(); // Özelliğin değeri zorunlu olsun dedik.
    }
}