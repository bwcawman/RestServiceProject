namespace RestServiceProject.Models;

/// <summary>
/// User object
/// </summary>
public class User
{
    /// <summary>
    /// Primary Key
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Email Address
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Password
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// Date Created
    /// </summary>
    public DateOnly? Created { get; set; }
}