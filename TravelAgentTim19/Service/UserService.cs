using System;
using TravelAgentTim19.Model;
using TravelAgentTim19.Model.Enum;
using TravelAgentTim19.Repository;
using TravelAgentTim19.View;

namespace TravelAgentTim19.Service;

public class UserService
{
    private MainRepository MainRepository;

    public UserService(MainRepository mainRepository)
    {
        this.MainRepository = mainRepository;
    }

    public bool Register(string firstName, string lastName, string email, string password, string confirmedPassword)
    {
        if (password.Equals(confirmedPassword) && !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(email))
        {
            Random rand = new Random();
            int id = rand.Next();
            User user = new User(id, email, password, firstName, lastName, Role.Client);
            MainRepository.UserRepository.AddUser(user);
            return true;
        }
        return false;
    }

    public bool Login(string email, string password)
    {
        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
        {
            foreach (User user in MainRepository.UserRepository.GetUsers())
            {
                if (email.Equals(user.Email) && password.Equals(user.Password))
                {
                    return true;
                }
            }
         
        }
        return false;
    }


}