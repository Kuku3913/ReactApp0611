using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Models;

namespace ReactApp1.Server.Controllers
{
    public class AddToShoppingCartController : Controller
    {
        //點擊加入購物車按鈕(SAVE)

        // 設定路由為 "api/Activities"
        [Route("api/[controller]")]
        [ApiController]

        private readonly lookdaysContext _context;

        // 控制器建構函數，注入資料庫上下文

        public ShoppingController(lookdaysContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult ShoppingSave(int? UserId, int? ActivityId)
        {
            if (UserId == null)
                return Ok("請登入會員");
            if (ActivityId == null)
                return Ok("DB沒此活動ID");

            //productPrices 從DB找商品價格
            var productPrices = (from r in _context.Activities
                                 where r.ActivityId == ActivityId
                                 select r.Price).FirstOrDefault();

            if (productPrices == null)
            {
                return Ok("未找到商品價格");
            }

            // 建立新的 Bookings 物件並設置屬性值
            Booking newBooking = new Booking()
            {
                UserId = (int)UserId, // 替換為獲取當前用戶ID的方法
                ActivityId = (int)ActivityId, // 使用 FrmProduct 中的 ActivityID
                BookingDate = DateTime.Now, // 使用當前日期時間作為預訂日期
                Price = Convert.ToDecimal(productPrices), // 使用介面元件上的價格資訊
                BookingStatesId = 1, // 替換為預設的預訂狀態ID
                Member = 1
            };



            // 將新的 Bookings 物件添加到資料庫中
            using (var db = new lookdaysContext()) //change
            {
                db.Bookings.Add(newBooking);
                db.SaveChanges();
            }

            return Ok("success");

        }
    }
}
