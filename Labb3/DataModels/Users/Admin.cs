using System.Collections.Generic;
using Labb3ProgTemplate.DataModels.Products;
using Labb3ProgTemplate.Enums;

namespace Labb3ProgTemplate.DataModels.Users;

public class Admin : User
{
    
    public override UserTypes Type { get; } = UserTypes.Admin;

    public Admin(string name, string password, UserTypes type, List<RealProduct> cart) : base(name, password, type, cart)
    {
    }

}