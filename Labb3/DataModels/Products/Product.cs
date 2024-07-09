using Labb3ProgTemplate.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Labb3ProgTemplate.DataModels.Products;

public abstract class Product : INotifyPropertyChanged
{
    private string _name;
    private double _price;

    public string Name
    {
        get => _name;
        set
        {
            if (value == _name) return;
            _name = value;
            OnPropertyChanged();
        }
    }

    public double Price
    {
        get => _price;
        set
        {
            if (value.Equals(_price)) return;
            _price = value;
            OnPropertyChanged();
        }
    }

    public abstract ProductTypes Type { get; set; }

    protected Product(string name, double price, ProductTypes type)
    {
        Name = name;
        Price = price;
        
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}