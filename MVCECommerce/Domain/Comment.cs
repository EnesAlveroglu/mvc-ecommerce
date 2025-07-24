using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCECommerce.Domain;

public class Comment // Yorum sınıfı
{
    public Guid Id { get; set; } // Benzersiz Id
    public Guid ProductId { get; set; } // Ürün Id (Foreign Key)
    public DateTime Date { get; set; } // yorumun yapılma tarihi
    public Guid UserId { get; set; } // Yorum yapan kullanıcının Id'si (Foreign Key)
    public int Score { get; set; } // artan yorum puanı
    public string Text { get; set; } // Yorum metni
    public User? User { get; set; } // Yorumun bir tane kullanıcısı vardır.(navigation property)
    public Product? Product { get; set; } // Yorumun bir tane ürünü vardır.(navigation property)

}

public class CommentConfiguration : IEntityTypeConfiguration<Comment> // microsoft.EntityFrameworkCore kütüphanesinden IEntityTypeConfiguration arayüzünü implement ettik(uyguladık).  IEntityTypeConfiguration<_EntityBase> interface'si classa ne yapıcağını söyler. aşağıdaki configure metodunu bu sayede kullanabiliyoruz.
{
    public void Configure(EntityTypeBuilder<Comment> builder) // configure methodu extra ayar yapmak ve özellik eklemek bağlantı ayarlarını belirtmek için kullanılır.Database oluşturulacağı zaman configure methodunu çalıştırır orda ki ayarlara göre database'i oluşturur. eğer yapmaz isek varsayılan ayarlarla oluşturur.
    {
        builder
            .ToTable("Comments"); //Microsoft.EntityFrameworkCore yetmez.ToTable() metodu Microsoft.EntityFrameworkCore.Relational paketinde gelir. 
        builder
            .Property(p => p.Text)
            .IsRequired(); // Yorum metni database'de zorunlu olsun dedik.
    }
}
