using System.Diagnostics.CodeAnalysis;

namespace Entities;

public class Torch : Product
{
    [SetsRequiredMembers]
    public Torch(string name, string desctription, double price) : base(name, desctription, "Факел", price)
    {
    }
}