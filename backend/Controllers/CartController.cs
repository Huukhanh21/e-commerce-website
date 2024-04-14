using backend.Context;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartContext _dbContext;

        public CartController(CartContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Lấy tất cả các mục trong giỏ hàng kèm thông tin sản phẩm
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            var cartsWithProductInfo = await _dbContext.Carts
                .Include(c => c.Product)
                .ToListAsync();

            if (cartsWithProductInfo == null || !cartsWithProductInfo.Any())
            {
                return NotFound();
            }
            return Ok(cartsWithProductInfo);
        }

        // Lấy thông tin một mục trong giỏ hàng theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(int id)
        {
            var cart = await _dbContext.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        // Thêm một mục vào giỏ hàng
        [HttpPost]
        public async Task<ActionResult<Cart>> CreateCart(Cart cart)
        {
            _dbContext.Carts.Add(cart);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCart), new { id = cart.Id }, cart);
        }

        // Cập nhật thông tin một mục trong giỏ hàng
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(int id, Cart cart)
        {
            if (id != cart.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(cart).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Xóa một mục trong giỏ hàng
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _dbContext.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _dbContext.Carts.Remove(cart);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        // Kiểm tra xem một mục trong giỏ hàng có tồn tại không
        private bool CartExists(int id)
        {
            return _dbContext.Carts.Any(e => e.Id == id);
        }

        // Thêm một mục vào giỏ hàng hoặc cập nhật số lượng nếu mục đã tồn tại
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(Cart item)
        {
            // Kiểm tra xem sản phẩm đã tồn tại trong giỏ hàng chưa
            var existingCartItem = await _dbContext.Carts.Include(c => c.Product).FirstOrDefaultAsync(c => c.Product_Id == item.Product_Id);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += item.Quantity;
            }
            else
            {
                // Lấy thông tin sản phẩm từ Product_Id và gán vào thuộc tính Product
                var product = await _dbContext.Products.FindAsync(item.Product_Id);

                var newCartItem = new Cart
                {
                    Product_Id = item.Product_Id,
                    Quantity = item.Quantity,
                    Product = product
                };
                _dbContext.Carts.Add(newCartItem);
            }

            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        [HttpDelete("ClearCart")]
        public async Task<IActionResult> ClearCart()
        {
            var allItems = await _dbContext.Carts.ToListAsync();

            if (allItems == null || !allItems.Any())
            {
                return NotFound();
            }

            _dbContext.Carts.RemoveRange(allItems);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

    }
}
