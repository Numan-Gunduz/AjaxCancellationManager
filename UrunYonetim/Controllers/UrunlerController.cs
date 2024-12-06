using Microsoft.AspNetCore.Mvc;
using UrunYonetim.Data;
using UrunYonetim.Models;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class UrunlerController : Controller
{
    private readonly ApplicationDbContext _context;

    public UrunlerController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var urunler = await _context.Urunler.ToListAsync();
        return View(urunler);
    }

    public async Task<IActionResult> UrunListele(CancellationToken cancellationToken)
    {
        try
        {
            Console.WriteLine("UrunListele işlemi başladı.");
            for (int i = 0; i < 100; i++)
            {
                await Task.Delay(100, cancellationToken);
            }
            var urunler = await _context.Urunler.ToListAsync(cancellationToken);
            Console.WriteLine("UrunListele işlemi tamamlandı.");
            return Json(urunler);
        }
        catch (TaskCanceledException)
        {
            Console.WriteLine("UrunListele işlemi iptal edildi.");
            return Json(new { message = "İşlem iptal edildi." });
        }
    }
}











//using Microsoft.AspNetCore.Mvc;
//using UrunYonetim.Data;
//using UrunYonetim.Models;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;

//public class UrunlerController : Controller
//{
//    private readonly ApplicationDbContext _context;
//    private static CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

//    public UrunlerController(ApplicationDbContext context)
//    {
//        _context = context;
//    }

//    public async Task<IActionResult> Index()
//    {
//        var urunler = await _context.Urunler.ToListAsync();
//        return View(urunler);
//    }

//    [HttpPost]
//    public async Task<IActionResult> UrunListele()
//    {
//        var cancellationToken = _cancellationTokenSource.Token;
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


//    [HttpPost]
//    public IActionResult CancelRequest()
//    {
//        _cancellationTokenSource.Cancel();
//        _cancellationTokenSource = new CancellationTokenSource();
//        return Ok();
//    }
//}