using HelpDeskAPI.Data;
using HelpDeskAPI.DTOs;
using HelpDeskAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace HelpDeskAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly HelpDeskContext _context;

    public UsersController(HelpDeskContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all users
    /// </summary>
    [HttpGet]
    [SwaggerOperation(Summary = "Get all users", Description = "Retrieve all users in the system")]
    [SwaggerResponse(200, "Success", typeof(List<UserSummaryDto>))]
    public async Task<ActionResult<List<UserSummaryDto>>> GetUsers([FromQuery] string? role = null)
    {
        var query = _context.Users.AsQueryable();

        if (!string.IsNullOrWhiteSpace(role))
        {
            query = query.Where(u => u.Role.ToLower() == role.ToLower());
        }

        var users = await query
            .Where(u => u.IsActive)
            .OrderBy(u => u.LastName)
            .ThenBy(u => u.FirstName)
            .Select(u => new UserSummaryDto
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                Role = u.Role,
                Department = u.Department
            })
            .ToListAsync();

        return Ok(users);
    }

    /// <summary>
    /// Get technicians only
    /// </summary>
    [HttpGet("technicians")]
    [SwaggerOperation(Summary = "Get technicians", Description = "Retrieve all technician users")]
    [SwaggerResponse(200, "Success", typeof(List<UserSummaryDto>))]
    public async Task<ActionResult<List<UserSummaryDto>>> GetTechnicians()
    {
        var technicians = await _context.Users
            .Where(u => u.Role == "Technician" || u.Role == "Admin")
            .Where(u => u.IsActive)
            .OrderBy(u => u.LastName)
            .Select(u => new UserSummaryDto
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                Role = u.Role,
                Department = u.Department
            })
            .ToListAsync();

        return Ok(technicians);
    }

    /// <summary>
    /// Get user by ID
    /// </summary>
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get user by ID", Description = "Retrieve user details")]
    [SwaggerResponse(200, "Success", typeof(UserSummaryDto))]
    [SwaggerResponse(404, "User not found")]
    public async Task<ActionResult<UserSummaryDto>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound(new { message = $"User with ID {id} not found" });
        }

        var result = new UserSummaryDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role,
            Department = user.Department
        };

        return Ok(result);
    }
}
