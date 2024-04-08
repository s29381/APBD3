using System.Diagnostics;
using LegacyApp;

namespace TestUserService;

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
        Assert.Equal(false,result);
    }
}