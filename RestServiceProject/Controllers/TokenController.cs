using Microsoft.AspNetCore.Mvc;

namespace RestServiceProject.Controllers;

public class TokenRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

[Route("api/login")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public TokenController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // This should require SSL
    [HttpPost]
    public dynamic Post([FromBody] TokenRequest tokenRequest)
    {
        var user = _userRepository.Users.FirstOrDefault(u => u.Email == tokenRequest.Email);
        if (user == null || user.Password != tokenRequest.Password)
        {
            return BadRequest();
        }

        var token = TokenHelper.GetToken(user.Email);
        return new { Token = token };
    }

    // This should require SSL
    [HttpGet("{email}/{password}")]
    public dynamic Get(string email, string password)
    {
        var user = _userRepository.Users.FirstOrDefault(u => u.Email == email);
        if (user == null || user.Password != password)
        {
            return BadRequest();
        }

        var token = TokenHelper.GetToken(email);
        return new { Token = token };
    }
}