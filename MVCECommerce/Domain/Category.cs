using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCECommerce.Domain;

public class Category : _EntityBase // Kategori sınıfı, EntityBase sınıfından miras alır.
{
  
    public  string? NameTr { get; set; } // Türkçe isim
    public  string? NameEn { get; set; } // English name
    public ICollection<Product> Products { get; set; } = new List<Product>();
}

public class CategoryConfiguration : IEntityTypeConfiguration<Category> // microsoft.EntityFrameworkCore kütüphanesinden IEntityTypeConfiguration arayüzünü implement ettik(uyguladık).  IEntityTypeConfiguration<_EntityBase> interface'si classa ne yapıcağını söyler. aşağıdaki configure metodunu bu sayede kullanabiliyoruz.
{
    public void Configure(EntityTypeBuilder<Category> builder) // configure methodu extra ayar yapmak ve özellik eklemek bağlantı ayarlarını belirtmek için kullanılır.Database oluşturulacağı zaman configure methodunu çalıştırır orda ki ayarlara göre database'i oluşturur. eğer yapmaz isek varsayılan ayarlarla oluşturur.
    {
        //tph(Table Per Hierarchy) = inheritence(miras) özelliği kullandığımız için Entity Framework core tek bir tablo oluşturup büyün kayıtları o tablada tutar. Diğer bütün modeller _EntityBase den miras aldığı için tek bir tablo da tutar. Bu tablo çok karışık olur. bu yüzden TPT (table per type) kullanıcaz. bunun için aşağıdaki .ToTable metodunu kullanıyoruz. ToTable paketini kurmamız gerekiyor. Microsoft.EntitiyFrameworkCore.Relatioanal paketini kurmamız gerekiyor.
        builder
            .ToTable("Categories"); //Microsoft.EntityFrameworkCore yetmez.ToTable() metodu Microsoft.EntityFrameworkCore.Relational paketinde gelir. 
        builder.Property(p => p.NameTr)
            .IsRequired(); // Türkçe isim database'de zorunlu olsun dedik.
        builder.Property(p => p.NameEn)
            .IsRequired(); // English name database'de zorunlu olsun dedik.
        builder.HasMany(p => p.Products) // Bir kategorinin birden fazla ürünü olabilir.
            .WithOne(p => p.Category) // Bir ürünün bir kategorisi vardır.
            .HasForeignKey(p => p.CategoryId) // Product tablosu CategoryId foreign key ile Category tablosuna bağlıdır.
            .OnDelete(DeleteBehavior.Restrict); // ürünü olan kategori silinemez.
    }
}
