using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelloApi.Data;
using HelloApi.Models;
using Swashbuckle.AspNetCore.Annotations;  // 需要添加这个 using
using Request.userDto;
using Req.Dto;

namespace HelloApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "获取用户列表", Description = "查询系统中所有用户的详细信息")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        var result = users.Select(s => new
        {
            name = s.Name,
            age = s.Age,
            id = s.Id,
        }).ToList();
        return Ok(new { success = true, data = result });
    }

    // GET: api/users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(new ApiResponse<string>(true, " ", "user")
        {
            Data = user.Name
        });
    }

    // POST: api/users
    [HttpPost]
    public async Task<IActionResult> CreateUser(UserDto user)
    {
        _context.Users.Add(new User()
        {
            Name = user.Name,
            Age = user.Age,
            Email = "asfsa@qq.com",
        });
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }
}
