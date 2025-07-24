using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MVCECommerce.Domain;

public enum Genders
{
    Male , Female // Cinsiyet enumu oluşturduk. Enumlar sabit değerlerdir. Bu enumu User sınıfında kullanacağız. cinsiyet için enum kullanmak daha iyi bir yöntemdir. Çünkü enumlar sabit değerlerdir(1,0).
}
public class User : IdentityUser<Guid> //IdentityUser classını kullanabilmek için Microsoft.AspNetCore.Identity paketini nugetten indiriyoruz. Microsoftun son iki versiyonun da ismini doğru yazarsak paketi kendisi buluyor. Doğru yazdıktan sonra ctrl+(nokta) ya basıyoruz ve paketi kendisi buluyor(using Microsoft.AspNetCore.Identity). Guid(benzersiz ıd) kullanıcağımız için <Guid> şeklinde yanına ekledik.
{
    //scalar properties(veritabanında tutulan özellikler)
    public required string GivenName { get; set; } //Üye adı soyadı
    public required DateTime Date { get; set; } //Üye kayıt tarihi
    public Genders Gender { get; set; } //Cinsiyet enumu.
    //Navigation Properties(ilişki özellikleri)
    public ICollection<Address> Addresses { get; set; } = new List<Address>(); //kullanıcının adresleri olabilir.
    public ICollection<Comment> Comments { get; set; } = new List<Comment>(); // bir kullanıcının birden fazla yorumu olabilir.
    public ICollection<Order> Orders { get; set; } = new List<Order>(); // bir kullanıcının birden fazla siparişi olabilir.
    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>(); // bir kullanıcının birden fazla sepet ıtemı olabilir.
}

public class UserConfiguration : IEntityTypeConfiguration<User> // microsoft.EntityFrameworkCore kütüphanesinden IEntityTypeConfiguration arayüzünü implement ettik(uyguladık).  IEntityTypeConfiguration<_EntityBase> interface'si classa ne yapıcağını söyler. aşağıdaki configure metodunu bu sayede kullanabiliyoruz.
{
    public void Configure(EntityTypeBuilder<User> builder) // configure methodu extra ayar yapmak ve özellik eklemek bağlantı ayarlarını belirtmek için kullanılır.Database oluşturulacağı zaman configure methodunu çalıştırır orda ki ayarlara göre database'i oluşturur. eğer yapmaz isek varsayılan ayarlarla oluşturur.
    {
       builder
            .HasMany(p => p.Addresses) // Bir kullanıcının birden fazla adresi olabilir.
            .WithOne(p => p.User) // Bir adresin bir kullanıcısı vardır.
            .HasForeignKey(p => p.UserId) // Address tablosu UserId foreign key ile User tablosuna bağlıdır.
            .OnDelete(DeleteBehavior.Cascade); // Kullanıcı silindiğinde adresler de silinsin.
        builder
            .HasMany(p => p.Comments) // Bir kullanıcının birden fazla yorumu olabilir.
            .WithOne(p => p.User) // Bir yorumun bir kullanıcısı vardır.
            .HasForeignKey(p => p.UserId) // Comment tablosu UserId foreign key ile User tablosuna bağlıdır.
            .OnDelete(DeleteBehavior.Cascade); // Kullanıcı silindiğinde yorumlar da silinsin.
        builder
            .HasMany(p => p.Orders) // Bir kullanıcının birden fazla siparişi olabilir.
            .WithOne(p => p.User) // Bir siparişin bir kullanıcısı vardır.
            .HasForeignKey(p => p.UserId) // Order tablosu UserId foreign key ile User tablosuna bağlıdır.
            .OnDelete(DeleteBehavior.Restrict); // Kullanıcı silinemesin siparişleri var.
        builder
            .HasMany(p => p.ShoppingCartItems) // Bir kullanıcının birden fazla sepet ıtemi olabilir.
            .WithOne(p => p.User) // Bir sepet ıteminin bir kullanıcısı vardır.
            .HasForeignKey(p => p.UserId) // ShoppingCartItem tablosu UserId foreign key ile User tablosuna bağlıdır.
            .OnDelete(DeleteBehavior.Cascade); // Kullanıcı silindiğinde sepet ıtemleri de silinsin.
    }
}