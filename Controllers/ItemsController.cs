using Microsoft.AspNetCore.Mvc;
using DotNetApi.Models;
using DotNetApi.DTOs;

namespace DotNetApi.Controllers
{
    /// <summary>
    /// Controller for managing shopping items.
    /// </summary>
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        // In-memory store for demonstration
        private static readonly List<Item> Items = new();

        /// <summary>
        /// Retrieves all shopping items.
        /// </summary>
        /// <returns>A list of items.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Item>), 200)]
        [ProducesResponseType(500)]
        public ActionResult<IEnumerable<Item>> GetItems()
        {
            try
            {
                return Ok(Items);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error");
            }
        }

        /// <summary>
        /// Creates or updates a shopping item based on the item name.
        /// </summary>
        /// <param name="request">The item data (name, quantity).</param>
        /// <returns>The newly created or updated item.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Item), 200)]
        [ProducesResponseType(typeof(Item), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<Item> CreateOrUpdateItem([FromBody] ItemRequestDto request)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(request.Name) || request.Quantity <= 0)
                {
                    return BadRequest("Invalid input: 'name' must be non-empty and 'quantity' must be > 0.");
                }

                // Check if an item with the same name exists
                var existingItem = Items.FirstOrDefault(i => i.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase));
                if (existingItem != null)
                {
                    // Update existing item
                    existingItem.Quantity += request.Quantity;
                    return Ok(existingItem); // 200
                }
                else
                {
                    // Create new item
                    var newItem = new Item
                    {
                        Id = Items.Count > 0 ? Items.Max(i => i.Id) + 1 : 1,
                        Name = request.Name,
                        Quantity = request.Quantity
                    };
                    Items.Add(newItem);
                    return StatusCode(201, newItem); // 201
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error");
            }
        }

        /// <summary>
        /// Retrieves a specific shopping item by its ID.
        /// </summary>
        /// <param name="itemId">The ID of the item.</param>
        /// <returns>The item if found.</returns>
        [HttpGet("{itemId}")]
        [ProducesResponseType(typeof(Item), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<Item> GetItemById(int itemId)
        {
            try
            {
                var item = Items.FirstOrDefault(i => i.Id == itemId);
                if (item == null)
                {
                    return NotFound("Item not found");
                }
                return Ok(item);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error");
            }
        }

        /// <summary>
        /// Updates an existing shopping item.
        /// </summary>
        /// <param name="itemId">The ID of the item to update.</param>
        /// <param name="request">The updated item data (name, quantity).</param>
        /// <returns>The updated item if found.</returns>
        [HttpPut("{itemId}")]
        [ProducesResponseType(typeof(Item), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<Item> UpdateItem(int itemId, [FromBody] ItemRequestDto request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name) || request.Quantity <= 0)
                {
                    return BadRequest("Invalid input: 'name' must be non-empty and 'quantity' must be > 0.");
                }

                var item = Items.FirstOrDefault(i => i.Id == itemId);
                if (item == null)
                {
                    return NotFound("Item not found");
                }

                item.Name = request.Name;
                item.Quantity = request.Quantity;
                return Ok(item); // 200
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error");
            }
        }

        /// <summary>
        /// Deletes a shopping item by its ID.
        /// </summary>
        /// <param name="itemId">The ID of the item to delete.</param>
        /// <returns>204 if deleted, 404 if not found.</returns>
        [HttpDelete("{itemId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteItem(int itemId)
        {
            try
            {
                var item = Items.FirstOrDefault(i => i.Id == itemId);
                if (item == null)
                {
                    return NotFound("Item not found");
                }

                Items.Remove(item);
                return NoContent(); // 204
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error");
            }
        }
    }
}
