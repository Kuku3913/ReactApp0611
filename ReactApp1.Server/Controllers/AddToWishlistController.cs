using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Models;

namespace ReactApp1.Server.Controllers
{

    //點擊加入願望清單(SAVE)

    // 設定路由為 "api/Activities"
    [Route("api/[controller]")]
    [ApiController]

    public class AddToWishlistController : Controller
    {
        private readonly lookdaysContext _context;

        public AddToWishlistController(lookdaysContext context)
        { 
            _context = context;
        }


        [HttpPost]
        public IActionResult WishlistSave(int? UserId, int? ActivityId)
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
                BookingStatesId = 2, // 替換為預設的預訂狀態ID
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
