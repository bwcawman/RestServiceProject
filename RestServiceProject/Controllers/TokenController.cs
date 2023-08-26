using Microsoft.AspNetCore.Mvc;

namespace RestServiceProject.Controllers;

public class TokenRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

[Route("api/token")]
[ApiController]
public class TokenController : ControllerBase
{
    // This should require SSL
    [HttpPost]
    public dynamic Post([FromBody] TokenRequest tokenRequest)
    {
        var token = TokenHelper.GetToken(tokenRequest.UserName, tokenRequest.Password);
        return new { Token = token };
    }

    // This should require SSL
    [HttpGet("{userName}/{password}")]
    public dynamic Get(string userName, string password)
    {
        var token = TokenHelper.GetToken(userName, password);
        return new { Token = token };
    }
}