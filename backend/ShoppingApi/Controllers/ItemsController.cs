using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingApi.Data;
using ShoppingApi.DTOs;
using ShoppingApi.Models;

namespace ShoppingApi.Controllers
{
    [ApiController]
    [Route("items")]
    [Produces("application/json")]
    public class ItemsController : ControllerBase
    {
        private readonly ShoppingContext _context;

        public ItemsController(ShoppingContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all shopping items.
        /// </summary>
        /// <returns>List of all items.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Item>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            var items = await _context.Items.ToListAsync();
            return Ok(items);
        }

        /// <summary>
        /// Create a new item or update existing item by name.
        /// </summary>
        /// <param name="itemRequest">Item payload with name and quantity.</param>
        /// <returns>Created or updated item.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Item), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Item>> CreateOrUpdateItem([FromBody] ItemRequest itemRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingItem = await _context.Items
                .FirstOrDefaultAsync(i => i.Name == itemRequest.Name);

            if (existingItem != null)
            {
                existingItem.Quantity += itemRequest.Quantity;
                await _context.SaveChangesAsync();
                return Ok(existingItem);
            }

            var newItem = new Item
            {
                Name = itemRequest.Name,
                Quantity = itemRequest.Quantity
            };

            _context.Items.Add(newItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetItemById),
                new { itemId = newItem.Id },
                newItem);
        }

        /// <summary>
        /// Get an item by its ID.
        /// </summary>
        /// <param name="itemId">Item identifier.</param>
        /// <returns>Requested item if found.</returns>
        [HttpGet("{itemId:int}")]
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Item>> GetItemById([FromRoute] int itemId)
        {
            var item = await _context.Items.FindAsync(itemId);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        /// <summary>
        /// Update an existing item's name or quantity.
        /// </summary>
        /// <param name="itemId">Item identifier.</param>
        /// <param name="itemRequest">Updated item payload.</param>
        /// <returns>Updated item.</returns>
        [HttpPut("{itemId:int}")]
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Item>> UpdateItem(
            [FromRoute] int itemId,
            [FromBody] ItemRequest itemRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingItem = await _context.Items.FindAsync(itemId);
            if (existingItem == null)
                return NotFound();

            existingItem.Name = itemRequest.Name;
            existingItem.Quantity = itemRequest.Quantity;
            await _context.SaveChangesAsync();

            return Ok(existingItem);
        }

        /// <summary>
        /// Delete an item.
        /// </summary>
        /// <param name="itemId">Item identifier.</param>
        [HttpDelete("{itemId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteItem([FromRoute] int itemId)
        {
            var existingItem = await _context.Items.FindAsync(itemId);
            if (existingItem == null)
                return NotFound();

            _context.Items.Remove(existingItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
