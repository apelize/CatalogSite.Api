namespace Entities;

public class Order
{
    public required string PhoneNumber {get; set;}
    public required List<string> ProductList {get; set;}
}