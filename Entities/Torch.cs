using System.Diagnostics.CodeAnalysis;

namespace Entities;

public class Torch : Product
{
    [SetsRequiredMembers]
    public Torch(string name, string desctription, string image, double price) : base(name, desctription, image, "Факел", price)
    {
    }
}