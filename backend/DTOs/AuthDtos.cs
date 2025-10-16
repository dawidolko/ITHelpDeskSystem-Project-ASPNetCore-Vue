using System.ComponentModel.DataAnnotations;

namespace HelpDeskAPI.DTOs;

public class RegisterDto
{
    [Required(ErrorMessage = "First name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 100 characters")]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 100 characters")]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(200, ErrorMessage = "Email cannot exceed 200 characters")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", 
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")]
    public required string Password { get; set; }

    [StringLength(50, ErrorMessage = "Phone number cannot exceed 50 characters")]
    public string? PhoneNumber { get; set; }

    [StringLength(100, ErrorMessage = "Department cannot exceed 100 characters")]
    public string? Department { get; set; }
}

public class LoginDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public required string Password { get; set; }
}

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public UserDto User { get; set; } = null!;
}

public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string Role { get; set; } = string.Empty;
    public string? Department { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ChangeRoleDto
{
    [Required(ErrorMessage = "User ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "User ID must be valid")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Role is required")]
    [RegularExpression(@"^(User|Technician|Admin)$", ErrorMessage = "Role must be User, Technician, or Admin")]
    public required string Role { get; set; }
}

public class UpdateUserDto
{
    [StringLength(100, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 100 characters")]
    public string? FirstName { get; set; }

    [StringLength(100, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 100 characters")]
    public string? LastName { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(200, ErrorMessage = "Email cannot exceed 200 characters")]
    public string? Email { get; set; }

    [StringLength(50, ErrorMessage = "Phone number cannot exceed 50 characters")]
    public string? PhoneNumber { get; set; }

    [StringLength(100, ErrorMessage = "Department cannot exceed 100 characters")]
    public string? Department { get; set; }

    public bool? IsActive { get; set; }
}

public class ChangePasswordDto
{
    [Required(ErrorMessage = "Current password is required")]
    public required string CurrentPassword { get; set; }

    [Required(ErrorMessage = "New password is required")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", 
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")]
    public required string NewPassword { get; set; }
}
