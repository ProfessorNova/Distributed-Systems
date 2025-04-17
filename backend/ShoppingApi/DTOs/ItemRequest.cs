using System.ComponentModel.DataAnnotations;

namespace ShoppingApi.DTOs
{
    /// <summary>
    /// Data transfer object for creating or updating items.
    /// </summary>
    public class ItemRequest
    {
        /// <summary>
        /// Name of the item.
        /// </summary>
        [Required]
        public required string Name { get; set; }

        /// <summary>
        /// Quantity of the item (must be non-negative).
        /// </summary>
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
