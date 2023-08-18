namespace Entities;

public class Product 
{
    public Guid _id = Guid.NewGuid();
    public required string Name {get; set;}
    public required string Description {get; set;}
    public required string Type {get; set;}
    public double Price {get; set;}

    public Product(string name, string desctription, string type, double price) => (Name, Description, Type, Price) = (name, desctription, type, price);
}