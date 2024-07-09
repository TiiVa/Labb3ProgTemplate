using Labb3ProgTemplate.Enums;

namespace Labb3ProgTemplate.DataModels.Products;

public class Fruit : RealProduct
{
    public override ProductTypes Type { get; set; } = ProductTypes.Fruit;
    public Fruit(string name, double price, ProductTypes productTypes) : base(name, price, productTypes)
    {
    }
}