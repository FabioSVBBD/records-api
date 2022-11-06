using Microsoft.AspNetCore.Mvc;
using records.Models;
using records.Utilities;

namespace records.Controllers;

[ApiController]
[Route("users")]
public class UserController: ControllerBase
{
    [HttpGet]
    public IActionResult GetUser([FromQuery] String id)
    {
        User? user = Mock.Users.Find((user) => user.Id == id);

        if (user == null)
            return NotFound(new Message($"User with id {id} not found"));

        return Ok(user);
    }

    [HttpPost]
    public IActionResult AddUser()
    {
        User user = new();
        Mock.Users.Add(user);

        return Ok(new Response<User>("New user created", user));
    }
}
