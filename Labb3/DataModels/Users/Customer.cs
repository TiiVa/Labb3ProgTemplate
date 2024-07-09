using System.Collections.Generic;
using System.Windows.Documents;
using Labb3ProgTemplate.DataModels.Products;
using Labb3ProgTemplate.Enums;

namespace Labb3ProgTemplate.DataModels.Users;

public class Customer : User
{

    public override UserTypes Type{ get; } = UserTypes.Customer;

    public Customer(string name, string password, UserTypes type, List<RealProduct> cart) : base(name, password, type, cart)
    {
        Cart = cart;
    }

    
}