using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Models;

namespace ReactApp1.Server.Controllers
{
    //載入購物車(Load)

    // 設定路由為 "api/Activities"
    [Route("api/[controller]")]
    [ApiController]

    public class LoadShoppingCartController : Controller
    {

        public class ShoppingController : Controller
        {
            private readonly lookdaysContext _context;

            // 控制器建構函數，注入資料庫上下文

            public ShoppingController(lookdaysContext context)
            {
                _context = context;
            }

        }
        [HttpPost]
        public IActionResult ShoppingLoad(int UserId)
        {
            //loadaddcart (載入購物車資料)
            lookdaysContext _context = new lookdaysContext();
            var loadaddcart = from r in _context.Bookings
                              where r.UserId == UserId && r.BookingStatesId == 1
                              select r;


            return Ok(loadaddcart);
        }
    }
}
