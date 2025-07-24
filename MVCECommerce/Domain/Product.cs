using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCECommerce.Domain;

public class Product: _EntityBase // Ürün sınıfı, EntityBase sınıfından miras alır.
{
  
    public Guid CategoryId { get; set; } // Kategori Id(Foreign Key)
    public Guid? BrandId { get; set; } // Marka Id(Foreign Key) markası olabilir olmayabilir (zero or 1 to many)
    public string? NameTr { get; set; } // Türkçe isim
    public string? NameEn { get; set; } // English name
    public string? DescriptionTr { get; set; } // açıklamalar(türkçe) boş olabilir.
    public string? DescriptionEn { get; set; } // açıklamalar(ingilizce) boş olabilir.
    public decimal Price { get; set; } // Fiyat
    public int Views { get; set; } // ürün görüntülenme sayısı
    public Brand? Brand { get; set; } // Bir ürünün bir markası olur.

    public Category? Category { get; set; } // Bir ürünün bir kategorisi olur.
    public ICollection<Catalog> Catalogs { get; set; } = new List<Catalog>(); // many to many ilişkisi. Bir katalogda birden fazla ürün olabilir. Bir ürün birden fazla katalogda olabilir. Bu yüzden ICollection<Catalog> kullandık.  Catalog sınıfı için de ICollection<Product> Products { get; set; } = new List<Product>(); şeklinde tanımlayacağız. Bu sayede iki yönlü ilişki kurmuş olacağız.
    public ICollection<Comment> Comments { get; set; } = new List<Comment>(); // Bir ürünün birden fazla yorumu olabilir navigation property olarak tanımladık.
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); // Bir ürünün birden fazla sipariş bölümü olabilir. navigation property olarak tanımladık.
    public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>(); // Bir ürünün birden fazla resmi olabilir. navigation property olarak tanımladık.
    public ICollection<ShoppingCartItem> ShoppingCartItems  { get; set; } = new List<ShoppingCartItem>(); // Bir ürün birden fazla sepette olabilir. navigation property olarak tanımladık.

}

public class ProductConfiguration : IEntityTypeConfiguration<Product> // microsoft.EntityFrameworkCore kütüphanesinden IEntityTypeConfiguration arayüzünü implement ettik(uyguladık).  IEntityTypeConfiguration<_EntityBase> interface'si classa ne yapıcağını söyler. aşağıdaki configure metodunu bu sayede kullanabiliyoruz.
{
    public void Configure(EntityTypeBuilder<Product> builder) // configure methodu extra ayar yapmak ve özellik eklemek bağlantı ayarlarını belirtmek için kullanılır.Database oluşturulacağı zaman configure methodunu çalıştırır orda ki ayarlara göre database'i oluşturur. eğer yapmaz isek varsayılan ayarlarla oluşturur.
    {
        //tph(Table Per Hierarchy) = inheritence(miras) özelliği kullandığımız için Entity Framework core tek bir tablo oluşturup büyün kayıtları o tablada tutar. Diğer bütün modeller _EntityBase den miras aldığı için tek bir tablo da tutar. Bu tablo çok karışık olur. bu yüzden TPT (table per type) kullanıcaz. bunun için aşağıdaki .ToTable metodunu kullanıyoruz. ToTable paketini kurmamız gerekiyor. Microsoft.EntitiyFrameworkCore.Relatioanal paketini kurmamız gerekiyor.
        builder
            .ToTable("Products"); //Microsoft.EntityFrameworkCore yetmez.ToTable() metodu Microsoft.EntityFrameworkCore.Relational paketinde gelir. 
        builder.Property(p => p.NameTr)
            .IsRequired(); // Türkçe isim database'de zorunlu olsun dedik.
        builder.Property(p => p.NameEn)
            .IsRequired(); // English name database'de zorunlu olsun dedik.
        builder
            .HasMany(p => p.Comments) // Bir ürünün birden fazla yorumu olabilir.
            .WithOne(p => p.Product) // Bir yorumun bir ürünü olur.
            .HasForeignKey(p => p.ProductId) // Comment tablosu ProductId foreign key ile Product tablosuna bağlıdır.
            .OnDelete(DeleteBehavior.Restrict); // yorumu olan ürün silinemez.
        builder
            .HasMany(p => p.OrderItems) // Bir ürünün birden fazla siparişi olabilir.
            .WithOne(p => p.Product) // Bir siparişin bir ürünü olur.
            .HasForeignKey(p => p.ProductId) // OrderItems tablosu ProductId foreign key ile Product tablosuna bağlıdır.
            .OnDelete(DeleteBehavior.Restrict); //Siparişi olan ürün silinemez.
        builder
            .HasMany(p => p.ProductImages) // Bir ürünün birden fazla resmi olabilir.
            .WithOne(p=>p.Product) // Bir resmin bir ürünü olur.
            .HasForeignKey(p => p.ProductId) // ProductImage tablosu ProductId foreign key ile Product tablosuna bağlıdır.
            .OnDelete(DeleteBehavior.Cascade); // Ürün silindiğinde resimler de silinsin.
        builder
            .HasMany(p => p.ShoppingCartItems) // Bir ürün birden fazla sepette olabilir.
            .WithOne(p=>p.Product) // Bir sepette bir ürün 3 kere 5 kere olabilir.
            .HasForeignKey(p => p.ProductId) // ShoppinCartItems tablosu ProductId foreign key ile Product tablosuna bağlıdır.
            .OnDelete(DeleteBehavior.Restrict); // Ürün sepetlerde duruyorsa silinemez.

    }
}         