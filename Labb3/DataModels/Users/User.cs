using Labb3ProgTemplate.DataModels.Products;
using Labb3ProgTemplate.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Labb3ProgTemplate.Managerrs;

namespace Labb3ProgTemplate.DataModels.Users;

public abstract class User
{
    public string Name { get; }

    public string Password { get; }

    public abstract UserTypes Type { get; }

    public List<RealProduct> _cart;

    public List<RealProduct> Cart
    {
        get { return _cart; }
        set => _cart = value;
    }

    protected User(string name, string password, UserTypes type, List<RealProduct> cart)
    {
        Name = name;
        Password = password;
        Cart = new List<RealProduct>();
    }

    public bool Authenticate(string password)
    {
        return Password.Equals(password);
    }

    
}