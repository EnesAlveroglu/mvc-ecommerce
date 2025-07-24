using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCECommerce.Domain;

public class Order // Sipariş sınıfı
{
    public Guid Id { get; set; } // Benzersiz Id
    public DateTime Date { get; set; } // Sipariş tarihi
    public Guid UserId { get; set; } // Siparişi veren kullanıcının Id'si (Foreign Key)
    public Guid ShippingAddressId { get; set; } //Adresi tutan Id(foreign Key)
    public ICollection<OrderItem> Itmes { get; set; } = new List<OrderItem>(); // sipariş edilen ürün listesi.
    public User? User { get; set; } // Siparişin bir tane kullanıcısı vardır.(navigation property)

}
public class OrderConfiguration : IEntityTypeConfiguration<Order> // microsoft.EntityFrameworkCore kütüphanesinden IEntityTypeConfiguration arayüzünü implement ettik(uyguladık).  IEntityTypeConfiguration<_EntityBase> interface'si classa ne yapıcağını söyler. aşağıdaki configure metodunu bu sayede kullanabiliyoruz.
{
    public void Configure(EntityTypeBuilder<Order> builder) // configure methodu extra ayar yapmak ve özellik eklemek bağlantı ayarlarını belirtmek için kullanılır.Database oluşturulacağı zaman configure methodunu çalıştırır orda ki ayarlara göre database'i oluşturur. eğer yapmaz isek varsayılan ayarlarla oluşturur.
    {
        builder
            .ToTable("Orders"); //Microsoft.EntityFrameworkCore yetmez.ToTable() metodu Microsoft.EntityFrameworkCore.Relational paketinde gelir. 
        builder
            .HasMany(p => p.Itmes) // Bir siparişin birden fazla ürünü olabilir.
            .WithOne(p => p.Order) // Bir ürünün bir siparişi vardır.
            .HasForeignKey(p => p.OrderId) // OrderItem tablosu OrderId foreign key ile Order tablosuna bağlıdır.
            .OnDelete(DeleteBehavior.Cascade); // Sipariş silindiğinde siparişe ait ürünler de silinsin. Sipariş silindiğinde siparişe ait ürünler de silinsin. 
    }
}
