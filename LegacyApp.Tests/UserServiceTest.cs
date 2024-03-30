using System;
using JetBrains.Annotations;
using LegacyApp;
using Microsoft.VisualBasic.CompilerServices;
using Xunit;

namespace LegacyApp.Tests;

[TestSubject(typeof(UserService))]
public class UserServiceTest
{

    [Fact]
    public void Add_User_Should_Return_False_When_Missing_Firstname()
    {
        // Arrange
        var userService = new UserService();
        // Act
        var addUserService = userService.AddUser("", "Doe", "email@email.com", DateTime.Parse("12-12-1930"), 2);
        // Assert
        Assert.False(addUserService);
    }

    [Fact]
    public void Add_User_Should_Return_False_When_Missing_Lastname()
    {
        // Arrange
        var userService = new UserService();
        // Act
        var addUserService = userService.AddUser("John", "", "email@email.com", DateTime.Parse("12-12-1930"), 2);
        // Assert
        Assert.False(addUserService);
    }

    [Fact]
    public void Add_User_Should_Return_False_When_Missing_At()
    {
        // Arrange
        var userService = new UserService();
        // Act
        var addUserService = userService.AddUser("John", "Doe", "email@emailcom", DateTime.Parse("12-12-1930"), 2);
        // Assert
        Assert.False(addUserService);
    }
    [Fact]
    public void Add_User_Should_Return_False_When_Missing_Dot()
    {
        // Arrange
        var userService = new UserService();
        // Act
        var addUserService = userService.AddUser("John", "Doe", "emailemail.com", DateTime.Parse("12-12-1930"), 2);
        // Assert
        Assert.False(addUserService);
    }


    [Fact] public void Add_User_Should_Return_False_When_Age_Under_21()
    {
        // Arrange
        var userService = new UserService();
        var date12YearAgo = DateTime.Now.AddYears(-21).AddDays(1);
        // Act
        var addUserService = userService.AddUser("John", "Doe", "email@email.com",date12YearAgo, 2);
        // Assert
        Assert.False(addUserService);
    }
    
    

    [Fact]
    public void Add_User_Should_Throw_Exception_When_User_Does_Not_Exist()
    {
        // Arrange
        var userService = new UserService();
        var id = int.MaxValue;
        // Act
        
        // Assert
        Assert.Throws<ArgumentException>((() => userService.AddUser("John", "Doe", "email@email.com",DateTime.Parse("12-12-1930"),id)));
    }
    
    
    [Fact]
    public void Add_User_Should_Throw_Exception_When_No_Data_On_Non_VeryImportant_User()
    {
        // Arrange
        var userService = new UserService();
        var id = int.MaxValue;
        // Act
        
        // Assert
        Assert.Throws<ArgumentException>((() => userService.AddUser("John", "NotExisting", "email@email.com",DateTime.Parse("12-12-1930"),1)));
    }
}