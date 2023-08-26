using System.Net;
using System.Text;
using System.Text.Json;

namespace RestServiceProject.Tests;

[TestClass]
public class RestServiceProjectUserControllerTests
{
    private static HttpClient? _client;

    [ClassInitialize]
    public static void Setup(TestContext context)
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5259/api/")
        };
    }

    private async Task<HttpResponseMessage> CreateUser(string email, string password)
    {
        TestUser newUser = new() { Email = email, Password = password };
        var newJson = JsonSerializer.Serialize(newUser);
        var postContent = new StringContent(newJson, Encoding.UTF8, "application/json");
        var postResult = await _client.PostAsync("user", postContent);

        return postResult;
    }

    [TestMethod]
    public async Task AddValidUser()
    {
        // Arrange
        var postResult = await CreateUser("name@domain.com", "password1");
        
        // Assert
        Assert.AreEqual(HttpStatusCode.Created, postResult.StatusCode);
    }

    [TestMethod]
    public async Task AddInvalidUserEmail()
    {
        // Arrange
        var postResult = await CreateUser("", "password1");

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, postResult.StatusCode);
    }

    [TestMethod]
    public async Task AddInvalidUserPassword()
    {
        // Arrange
        var postResult = await CreateUser("dan@dan.com", "");

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, postResult.StatusCode);
    }

    [TestMethod]
    public async Task GetValidUser()
    {
        // Arrange
        var postResult = await CreateUser("email@domain.com", "password");
        var json = await postResult.Content.ReadAsStringAsync();
        TestUser user = JsonSerializer.Deserialize<TestUser>(json);

        // Act
        var result = await _client.GetAsync("user/" + user.Id);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
    }

    [TestMethod]
    public async Task GetInvalidUser()
    {
        // Arrange
        var postResult = await CreateUser("email@domain.com", "password");
        var json = await postResult.Content.ReadAsStringAsync();
        TestUser user = JsonSerializer.Deserialize<TestUser>(json);
        user.Id = Guid.NewGuid();

        // Act
        var result = await _client.GetAsync("user/" + user.Id);

        // Assert
        Assert.IsTrue(result.StatusCode == HttpStatusCode.NotFound);
    }

    [TestMethod]
    public async Task GetAllUsers()
    {
        // Arrange
        var result = await _client.GetAsync("user");

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
    }

    [TestMethod]
    public async Task PutValidUser()
    {
        // Arrange
        var postResult = await CreateUser("email@domain.com", "password");
        var json = await postResult.Content.ReadAsStringAsync();
        TestUser user = JsonSerializer.Deserialize<TestUser>(json);
        user.Email = "newemail@domain.com";
        user.Password = "better_password";
        json = JsonSerializer.Serialize(user);
        var putContent = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var result = await _client.PutAsync("user/" + user.Id, putContent);

        // Assert
        Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
    }

    [TestMethod]
    public async Task PutInvalidUserPassword()
    {
        // Arrange
        var postResult = await CreateUser("email@domain.com", "password");
        var json = await postResult.Content.ReadAsStringAsync();
        TestUser user = JsonSerializer.Deserialize<TestUser>(json);
        user.Email = "newemail@domain.com";
        user.Password = "";
        json = JsonSerializer.Serialize(user);
        var putContent = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var result = await _client.PutAsync("user/" + user.Id, putContent);

        // Assert
        Assert.IsTrue(result.StatusCode == HttpStatusCode.BadRequest);
    }

    [TestMethod]
    public async Task PutInvalidUserEmail()
    {
        // Arrange
        var postResult = await CreateUser("email@domain.com", "password");
        var json = await postResult.Content.ReadAsStringAsync();
        TestUser user = JsonSerializer.Deserialize<TestUser>(json);
        user.Email = "";
        user.Password = "thebestestpassword";
        json = JsonSerializer.Serialize(user);
        var putContent = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var result = await _client.PutAsync("user/" + user.Id, putContent);

        // Assert
        Assert.IsTrue(result.StatusCode == HttpStatusCode.BadRequest);
    }

    [TestMethod]
    public async Task DeleteValidUserId()
    {
        // Arrange
        var postResult = await CreateUser("email@domain.com", "password");
        var json = await postResult.Content.ReadAsStringAsync();
        TestUser user = JsonSerializer.Deserialize<TestUser>(json);

        // Act
        var result = await _client.DeleteAsync("user/" + user.Id);

        // Assert
        Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
    }


    [TestMethod]
    public async Task DeleteInvalidUserId()
    {
        // Arrange
        var result = await _client.DeleteAsync("user/" + Guid.NewGuid());

        // Assert
        Assert.IsTrue(result.StatusCode == HttpStatusCode.NotFound);
    }
}