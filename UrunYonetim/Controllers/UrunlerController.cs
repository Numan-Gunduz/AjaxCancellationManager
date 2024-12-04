//using Microsoft.AspNetCore.Mvc;
//using UrunYonetim.Data;
//using UrunYonetim.Models;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;

//public class UrunlerController : Controller
//{
//    private readonly ApplicationDbContext _context;

//    public UrunlerController(ApplicationDbContext context)
//    {
//        _context = context;
//    }

//    public async Task<IActionResult> Index()
//    {
//        var urunler = await _context.Urunler.ToListAsync();
//        return View(urunler);
//    }

//    public async Task<IActionResult> UrunListele(CancellationToken cancellationToken)
//    {
//        try
//        {
//            Console.WriteLine("UrunListele işlemi başladı.");
//            for (int i = 0; i < 100; i++)
//            {
//                await Task.Delay(100, cancellationToken);
//            }
//            var urunler = await _context.Urunler.ToListAsync(cancellationToken);
//            Console.WriteLine("UrunListele işlemi tamamlandı.");
//            return Json(urunler);
//        }
//        catch (TaskCanceledException)
//        {
//            Console.WriteLine("UrunListele işlemi iptal edildi.");
//            return Json(new { message = "İşlem iptal edildi." });
//        }
//    }




//}
using Microsoft.AspNetCore.Mvc;
using UrunYonetim.Data;
using UrunYonetim.Models;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class UrunlerController : Controller
{
    private readonly ApplicationDbContext _context;
    private static CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    public UrunlerController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Ana sayfa (Index) 
    public async Task<IActionResult> Index()
    {
        var urunler = await _context.Urunler.ToListAsync();
        return View(urunler);
    }

    // Ürün Listeleme İşlemi
    [HttpPost]
    public async Task<IActionResult> UrunListele()
    {
        var cancellationToken = _cancellationTokenSource.Token;
        try
        {
            Console.WriteLine("UrunListele işlemi başladı."); // İşlem başladığında log
            for (int i = 0; i < 100; i++)
            {
                await Task.Delay(100, cancellationToken);
            }

            var urunler = await _context.Urunler.ToListAsync(cancellationToken);
            Console.WriteLine("UrunListele işlemi tamamlandı."); // İşlem tamamlandığında log
            return Json(urunler);
        }
        catch (TaskCanceledException)
        {
            Console.WriteLine("UrunListele işlemi iptal edildi."); // İptal olduğunda log
            return Json(new { message = "İşlem iptal edildi." });
        }

    }

    // İptal İsteği Endpoint'i
    [HttpPost]
    public IActionResult CancelRequest()
    {
        _cancellationTokenSource.Cancel();  // İptal kaynağını tetikle
        _cancellationTokenSource = new CancellationTokenSource(); // Yeniden kullanmak için yenisini oluştur
        return Ok();
    }
}