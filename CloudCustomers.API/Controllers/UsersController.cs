using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudCustomers.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpPost(Name = "AddUser")]
    public async Task<IActionResult> Post([FromBody] UserInput userInput)
    {
        var user = new User()
        {
            Name = userInput.Name,
            Email = userInput.Email,
            Address = userInput.Address            
        };

        var newUser = await _usersService.AddUser(user);

        var routeValues = new { id = newUser.Id };
        return CreatedAtAction("GetUser", routeValues, newUser);
    }

    
    [HttpGet("{id}", Name = "GetUser")]
    public async Task<IActionResult> GetUser( int id)
    {
        var user = await _usersService.GetUser(id);

        if (user != null)
        {
            return Ok(user);
        }

        return NotFound();
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _usersService.GetAllUsers();

        if (users.Any())
        {
            return Ok(users);
        }

        return NotFound();
    }
}