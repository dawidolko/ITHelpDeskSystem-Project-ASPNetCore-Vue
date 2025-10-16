using HelpDeskAPI.Data;
using HelpDeskAPI.DTOs;
using HelpDeskAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace HelpDeskAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly HelpDeskContext _context;
    private readonly ILogger<UsersController> _logger;

    public UsersController(HelpDeskContext context, ILogger<UsersController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [Authorize(Roles = "Admin,Technician")]
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all users", 
        Description = "Retrieve all active users. Admin and Technician only. Can be filtered by role.")]
    [SwaggerResponse(200, "Success", typeof(List<UserDto>))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(403, "Forbidden - Admin or Technician role required")]
    public async Task<ActionResult<List<UserDto>>> GetUsers([FromQuery] string? role = null)
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
            .Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                FullName = u.FullName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Role = u.Role,
                Department = u.Department,
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt
            })
            .ToListAsync();

        return Ok(users);
    }

    [HttpGet("technicians")]
    [SwaggerOperation(
        Summary = "Get technicians", 
        Description = "Retrieve all active users with Technician or Admin role. Used for ticket assignment.")]
    [SwaggerResponse(200, "Success", typeof(List<UserSummaryDto>))]
    [SwaggerResponse(401, "Unauthorized")]
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

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get user by ID", 
        Description = "Retrieve detailed information about a specific user.")]
    [SwaggerResponse(200, "Success", typeof(UserDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "User not found")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound(new { message = $"User with ID {id} not found" });
        }

        var result = new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            FullName = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role,
            Department = user.Department,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create user (Admin only)",
        Description = "Create a new user account. Admin only.")]
    [SwaggerResponse(201, "User created", typeof(UserDto))]
    [SwaggerResponse(400, "Bad Request - Email already exists")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(403, "Forbidden - Admin role required")]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] RegisterDto dto)
    {
        try
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == dto.Email.ToLower());
            if (existingUser != null)
            {
                return BadRequest(new { message = "Email already registered" });
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash = passwordHash,
                PhoneNumber = dto.PhoneNumber,
                Department = dto.Department,
                Role = "User",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var result = MapToUserDto(user);

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user");
            return StatusCode(500, new { message = "An error occurred while creating user" });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update user (Admin only)",
        Description = "Update user information. Admin only.")]
    [SwaggerResponse(200, "User updated", typeof(UserDto))]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(403, "Forbidden - Admin role required")]
    [SwaggerResponse(404, "User not found")]
    public async Task<ActionResult<UserDto>> UpdateUser(int id, [FromBody] UpdateUserDto dto)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { message = $"User with ID {id} not found" });
            }

            if (!string.IsNullOrWhiteSpace(dto.FirstName))
            {
                user.FirstName = dto.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(dto.LastName))
            {
                user.LastName = dto.LastName;
            }

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == dto.Email.ToLower() && u.Id != id);
                if (existingUser != null)
                {
                    return BadRequest(new { message = "Email already in use" });
                }
                user.Email = dto.Email;
            }

            if (dto.PhoneNumber != null)
            {
                user.PhoneNumber = dto.PhoneNumber;
            }

            if (dto.Department != null)
            {
                user.Department = dto.Department;
            }

            if (dto.IsActive.HasValue)
            {
                user.IsActive = dto.IsActive.Value;
            }

            await _context.SaveChangesAsync();

            var result = MapToUserDto(user);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user {UserId}", id);
            return StatusCode(500, new { message = "An error occurred while updating user" });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}/role")]
    [SwaggerOperation(
        Summary = "Change user role (Admin only)",
        Description = "Change user role to User, Technician, or Admin. Admin only.")]
    [SwaggerResponse(200, "Role changed successfully")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(403, "Forbidden - Admin role required")]
    [SwaggerResponse(404, "User not found")]
    public async Task<IActionResult> ChangeRole(int id, [FromBody] ChangeRoleDto dto)
    {
        try
        {
            if (id != dto.UserId)
            {
                return BadRequest(new { message = "User ID mismatch" });
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { message = $"User with ID {id} not found" });
            }

            user.Role = dto.Role;
            await _context.SaveChangesAsync();

            return Ok(new { message = $"User role changed to {dto.Role} successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error changing role for user {UserId}", id);
            return StatusCode(500, new { message = "An error occurred while changing role" });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete user (Admin only)",
        Description = "Soft delete user by marking as inactive. Admin only.")]
    [SwaggerResponse(204, "User deleted")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(403, "Forbidden - Admin role required")]
    [SwaggerResponse(404, "User not found")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { message = $"User with ID {id} not found" });
            }

            user.IsActive = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting user {UserId}", id);
            return StatusCode(500, new { message = "An error occurred while deleting user" });
        }
    }

    private static UserDto MapToUserDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            FullName = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role,
            Department = user.Department,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
    }
}
