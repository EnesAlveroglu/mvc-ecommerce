var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddControllersWithViews(); // Controllers içindeki viewleri kullanabilmek için servis olarak ekledik. Ziyaretçi istek attýðý zaman ilk program.cs dosyasýna gelir ve oradan yönlendirme yapýlýr.

app.MapGet("/", () => "Hello World!");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // rota tanýmlamasý yapýldý. Ýstek geldiðinde HomeController'daki Index action'ýna yönlendirilecek. id varsa yazýcak yoksa yazmaz.
app.Run();
