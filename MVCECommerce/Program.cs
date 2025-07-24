var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); // Controllers içindeki viewleri kullanabilmek için servis olarak ekledik. Ziyaretçi istek attýðý zaman ilk program.cs dosyasýna gelir ve oradan yönlendirme yapýlýr..AddControllersWithViews(); servisi eklediðimiz zaman MVC'nin url yapýsýný kullanabiiliyoruz.

var app = builder.Build();

app.UseStaticFiles(); // wwwroot klasöründeki statik dosyalarý kullanabilmek için ekledik. Örneðin, CSS, JS, resimler gibi.

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // rota tanýmlamasý yapýldý. Ýstek geldiðinde HomeController'daki Index action'ýna yönlendirilecek. id varsa yazýcak yoksa yazmaz.
app.Run();
