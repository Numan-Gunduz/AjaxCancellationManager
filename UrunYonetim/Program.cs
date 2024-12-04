using Microsoft.EntityFrameworkCore;
using UrunYonetim.Data;
using UrunYonetim.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString")));

var app = builder.Build();
void InitializeTestData(ApplicationDbContext context)
{
    // Eðer veritabanýnda zaten ürünler varsa, verileri yeniden ekleme
    if (!context.Urunler.Any())
    {
        var urunler = new List<Urun>();

        // 10.000 adet ürün oluþtur ve listeye ekle
        for (int i = 1; i <= 10000; i++)
        {
            urunler.Add(new Urun
            {
                Ad = $"Urun {i}",
                Fiyat = (decimal)(i % 100 + 1) // Fiyatlarý örnek olarak 1-100 arasýnda belirle
            });
        }

        // Veritabanýna bu ürünleri ekleyip kaydet
        context.Urunler.AddRange(urunler);
        context.SaveChanges();
    }
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    InitializeTestData(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
