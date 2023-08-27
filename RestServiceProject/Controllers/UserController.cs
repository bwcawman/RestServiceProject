using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestServiceProject.Models;

namespace RestServiceProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_userRepository.Users);
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(Guid id)
    {
        var user = _userRepository.Users.FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            return NotFound(null);
        }
        else
        {
            return Ok(user);
        }
    }

    [HttpPost]
    public IActionResult Post([FromBody] User user)
    {
        bool success = _userRepository.Add(user);

        if (success)
        {
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        return BadRequest();
    }

    [HttpPut("{id}")]
    public IActionResult Put([FromBody] User user)
    {
        if (_userRepository.Update(user))
        {
            return Ok(user);
        }

        return BadRequest();
    }

    [HttpDelete("{id}")]
    [Authenticator]
    public IActionResult Delete(Guid id)
    {
        bool deleted = _userRepository.Delete(id);

        if (deleted)
        {
            return Ok();
        }
        else
        {
            return NotFound(null);
        }
    }
}