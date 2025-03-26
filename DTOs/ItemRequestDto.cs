namespace DotNetApi.DTOs
{
    /// <summary>
    /// Data Transfer Object for creating or updating an item.
    /// </summary>
    public class ItemRequestDto
    {
        /// <summary>
        /// The name of the item (e.g., Bananas).
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// The quantity of the item.
        /// </summary>
        public int Quantity { get; set; }
    }
}