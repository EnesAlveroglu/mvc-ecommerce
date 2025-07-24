using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCECommerce.Domain;

public class OrderItem // sipariş bölümleri sınıfı
{
    public Guid Id { get; set; } // Benzersiz Id
    public Guid ProductId { get; set; } // Ürün Id (Foreign Key)
    public Guid OrderId { get; set; } // Sipariş Id (Foreign Key)
    public int Quantity { get; set; } // Sipariş edilen ürün adedi
    public decimal Price { get; set; } // satıldığı zaman ki fiyatı
    public Order? Order { get; set; } // bir siparişte birden fazla ürün siparişi olabilir.(navigation property)
    public Product? Product { get; set; } // bir ürün birden fazla sipariş verilmiş olabilir.(navigation property)
}
public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem> // microsoft.EntityFrameworkCore kütüphanesinden IEntityTypeConfiguration arayüzünü implement ettik(uyguladık).  IEntityTypeConfiguration<_EntityBase> interface'si classa ne yapıcağını söyler. aşağıdaki configure metodunu bu sayede kullanabiliyoruz.
{
    public void Configure(EntityTypeBuilder<OrderItem> builder) // configure methodu extra ayar yapmak ve özellik eklemek bağlantı ayarlarını belirtmek için kullanılır.Database oluşturulacağı zaman configure methodunu çalıştırır orda ki ayarlara göre database'i oluşturur. eğer yapmaz isek varsayılan ayarlarla oluşturur.
    {
        builder
            .ToTable("OrderItems"); //Microsoft.EntityFrameworkCore yetmez.ToTable() metodu Microsoft.EntityFrameworkCore.Relational paketinde gelir. 
        builder
            .Property(p => p.Price)
            .HasPrecision(18, 4); // Price alanının hassasiyetini belirledik. 18 basamaklı ve 4 ondalıklı olacak şekilde ayarladık. Bu sayede fiyatların hassasiyetini artırmış olduk. devlet fiyatlar için virgülden sonra 4 basamak kullanıyor. bu yüzden 4 ondalıklı olarak ayarladık.
    }
}
