using RestServiceProject.Models;

namespace RestServiceProject;

public interface IUserRepository
{
    IEnumerable<User> Users { get; }
    bool Add(User user);
    bool Update(User user);
    bool Delete(Guid userId);
}

public class UserRepository : IUserRepository
{
    private static readonly List<User> users = new();

    public UserRepository()
    {
    }

    public IEnumerable<User> Users
    {
        get
        {
            return users;
        }
    }

    public bool Add(User user)
    {
        if (String.IsNullOrEmpty(user.Email) || String.IsNullOrEmpty(user.Password))
        {
            return false;
        }

        user.Id = Guid.NewGuid();
        user.Created = DateOnly.FromDateTime(DateTime.UtcNow);
        users.Add(user);
        return true;
    }

    public bool Delete(Guid userId)
    {
        var rowsDeleted = users.RemoveAll(u => u.Id == userId);
        return (rowsDeleted > 0);
    }

    public bool Update(User user)
    {
        var userFound = users.FirstOrDefault(t => t.Id == user.Id);

        if (userFound != null && !String.IsNullOrEmpty(user.Email) && !String.IsNullOrEmpty(user.Password))
        {
            userFound.Email = user.Email;
            userFound.Password = user.Password;
            return true;
        }

        return false;
    }
}
