using System.Text.Json.Serialization;

namespace RestServiceProject.Tests;

/// <summary>
/// User object for unit testing
/// </summary>
public class TestUser
{
    /// <summary>
    /// Primary Key
    /// </summary>
    [JsonPropertyName("id")]
    public Guid? Id { get; set; }

    /// <summary>
    /// Email Address
    /// </summary>
    [JsonPropertyName("email")] 
    public required string Email { get; set; }

    /// <summary>
    /// Password
    /// </summary>
    [JsonPropertyName("password")]
    public required string Password { get; set; }

    /// <summary>
    /// Date Created
    /// </summary>
    [JsonPropertyName("created")]
    public DateOnly? Created { get; set; }
}