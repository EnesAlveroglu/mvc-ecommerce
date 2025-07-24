using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCECommerce.Domain;

public class CarouselImage: _EntityBase // sağa sola kayan resim(carousel) için class  : (entity base sınıfından miras)
{
    public byte[] Image { get; set; } // Resim verisi
    public string? Url { get; set; } // Resmin yönlendireceği URL, boş bırakılabilir.
    public Guid? CatalogId { get; set; } // zero or 1 to many = sıfır ya da birden çoğa bağlantı. Resimlerin bir kataloğu olabilir ya da olmayabilir.(Guid?)
}

public class CarouselImageConfiguration : IEntityTypeConfiguration<CarouselImage> // microsoft.EntityFrameworkCore kütüphanesinden IEntityTypeConfiguration arayüzünü implement ettik(uyguladık).  IEntityTypeConfiguration<_EntityBase> interface'si classa ne yapıcağını söyler. aşağıdaki configure metodunu bu sayede kullanabiliyoruz.
{
    public void Configure(EntityTypeBuilder<CarouselImage> builder) // configure methodu extra ayar yapmak ve özellik eklemek bağlantı ayarlarını belirtmek için kullanılır.Database oluşturulacağı zaman configure methodunu çalıştırır orda ki ayarlara göre database'i oluşturur. eğer yapmaz isek varsayılan ayarlarla oluşturur.
    {
        //tph(Table Per Hierarchy) = inheritence(miras) özelliği kullandığımız için Entity Framework core tek bir tablo oluşturup büyün kayıtları o tablada tutar. Diğer bütün modeller _EntityBase den miras aldığı için tek bir tablo da tutar. Bu tablo çok karışık olur. bu yüzden TPT (table per type) kullanıcaz. bunun için aşağıdaki .ToTable metodunu kullanıyoruz. ToTable paketini kurmamız gerekiyor. Microsoft.EntitiyFrameworkCore.Relatioanal paketini kurmamız gerekiyor.
        builder
            .ToTable("CarouselImages"); //Microsoft.EntityFrameworkCore yetmez.ToTable() metodu Microsoft.EntityFrameworkCore.Relational paketinde gelir. 
        builder.Property(p => p.Image)
            .IsRequired(); //fotoğraf database'de zorunlu olsun dedik.
        
    }
}

/*
 * one to many
 * many to many
 * zero or one to many
 * one to one
 * 
 */
