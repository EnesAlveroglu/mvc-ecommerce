using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCECommerce.Domain;

public class City // İlçe sınıfı
{
    public int Id { get; set; } // Benzersiz Id 
    public int ProvinceId { get; set; } // İl Id (Foreign Key) 
    public string? Name { get; set; } // İlçe adı null olabilir dedik ki oluştururken bu classı Name vermeden oluşturabilelim.
    public ICollection<Address> Adresses { get; set; } = new List<Address>();
    public Province? Province { get; set; } // İlçenin bir tane ili vardır.(navigation property)
}
public class CityConfiguration : IEntityTypeConfiguration<City> // microsoft.EntityFrameworkCore kütüphanesinden IEntityTypeConfiguration arayüzünü implement ettik(uyguladık).  IEntityTypeConfiguration<_EntityBase> interface'si classa ne yapıcağını söyler. aşağıdaki configure metodunu bu sayede kullanabiliyoruz.
{
    public void Configure(EntityTypeBuilder<City> builder) // configure methodu extra ayar yapmak ve özellik eklemek bağlantı ayarlarını belirtmek için kullanılır.Database oluşturulacağı zaman configure methodunu çalıştırır orda ki ayarlara göre database'i oluşturur. eğer yapmaz isek varsayılan ayarlarla oluşturur.
    {
        builder
            .ToTable("Cities"); //Tablodaki adı. //Microsoft.EntityFrameworkCore yetmez.ToTable() metodu Microsoft.EntityFrameworkCore.Relational paketinde gelir. 
        builder.HasIndex(p=> new { p.ProvinceId, p.Name }) // il Id'si, İlçe adı için bir index oluşturduk. Bu index sayesinde İl Id'si ilçe adı ile sorgulama yaparken performans artışı sağlar. Hem il Id'si hem ilçe adı aynı olan iki kayıt olamaz. Bu yüzden hem il Id'si hem ilçe adı için bir index oluşturduk.
            .IsUnique(); // İl Id'si, İlçe adı database'de unique olsun dedik. Aynı il Id'si, ilçe adı birden fazla kez eklenemesin.
        builder
            .Property(p => p.Name)
            .IsRequired(); // İlçe adı database'de zorunlu olsun dedik.
        builder
            .HasMany(p => p.Adresses) // bir ilçenin birden fazla adresi olabilir.
            .WithOne(p => p.City) // bir adres bir ilçeye bağldır.
            .HasForeignKey(p => p.CityId) // Address tablosu CityId foreign key ile City tablosuna bağlıdır. 
            .OnDelete(DeleteBehavior.Restrict); // bağlı olduğu adresler varsa ilçe silinemesin.
        builder
            .HasData(
            new City { Id = 1, ProvinceId = 1, Name = "Ceyhan" }
            );
    }
}
