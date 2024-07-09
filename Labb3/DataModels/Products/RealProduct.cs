using Labb3ProgTemplate.Enums;

namespace Labb3ProgTemplate.DataModels.Products;

public class RealProduct : Product
{
    public RealProduct(string name, double price, ProductTypes type) : base(name, price, type)
    {

    }

    public override ProductTypes Type { get; set; }
}