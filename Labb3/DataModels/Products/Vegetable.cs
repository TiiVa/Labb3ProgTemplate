using Labb3ProgTemplate.Enums;

namespace Labb3ProgTemplate.DataModels.Products;

public class Vegetable : RealProduct
{
    public override ProductTypes Type { get; set; } = ProductTypes.Vegetable;

    public Vegetable(string name, double price, ProductTypes productTypes) : base(name, price, productTypes)
    {
    }
}