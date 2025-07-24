using Microsoft.AspNetCore.Identity;

namespace MVCECommerce.Domain;

public class Role : IdentityRole<Guid>//(üyelik sistemi) iki tane entity'si(varlık) var bunlardan birisi Role üyelik sistemi bunla ile ilgili her şeyi IdentityRole classı yapıyor. Ondan miras alıyoruz.IdentityRole classını kullanabilmek için Microsoft.AspNetCore.Identity paketini nugetten indiriyoruz.Microsoftun son iki versiyonun da ismini doğru yazarsak pakaeti kendisi buluyor.Doğru yazdıktan sonra ctrl+(nokta) ya basıyoruz ve paketi kendisi buluyor(using Microsoft.AspNetCore.Identity). Guid(benzersiz ıd) kullanıcağımız için <Guid> şeklinde yanına ekledik.(rolü masa gibi düşünün.) Masa başında oturanlar bu roldeki kişiler. Bu kişiler o masanın yetkilerini kullanır. bir kişi birden fazla rolde olabilir. bir rolde birden fazla kişi olabilir.
{
    public required string DisplayName { get; set; } //Microsoftun Identity rolünü direkte kullanabilirz ama ilave olarak DisplayName isimli bir required(zorunlu) alan ekledik. Yani rolün bir görünen adı var bir de databaseye kaydettiğimiz adı var. 
}
