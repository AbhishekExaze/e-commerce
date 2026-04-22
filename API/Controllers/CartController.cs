using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CartController(ICartService cartService) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ShoppingCart>> GetCartById(string cartId)
        {
            var cart = await cartService.GetCartAsync(cartId);
            return Ok(cart ?? new ShoppingCart { Id = cartId });
        }
        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCart cart)
        {
            var updatedCart = await cartService.UpdateCartAsync(cart);
            if (updatedCart == null) return BadRequest("Problem updating the cart");
            return Ok(updatedCart);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteCart(string cartId)
        {
            var result = await cartService.DeleteCartAsync(cartId);
            if (!result) return BadRequest("Problem deleting the cart");
            return Ok();
        }
    }
}
