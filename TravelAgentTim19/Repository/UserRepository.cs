using System;
using System.Collections.Generic;
using System.IO;
using TravelAgentTim19.Model;
using Newtonsoft.Json;

namespace TravelAgentTim19.Repository;

public class UserRepository
{
    private List<User> Users;
    
    public UserRepository()
    {
        string json = File.ReadAllText(@"..\..\..\Data\Users.json");
        List<User> users = JsonConvert.DeserializeObject<List<User>>(json);
        Users = users;
    }
    public List<User> GetUsers()
    {
        return this.Users;
    }

    public void AddUser(User user)
    {
        this.Users.Add(user);
    }
    
    public User GetUserByEmail(string email)
    {
        foreach (User user in Users)
        {
            if (user.Email.Equals(email))
            {
                return user;
            }
        }
        return null;
    }
    public User GetUserById(int id)
    {
        foreach (User user in Users)
        {
            if (user.Id.Equals(id))
            {
                return user;
            }
        }
        return null;
    }
    
    public void Save()
    {
        File.WriteAllText(@"..\..\..\Data\Users.json", 
            JsonConvert.SerializeObject(Users));
    }
}