var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddControllersWithViews(); // Controllers i�indeki viewleri kullanabilmek i�in servis olarak ekledik. Ziyaret�i istek att��� zaman ilk program.cs dosyas�na gelir ve oradan y�nlendirme yap�l�r.

app.MapGet("/", () => "Hello World!");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // rota tan�mlamas� yap�ld�. �stek geldi�inde HomeController'daki Index action'�na y�nlendirilecek. id varsa yaz�cak yoksa yazmaz.
app.Run();
