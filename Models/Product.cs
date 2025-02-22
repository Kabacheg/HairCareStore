namespace Hair_Care_Store.Models;
public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int Price { get; set; }
}
