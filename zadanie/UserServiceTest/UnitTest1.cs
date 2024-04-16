using System.Diagnostics;
using LegacyApp;

namespace TestProject3;

public class UnitTest1
{
    [Fact]
    public void AddUser_Should_Return_False_When_Email_Without_At_And_Dot()
    {
        //Arrange
        string firstName = "John";
        string lastName = "Doe";
        DateTime dateOfBirth = new DateTime(1980,1,1);
        int clientId = 1;
        string email = "doe";
        var service = new UserService();
        
        //Act
        bool result = service.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        //Assert
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_Should_Return_True_When_Email_With_At_And_Dot()
    {
        //Arrange
        string firstName = "John";
        string lastName = "Doe";
        DateTime dateOfBirth = new DateTime(1980,1,1);
        int clientId = 1;
        string email = "JohnDoe@gmail.com";
        var service = new UserService();
        
        //Act
        bool result = service.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_FirstName_Or_LastName_Is_Empty()
    {
        //Arrange
        string firstName = "";
        string lastName = "";
        DateTime dateOfBirth = new DateTime(1980,1,1);
        int clientId = 1;
        string email = "john.doe@gmail.com";
        var service = new UserService();

        //Act
        bool result = service.AddUser(firstName, lastName, email, dateOfBirth, clientId);
        
        //Assert
        Assert.False(result);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Age_Is_Below_21()
    {
        //Arrange
        string firstName = "John";
        string lastName = "Doe";
        DateTime dateOfBirth = new DateTime(2010,1,1);
        int clientId = 1;
        string email = "john.doe@gmail.com";
        var service = new UserService();
        
        //Act
        bool result = service.AddUser(firstName, lastName, email, dateOfBirth, clientId);
        
        //Assert
        Assert.False(result);
    }

    
    [Fact]
    public void AddUser_Should_Return_True_When_Proper_Values_Are_Provided()
    {
        //Arrange
        string firstName = "John";
        string lastName = "Doe";
        DateTime dateOfBirth = new DateTime(1980,1,1);
        int clientId = 1;
        string email = "john.doe@gmail.com";
        var service = new UserService();
        
        //Act
        bool result = service.AddUser(firstName, lastName, email, dateOfBirth, clientId);
        
        //Assert
        Assert.True(result);
    }
}