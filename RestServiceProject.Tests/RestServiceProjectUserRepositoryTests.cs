using RestServiceProject.Models;

namespace RestServiceProject.Tests;

[TestClass]
public class RestServiceProjectUserRepositoryTests
{
    private readonly UserRepository _userRepository = new();

    [TestMethod]
    public void AddValidUserToUserRepository()
    {
        // Arrange
        User newUser = new() { Email = "name@domain.com", Password = "password1" };

        // Act
        bool success = _userRepository.Add(newUser);

        //Assert
        Assert.IsTrue(success);
    }

    [TestMethod]
    public void AddInvalidUserEmailToUserRepository()
    {
        // Arrange
        User newUser = new() { Email = "", Password = "password1" };

        // Act
        bool success = _userRepository.Add(newUser);

        // Assert
        Assert.IsFalse(success);
    }

    [TestMethod]
    public void AddInvalidUserPasswordToUserRepository()
    {
        // Arrange
        User newUser = new() { Email = "name@domain.com", Password = "" };

        // Act
        bool success = _userRepository.Add(newUser);

        //Assert
        Assert.IsFalse(success);
    }

    [TestMethod]
    public void DeleteValidUserFromUserRepository()
    {
        // Arrange
        User newUser = new() { Email = "name@domain.com", Password = "password1" };

        // Act
        bool addSuccess = _userRepository.Add(newUser);

        bool deleteSuccess;
        if (addSuccess)
        {
            deleteSuccess = _userRepository.Delete((Guid) newUser.Id);
        }
        else
        {
            deleteSuccess= false;
        }

        // Assert
        Assert.IsTrue(deleteSuccess);
    }

    [TestMethod]
    public void DeleteInvalidUserFromUserRepository()
    {
        // Assert
        Assert.IsFalse(_userRepository.Delete(Guid.NewGuid()));
    }

    [TestMethod]
    public void UpdateValidUserInUserRepository()
    {
        // Arrange
        User newUser = new() { Email = "name@domain.com", Password = "password1" };

        // Act
        bool addSuccess = _userRepository.Add(newUser);

        bool updateSuccess;
        if (addSuccess)
        {
            newUser.Email = "newEmailAddress.domain.com";
            newUser.Password = "changed password";
            updateSuccess = _userRepository.Update(newUser);
        }
        else
        {
            updateSuccess = false;
        }

        // Assert
        Assert.IsTrue(updateSuccess);
    }

    [TestMethod]
    public void UpdateInvalidUserInUserRepository()
    {
        // Arrange
        User newUser = new() { Email = "name@domain.com", Password = "password1" };

        // Act
        bool addSuccess = _userRepository.Add(newUser);

        bool updateSuccess;
        if (addSuccess)
        {
            User invalidUser = new() { Id = Guid.NewGuid(), Email = "name@domain.com", Password = "password1" };
            updateSuccess = _userRepository.Update(invalidUser);
        }
        else
        {
            updateSuccess = false;
        }

        // Assert
        Assert.IsFalse(updateSuccess);
    }

    [TestMethod]
    public void UpdateInvalidUserEmailInUserRepository()
    {
        // Arrange
        User newUser = new() { Email = "name@domain.com", Password = "password1" };

        // Act
        bool addSuccess = _userRepository.Add(newUser);

        bool updateSuccess;
        if (addSuccess)
        {
            newUser.Email = "";
            updateSuccess = _userRepository.Update(newUser);
        }
        else
        {
            updateSuccess = false;
        }

        // Assert
        Assert.IsFalse(updateSuccess);
    }

    [TestMethod]
    public void UpdateInvalidUserPasswordInUserRepository()
    {
        // Arrange
        User newUser = new() { Email = "name@domain.com", Password = "password1" };

        // Act
        bool addSuccess = _userRepository.Add(newUser);

        bool updateSuccess;
        if (addSuccess)
        {
            newUser.Password = "";
            updateSuccess = _userRepository.Update(newUser);
        }
        else
        {
            updateSuccess = false;
        }

        // Assert
        Assert.IsFalse(updateSuccess);
    }
}