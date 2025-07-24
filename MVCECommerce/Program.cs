var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); // Controllers i�indeki viewleri kullanabilmek i�in servis olarak ekledik. Ziyaret�i istek att��� zaman ilk program.cs dosyas�na gelir ve oradan y�nlendirme yap�l�r..AddControllersWithViews(); servisi ekledi�imiz zaman MVC'nin url yap�s�n� kullanabiiliyoruz.

var app = builder.Build();

app.UseStaticFiles(); // wwwroot klas�r�ndeki statik dosyalar� kullanabilmek i�in ekledik. �rne�in, CSS, JS, resimler gibi.

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // rota tan�mlamas� yap�ld�. �stek geldi�inde HomeController'daki Index action'�na y�nlendirilecek. id varsa yaz�cak yoksa yazmaz.
app.Run();
