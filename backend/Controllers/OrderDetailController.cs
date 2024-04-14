//using backend.Context;
//using backend.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace backend.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class OrderDetailController : ControllerBase
//    {
//        private readonly OrderContext _dbContext;

//        public OrderDetailController(OrderContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        // Lấy chi tiết đơn hàng dựa trên id đơn hàng
//        [HttpGet("{orderId}")]
//        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails(int orderId)
//        {
//            var orderDetails = await _dbContext.OrderDetails.Where(od => od.OrderId == orderId).ToListAsync();
//            if (orderDetails == null || !orderDetails.Any())
//            {
//                return NotFound();
//            }
//            return Ok(orderDetails);
//        }

//        // Thêm mới chi tiết đơn hàng
//        [HttpPost]
//        public async Task<ActionResult<OrderDetail>> CreateOrderDetail(OrderDetail orderDetail)
//        {
//            _dbContext.OrderDetails.Add(orderDetail);
//            await _dbContext.SaveChangesAsync();

//            return CreatedAtAction(nameof(GetOrderDetails), new { orderId = orderDetail.OrderId }, orderDetail);
//        }

//        // Xóa chi tiết đơn hàng dựa trên id
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteOrderDetail(int id)
//        {
//            var orderDetail = await _dbContext.OrderDetails.FindAsync(id);
//            if (orderDetail == null)
//            {
//                return NotFound();
//            }

//            _dbContext.OrderDetails.Remove(orderDetail);
//            await _dbContext.SaveChangesAsync();

//            return NoContent();
//        }
//    }
//}
