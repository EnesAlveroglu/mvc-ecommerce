using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MVCECommerce.Domain;

public class Brand : _EntityBase // Marka sınıfı : (entity base sınıfından miras)
{
    public string? Name { get; set; } // Marka adı
    public byte[]? Logo { get; set; } // Logo verisi, boş bırakılabilir. 
    public ICollection<Product> Products { get; set; } = new List<Product>(); // bir markanın birden fazla ürünü olabilir.
}

public class BrandConfiguration : IEntityTypeConfiguration<Brand> // microsoft.EntityFrameworkCore kütüphanesinden IEntityTypeConfiguration arayüzünü implement ettik(uyguladık).  IEntityTypeConfiguration<_EntityBase> interface'si classa ne yapıcağını söyler. aşağıdaki configure metodunu bu sayede kullanabiliyoruz.
{
    public void Configure(EntityTypeBuilder<Brand> builder) // configure methodu extra ayar yapmak ve özellik eklemek bağlantı ayarlarını belirtmek için kullanılır.Database oluşturulacağı zaman configure methodunu çalıştırır orda ki ayarlara göre database'i oluşturur. eğer yapmaz isek varsayılan ayarlarla oluşturur.
    {
        //tph(Table Per Hierarchy) = inheritence(miras) özelliği kullandığımız için Entity Framework core tek bir tablo oluşturup büyün kayıtları o tablada tutar. Diğer bütün modeller _EntityBase den miras aldığı için tek bir tablo da tutar. Bu tablo çok karışık olur. bu yüzden TPT (table per type) kullanıcaz. bunun için aşağıdaki .ToTable metodunu kullanıyoruz. ToTable paketini kurmamız gerekiyor. Microsoft.EntitiyFrameworkCore.Relatioanal paketini kurmamız gerekiyor.
        builder
            .ToTable("Brands"); //Microsoft.EntityFrameworkCore yetmez.ToTable() metodu Microsoft.EntityFrameworkCore.Relational paketinde gelir. 
        builder.Property(p=>p.Name)
            .IsRequired(); // Marka adı database'de zorunlu olsun dedik.
        builder.HasMany(p => p.Products) // Bir markanın birden fazla ürünü olabilir.
            .WithOne(p => p.Brand) // Bir ürünün bir markası vardır.
            .HasForeignKey(p => p.BrandId) // Product tablosu BrandId foreign key ile Brand tablosuna bağlıdır. 
            .OnDelete(DeleteBehavior.SetNull); // Marka silindiğinde ürünlerin markası null olsun. Bu sayede ürünler silinmez ama markası olmayan ürünler olur. 
    }
}