using System;
using LegacyApp.Interfaces;

namespace LegacyApp;

public class UserDataAccessAdapter : IAddUserAdapter
{
    public void AddUser(User user)
    {
        UserDataAccess.AddUser(user);
    }
}