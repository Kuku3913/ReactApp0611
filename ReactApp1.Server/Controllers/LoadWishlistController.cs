using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Models;

namespace ReactApp1.Server.Controllers
{
    //載入願望清單(Load)


    // 設定路由為 "api/Activities"
    [Route("api/[controller]")]
    [ApiController]


    public class LoadWishlistController : Controller
    {
        // 控制器建構函數，注入資料庫上下文
        private readonly lookdaysContext _context;

        public LoadWishlistController(lookdaysContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult WishlistLoad(int UserId)
        {
            //loadwishlist (載入願望清單資料)
            lookdaysContext _context = new lookdaysContext();
            var loadwishlist = from r in _context.Bookings
                              where r.UserId == UserId && r.BookingStatesId == 2
                              select r;
            return Ok(loadwishlist);
        }


    }
}
