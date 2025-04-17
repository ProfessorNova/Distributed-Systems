using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingApi.Data;
using ShoppingApi.Models;

[ApiController]
[Route("items")]
public class ItemsController : ControllerBase
{
    private readonly ShoppingContext _context;

    public ItemsController(ShoppingContext context)
    {
        _context = context;
    }

    /// <summary>Get all shopping items.</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> GetItems()
    {
        return await _context.Items.ToListAsync();
    }

    /// <summary>Create new or update existing item by name.</summary>
    [HttpPost]
    public async Task<ActionResult<Item>> CreateOrUpdateItem(Item input)
    {
        if (string.IsNullOrWhiteSpace(input.Name) || input.Quantity < 0)
            return BadRequest();

        var existing = await _context.Items
            .FirstOrDefaultAsync(i => i.Name == input.Name);
        if (existing is not null)
        {
            existing.Quantity += input.Quantity;
            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        _context.Items.Add(input);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetItemById), new { itemId = input.Id }, input);
    }

    /// <summary>Get an item by its ID.</summary>
    [HttpGet("{itemId:int}")]
    public async Task<ActionResult<Item>> GetItemById(int itemId)
    {
        var item = await _context.Items.FindAsync(itemId);
        return item is not null ? Ok(item) : NotFound();
    }

    /// <summary>Update an existing item's name or quantity.</summary>
    [HttpPut("{itemId:int}")]
    public async Task<ActionResult<Item>> UpdateItem(int itemId, Item input)
    {
        if (itemId != input.Id || string.IsNullOrWhiteSpace(input.Name) || input.Quantity < 0)
            return BadRequest();

        var item = await _context.Items.FindAsync(itemId);
        if (item is null) return NotFound();

        item.Name = input.Name;
        item.Quantity = input.Quantity;
        await _context.SaveChangesAsync();

        return Ok(item);
    }

    /// <summary>Delete an item.</summary>
    [HttpDelete("{itemId:int}")]
    public async Task<IActionResult> DeleteItem(int itemId)
    {
        var item = await _context.Items.FindAsync(itemId);
        if (item is null) return NotFound();

        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
