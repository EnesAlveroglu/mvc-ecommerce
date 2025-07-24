using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCECommerce.Domain;

namespace MVCECommerce.Domain
{
    public class Catalog : _EntityBase // Katalog sınıfı, EntityBase sınıfından miras alır.
    {
        
        public string? NameTr { get; set; } // Türkçe ad
        public string? NameEn { get; set; } // English name
        public ICollection<Product> Products { get; set; } = new List<Product>(); // many to many ilişkisi. Bir katalogda birden fazla ürün olabilir. Bir ürün birden fazla katalogda olabilir. Bu yüzden ICollection<Product> kullandık.  Product sınıfı için de ICollection<Catalog> tanımlayacağız. Bu sayede iki yönlü ilişki kurmuş olacağız. Product sınıfında ICollection<Catalog> Catalogs { get; set; } = new List<Catalog>(); şeklinde tanımlayacağız.
        public ICollection<CarouselImage> CarouselImages { get; set; } = new List<CarouselImage>();
    }
}

public class CatalogConfiguration : IEntityTypeConfiguration<Catalog> // microsoft.EntityFrameworkCore kütüphanesinden IEntityTypeConfiguration arayüzünü implement ettik(uyguladık).  IEntityTypeConfiguration<_EntityBase> interface'si classa ne yapıcağını söyler. aşağıdaki configure metodunu bu sayede kullanabiliyoruz.
{
    public void Configure(EntityTypeBuilder<Catalog> builder) // configure methodu extra ayar yapmak ve özellik eklemek bağlantı ayarlarını belirtmek için kullanılır.Database oluşturulacağı zaman configure methodunu çalıştırır orda ki ayarlara göre database'i oluşturur. eğer yapmaz isek varsayılan ayarlarla oluşturur.
    {
        //tph(Table Per Hierarchy) = inheritence(miras) özelliği kullandığımız için Entity Framework core tek bir tablo oluşturup büyün kayıtları o tablada tutar. Diğer bütün modeller _EntityBase den miras aldığı için tek bir tablo da tutar. Bu tablo çok karışık olur. bu yüzden TPT (table per type) kullanıcaz. bunun için aşağıdaki .ToTable metodunu kullanıyoruz. ToTable paketini kurmamız gerekiyor. Microsoft.EntitiyFrameworkCore.Relatioanal paketini kurmamız gerekiyor.
        builder
            .ToTable("Catalogs"); //Microsoft.EntityFrameworkCore yetmez.ToTable() metodu Microsoft.EntityFrameworkCore.Relational paketinde gelir. 
        builder.Property(p => p.NameTr)
            .IsRequired(); // Türkçe ad database'de zorunlu olsun dedik.
        builder.Property(p => p.NameEn)
            .IsRequired(); // English name database'de zorunlu olsun dedik.
          
    }
}