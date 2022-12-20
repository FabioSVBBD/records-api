using Microsoft.AspNetCore.Mvc;
using records.Utilities;
using records.Model;
using records.Model.DTO;

namespace records.Controllers;

[ApiController]
[Route("users")]
public class UserController: ControllerBase
{
    private readonly RecordsDbContext Context = new();

    [HttpGet]
    public IActionResult GetUser([FromQuery] String id)
    {
        User? user = Context.Users.Where(user => user.Id == id).ToList().FirstOrDefault();

        if (user == null)
            return NotFound(new Message($"User with id {id} not found"));

        return Ok(new UserDTO(user));
    }

    [HttpPost]
    public IActionResult AddUser([FromQuery] string id)
    {
        User? match = Context.Users.Where((user) => user.Id == id).FirstOrDefault();

        if (match != null) {
            return BadRequest(new Message($"User with id {id} already exists"));
        }

        User user = new(id);
        Context.Users.Add(user);
        Context.SaveChanges();

        return Ok(new Response<User>("New user created", user));
    }
}
