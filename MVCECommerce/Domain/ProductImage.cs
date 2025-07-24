using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCECommerce.Domain;

public class ProductImage : _EntityBase // Ürün resmi sınıfı, EntityBase sınıfından miras alır.
{
    
    public Guid ProductId { get; set; } // Ürün Id (Foreign Key)
    public byte[] Image { get; set; } // Resim verisi
    public Product? Product { get; set; } // resimlerin bir ürünü olur.
}

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage> // microsoft.EntityFrameworkCore kütüphanesinden IEntityTypeConfiguration arayüzünü implement ettik(uyguladık).  IEntityTypeConfiguration<_EntityBase> interface'si classa ne yapıcağını söyler. aşağıdaki configure metodunu bu sayede kullanabiliyoruz.
{
    public void Configure(EntityTypeBuilder<ProductImage> builder) // configure methodu extra ayar yapmak ve özellik eklemek bağlantı ayarlarını belirtmek için kullanılır.Database oluşturulacağı zaman configure methodunu çalıştırır orda ki ayarlara göre database'i oluşturur. eğer yapmaz isek varsayılan ayarlarla oluşturur.
    {
        builder
            .ToTable("ProductImages"); //Microsoft.EntityFrameworkCore yetmez.ToTable() metodu Microsoft.EntityFrameworkCore.Relational paketinde gelir. 
        builder
            .Property(p=>p.Image)
            .IsRequired(); // Resim verisi database'de zorunlu olsun dedik.
    }
}
