using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCECommerce.Domain;

public class _EntityBase // Temel varlık sınıfı
{
    public Guid Id { get; set; } // Benzersiz Id
    public Guid UserId { get; set; } // oluşturan kullanıcının Id'si (Foreign Key)
    public DateTime CreatedAt { get; set; } // Oluşturulma tarihi
    public bool IsEnabled { get; set; } //(soft delete) eğer silmek istiyorsak geçmişe dönük siparişler satışlar silinmesin raporlama yaparken sıkıntı olmasın diye bu şekilde yapıyoruz.Silinmez ama sitede gösterilmek istenmezse görünmez yapılabilir.
    public User? User { get; set; } // Kullanıcı bilgisi (Navigation Property)  Entity Framework Core ile ilişkili sınıflar arasında bağlantı kurmak için kullanılır. Bu sayede UserId ile User arasında ilişki kurulur. UserId foreign key olarak kullanılır. User ise navigation property olarak kullanılır.
}

public class _EntityBaseConfiguration : IEntityTypeConfiguration<_EntityBase> // microsoft.EntityFrameworkCore kütüphanesinden IEntityTypeConfiguration arayüzünü implement ettik(uyguladık).  IEntityTypeConfiguration<_EntityBase> interface'si classa ne yapıcağını söyler. aşağıdaki configure metodunu bu sayede kullanabiliyoruz.
{
    public void Configure(EntityTypeBuilder<_EntityBase> builder) // configure methodu extra ayar yapmak ve özellik eklemek bağlantı ayarlarını belirtmek için kullanılır.Database oluşturulacağı zaman configure methodunu çalıştırır orda ki ayarlara göre database'i oluşturur. eğer yapmaz isek varsayılan ayarlarla oluşturur.
    {
        //tph(Table Per Hierarchy) = inheritence(miras) özelliği kullandığımız için Entity Framework core tek bir tablo oluşturup büyün kayıtları o tablada tutar. Diğer bütün modeller _EntityBase den miras aldığı için tek bir tablo da tutar. Bu tablo çok karışık olur. bu yüzden TPT (table per type) kullanıcaz. bunun için aşağıdaki .ToTable metodunu kullanıyoruz. ToTable paketini kurmamız gerekiyor. Microsoft.EntitiyFrameworkCore.Relatioanal paketini kurmamız gerekiyor.
        builder
            .ToTable("_EntityBase"); //Microsoft.EntityFrameworkCore yetmez.ToTable() metodu Microsoft.EntityFrameworkCore.Relational paketinde gelir. 

    }
}