using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCECommerce.Domain;

public class Address
{
    public Guid Id { get; set; } //Benzersiz Id
    public Guid UserId { get; set; } // Adresi oluşturan kullanıcının Id'si (Foreign Key)
    public string? Name { get; set; } // Adresin adı(başlığı) null olabilir dedik ki oluştururken bu classı Name vermeden oluşturabilelim.
    public string? Text { get; set; } // Adresin metni (açık adres bilgisi)  null olabilir dedik ki oluştururken bu classı Text vermeden oluşturabilelim.
    public string? ZipCode { get; set; } // Posta kodu null olabilir dedik ki oluştururken bu classı ZipCode vermeden oluşturabilelim.
    public int CityId { get; set; } // İlçe Id (Foreign Key)
    public City? City { get; set; } // Adresin bir tane ilçesi vardır.(navigation property)
    public User? User { get; set; } // Adresin bir tane kullanıcısı vardır.(navigation property)
}

public class AddressConfiguration : IEntityTypeConfiguration<Address> // microsoft.EntityFrameworkCore kütüphanesinden IEntityTypeConfiguration arayüzünü implement ettik(uyguladık).  IEntityTypeConfiguration<_EntityBase> interface'si classa ne yapıcağını söyler. aşağıdaki configure metodunu bu sayede kullanabiliyoruz.
{
    public void Configure(EntityTypeBuilder<Address> builder) // configure methodu extra ayar yapmak ve özellik eklemek bağlantı ayarlarını belirtmek için kullanılır.Database oluşturulacağı zaman configure methodunu çalıştırır orda ki ayarlara göre database'i oluşturur. eğer yapmaz isek varsayılan ayarlarla oluşturur.
    {
        //tph(Table Per Hierarchy) = inheritence(miras) özelliği kullandığımız için Entity Framework core tek bir tablo oluşturup büyün kayıtları o tablada tutar. Diğer bütün modeller  _EntityBase den miras aldığı için tek bir tablo da tutar. Bu tablo çok karışık olur. bu yüzden TPT (table per type) kullanıcaz.TPT ayrı ayrı tutar. Bunun için aşağıdaki .ToTable metodunu kullanıyoruz. ToTable paketini kurmamız gerekiyor. Microsoft.EntitiyFrameworkCore.Relatioanal paketini kurmamız gerekiyor.
        builder
            .ToTable("Addresses"); //Microsoft.EntityFrameworkCore yetmez.ToTable() metodu Microsoft.EntityFrameworkCore.Relational paketinde gelir. 
        builder
            .Property(p => p.Name)
            .IsRequired(); // Adresin adı database'de zorunlu olsun dedik.
        builder
            .Property(p => p.Text)
            .IsRequired(); // Adresin metni database'de zorunlu olsun dedik.
    }
}
