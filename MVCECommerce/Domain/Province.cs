using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCECommerce.Domain;

public class Province // İl  sınıfı 
{
    public int Id { get; set; } // Benzersiz Id
    public string? Name { get; set; } // İl adı null olabilir dedik ki oluştururken bu classı Name vermeden oluşturabilelim.
    public ICollection<City> Cities { get; set; } = new List<City>(); // İlçelerinin listesi. Bir il birden fazla ilçeye sahip olabilir.

}

public class ProvinceConfiguration : IEntityTypeConfiguration<Province> // microsoft.EntityFrameworkCore kütüphanesinden IEntityTypeConfiguration arayüzünü implement ettik(uyguladık).  IEntityTypeConfiguration<_EntityBase> interface'si classa ne yapıcağını söyler. aşağıdaki configure metodunu bu sayede kullanabiliyoruz.
{
    public void Configure(EntityTypeBuilder<Province> builder) // configure methodu extra ayar yapmak ve özellik eklemek bağlantı ayarlarını belirtmek için kullanılır.Database oluşturulacağı zaman configure methodunu çalıştırır orda ki ayarlara göre database'i oluşturur. eğer yapmaz isek varsayılan ayarlarla oluşturur.
    {
        builder //tabloyu oluşturucak olan kod builderdır.
            .ToTable("Provinces"); //Microsoft.EntityFrameworkCore yetmez.ToTable() metodu Microsoft.EntityFrameworkCore.Relational paketinde gelir. 
        builder
            .HasIndex(p=> new {p.Name }) // İl adı için bir index oluşturduk. Bu index sayesinde il adı ile sorgulama yaparken performans artışı sağlar.
            .IsUnique(); // İl adı database'de unique olsun dedik. Aynı il adı birden fazla kez eklenemesin.
        builder
            .Property(p => p.Name)
            .IsRequired(); // İl adı database'de zorunlu olsun dedik.
        builder
            .HasMany(p => p.Cities) // Bir şehrin birden çok ilçesi.
            .WithOne(p=> p.Province) // bir ilçenin bir tane şehri.
            .HasForeignKey(p=>p.ProvinceId) // ilçe tablosu şehir tablosuna bağlı(foreignkey) 
            .OnDelete(DeleteBehavior.Restrict); // şehre ait ilçeler olduğu için silinemez.

        builder.HasData(
    new Province { Id = 1, Name = "Adana" },
    new Province { Id = 2, Name = "Adıyaman" },
    new Province { Id = 3, Name = "Afyonkarahisar" },
    new Province { Id = 4, Name = "Ağrı" },
    new Province { Id = 5, Name = "Amasya" },
    new Province { Id = 6, Name = "Ankara" },
    new Province { Id = 7, Name = "Antalya" },
    new Province { Id = 8, Name = "Artvin" },
    new Province { Id = 9, Name = "Aydın" },
    new Province { Id = 10, Name = "Balıkesir" },
    new Province { Id = 11, Name = "Bilecik" },
    new Province { Id = 12, Name = "Bingöl" },
    new Province { Id = 13, Name = "Bitlis" },
    new Province { Id = 14, Name = "Bolu" },
    new Province { Id = 15, Name = "Burdur" },
    new Province { Id = 16, Name = "Bursa" },
    new Province { Id = 17, Name = "Çanakkale" },
    new Province { Id = 18, Name = "Çankırı" },
    new Province { Id = 19, Name = "Çorum" },
    new Province { Id = 20, Name = "Denizli" },
    new Province { Id = 21, Name = "Diyarbakır" },
    new Province { Id = 22, Name = "Edirne" },
    new Province { Id = 23, Name = "Elazığ" },
    new Province { Id = 24, Name = "Erzincan" },
    new Province { Id = 25, Name = "Erzurum" },
    new Province { Id = 26, Name = "Eskişehir" },
    new Province { Id = 27, Name = "Gaziantep" },
    new Province { Id = 28, Name = "Giresun" },
    new Province { Id = 29, Name = "Gümüşhane" },
    new Province { Id = 30, Name = "Hakkari" },
    new Province { Id = 31, Name = "Hatay" },
    new Province { Id = 32, Name = "Isparta" },
    new Province { Id = 33, Name = "Mersin" },
    new Province { Id = 34, Name = "İstanbul" },
    new Province { Id = 35, Name = "İzmir" },
    new Province { Id = 36, Name = "Kars" },
    new Province { Id = 37, Name = "Kastamonu" },
    new Province { Id = 38, Name = "Kayseri" },
    new Province { Id = 39, Name = "Kırklareli" },
    new Province { Id = 40, Name = "Kırşehir" },
    new Province { Id = 41, Name = "Kocaeli" },
    new Province { Id = 42, Name = "Konya" },
    new Province { Id = 43, Name = "Kütahya" },
    new Province { Id = 44, Name = "Malatya" },
    new Province { Id = 45, Name = "Manisa" },
    new Province { Id = 46, Name = "Kahramanmaraş" },
    new Province { Id = 47, Name = "Mardin" },
    new Province { Id = 48, Name = "Muğla" },
    new Province { Id = 49, Name = "Muş" },
    new Province { Id = 50, Name = "Nevşehir" },
    new Province { Id = 51, Name = "Niğde" },
    new Province { Id = 52, Name = "Ordu" },
    new Province { Id = 53, Name = "Rize" },
    new Province { Id = 54, Name = "Sakarya" },
    new Province { Id = 55, Name = "Samsun" },
    new Province { Id = 56, Name = "Siirt" },
    new Province { Id = 57, Name = "Sinop" },
    new Province { Id = 58, Name = "Sivas" },
    new Province { Id = 59, Name = "Tekirdağ" },
    new Province { Id = 60, Name = "Tokat" },
    new Province { Id = 61, Name = "Trabzon" },
    new Province { Id = 62, Name = "Tunceli" },
    new Province { Id = 63, Name = "Şanlıurfa" },
    new Province { Id = 64, Name = "Uşak" },
    new Province { Id = 65, Name = "Van" },
    new Province { Id = 66, Name = "Yozgat" },
    new Province { Id = 67, Name = "Zonguldak" },
    new Province { Id = 68, Name = "Aksaray" },
    new Province { Id = 69, Name = "Bayburt" },
    new Province { Id = 70, Name = "Karaman" },
    new Province { Id = 71, Name = "Kırıkkale" },
    new Province { Id = 72, Name = "Batman" },
    new Province { Id = 73, Name = "Şırnak" },
    new Province { Id = 74, Name = "Bartın" },
    new Province { Id = 75, Name = "Ardahan" },
    new Province { Id = 76, Name = "Iğdır" },
    new Province { Id = 77, Name = "Yalova" },
    new Province { Id = 78, Name = "Karabük" },
    new Province { Id = 79, Name = "Kilis" },
    new Province { Id = 80, Name = "Osmaniye" },
    new Province { Id = 81, Name = "Düzce" }
        ); // Başlangıç verilerini ekledik. Bu veriler database oluşturulurken eklenir. 

    }
}
