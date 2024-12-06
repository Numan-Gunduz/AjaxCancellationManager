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
    
    if (!context.Urunler.Any())
    {
        var urunler = new List<Urun>();

        
        for (int i = 1; i <= 10000; i++)
        {
            urunler.Add(new Urun
            {
                Ad = $"Urun {i}",
                Fiyat = (decimal)(i % 100 + 1) 
            });
        }

       
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

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
