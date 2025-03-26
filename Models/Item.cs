namespace DotNetApi.Models
{
    /// <summary>
    /// Represents a shopping item in the system.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Primary key (ID).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the item.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Quantity of the item.
        /// </summary>
        public int Quantity { get; set; }
    }
}