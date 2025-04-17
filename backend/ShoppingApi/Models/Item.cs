namespace ShoppingApi.Models
{
    /// <summary>
    /// Represents a shopping item with a name and quantity.
    /// </summary>
    public class Item
    {
        /// <summary>Primary key.</summary>
        public int Id { get; set; }
        /// <summary>Name of the item.</summary>
        public string Name { get; set; } = null!;
        /// <summary>Quantity of the item.</summary>
        public int Quantity { get; set; }
    }
}
