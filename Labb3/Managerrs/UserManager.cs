using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Documents;
using Labb3ProgTemplate.DataModels.Products;
using Labb3ProgTemplate.DataModels.Users;
using Labb3ProgTemplate.Enums;

namespace Labb3ProgTemplate.Managerrs;

public static class UserManager
{
    private static readonly IEnumerable<User>? _users = new List<User>();


    private static User _currentUser;

    public static IEnumerable<User>? Users => _users;


    public static User CurrentUser
    {
        get => _currentUser;
        set
        {
            _currentUser = value;
            CurrentUserChanged?.Invoke();
        }
    }

    public static event Action CurrentUserChanged;

    // Skicka detta efter att användarlistan ändrats eller lästs in
    public static event Action UserListChanged;

    public static bool IsAdminLoggedIn
    {
        get
        {
            if (CurrentUser is null)
            {
                return false;
            }
            return CurrentUser.Type is UserTypes
                .Admin;

        }
    }

    public static bool IsCustomerLoggedIn
    {
        get
        {
            if (CurrentUser is null)
            {
                return false;
            }
            return CurrentUser.Type is UserTypes
                .Customer;
        }
    }

    public static void ChangeCurrentUser(string name, string password, UserTypes type)
    {
        foreach (var user in Users)
        {
            if (user.Name == name)
            {
                if (user.Authenticate(password))
                {
                    CurrentUser = user;
                }
                break;
            }
        }

        
        

    }


    public static void LogOut()
    {
        CurrentUser = null;
        CurrentUserChanged.Invoke();
        SaveUsersToFile();

    }

    public static async Task SaveUsersToFile()
    {
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb3");
        Directory.CreateDirectory(directory);
        var filePath = Path.Combine(directory, "Users.json");
        var jsonOptions = new JsonSerializerOptions();
        jsonOptions.WriteIndented = true;
        var json = JsonSerializer.Serialize(Users, jsonOptions);

        await using StreamWriter sw = new StreamWriter(filePath);
        await sw.WriteLineAsync(json);

    }

    public static async Task LoadUsersFromFile()
    {
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb3");
        Directory.CreateDirectory(directory);
        var filePath = Path.Combine(directory, "Users.json");
        var deserializedUser = new List<User>();

        if (!File.Exists(filePath))
        {
            ((List<User>)_users).AddRange(new List<User>(){new Customer("Knatte", "123", UserTypes.Customer, new List<RealProduct>()),
                new Admin("Phoenix", "321", UserTypes.Admin, new List<RealProduct>()),
                new Customer("Fury", "500", UserTypes.Customer, new List<RealProduct>()),
                new Customer("Storm", "501", UserTypes.Customer, new List<RealProduct>()),
                new Customer("Ace", "502", UserTypes.Customer, new List<RealProduct>())});
             await SaveUsersToFile();
        }

        if (File.Exists(filePath))
        {
            
            var text = string.Empty;
            var line = string.Empty;

            using var sr = new StreamReader(filePath);

            while ((line =  await sr.ReadLineAsync()) != null)
            {
                text += line;
            }

            using (var jsonDoc = JsonDocument.Parse(text))
            {
                if (jsonDoc.RootElement.ValueKind == JsonValueKind.Array)
                {
                    foreach (var jsonElement in jsonDoc.RootElement.EnumerateArray())
                    {
                        User? a;
                        switch (jsonElement.GetProperty("Type").GetInt32())
                        {
                            case 0:
                                a = jsonElement.Deserialize<Admin>();
                                deserializedUser.Add(a);
                                break;
                            case 1:
                                a = jsonElement.Deserialize<Customer>();
                                deserializedUser.Add(a);
                                break;
                        }
                    }

                    

                }


            }

            foreach (var user in deserializedUser)
            {
                if (_users is List<User> users)
                {
                    users.Add(user);

                }
            }

            
            

        }
    }

    public static void RegisterNewUser(string newName, string newPassword, UserTypes typeOfNewUser)
    {

        if (Users is List<User> users)
        {
            if (typeOfNewUser is UserTypes.Admin)
            {
                users.Add(new Admin(newName, newPassword, UserTypes.Admin, new List<RealProduct>()));
            }
            else
            {
                users.Add(new Customer(newName, newPassword, UserTypes.Customer, new List<RealProduct>()));
            }

        }

        
    }

    public static void AddToCart(RealProduct product)
    {
        CurrentUser.Cart.Add(product);
        
    }

    public static void RemoveFromCart(RealProduct product)
    {
        CurrentUser.Cart.Remove(product);
        
    }


}