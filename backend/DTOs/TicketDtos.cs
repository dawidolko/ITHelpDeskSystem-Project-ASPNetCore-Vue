using HelpDeskAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskAPI.DTOs;

/// <summary>
/// DTO for creating a new ticket
/// </summary>
public class CreateTicketDto
{
    /// <summary>
    /// Title of the ticket (max 200 characters)
    /// </summary>
    /// <example>Printer not working in room 305</example>
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 200 characters")]
    public required string Title { get; set; }

    /// <summary>
    /// Detailed description of the issue
    /// </summary>
    /// <example>The HP LaserJet printer in room 305 is not responding when trying to print documents. Error message: "Printer offline"</example>
    [Required(ErrorMessage = "Description is required")]
    [StringLength(5000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 5000 characters")]
    public required string Description { get; set; }

    /// <summary>
    /// Priority level: Low (1), Medium (2), High (3), Critical (4)
    /// </summary>
    /// <example>High</example>
    [Required]
    public TicketPriority Priority { get; set; } = TicketPriority.Medium;

    /// <summary>
    /// Category: Hardware (1), Software (2), Network (3), Account (4), Email (5), Printer (6), Other (7)
    /// </summary>
    /// <example>Printer</example>
    [Required]
    public TicketCategory Category { get; set; }

    /// <summary>
    /// User ID of the ticket creator
    /// </summary>
    /// <example>1</example>
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "CreatedById must be a valid user ID")]
    public int CreatedById { get; set; }
}

/// <summary>
/// DTO for updating an existing ticket (all fields optional)
/// </summary>
public class UpdateTicketDto
{
    /// <summary>
    /// Updated title (optional, 5-200 characters)
    /// </summary>
    /// <example>Printer issue - RESOLVED</example>
    [StringLength(200, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 200 characters")]
    public string? Title { get; set; }

    /// <summary>
    /// Updated description (optional, 10-5000 characters)
    /// </summary>
    /// <example>Printer has been fixed. Replaced toner cartridge.</example>
    [StringLength(5000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 5000 characters")]
    public string? Description { get; set; }

    /// <summary>
    /// Updated status (optional)
    /// </summary>
    /// <example>Resolved</example>
    public TicketStatus? Status { get; set; }

    /// <summary>
    /// Updated priority (optional)
    /// </summary>
    /// <example>Medium</example>
    public TicketPriority? Priority { get; set; }

    /// <summary>
    /// Updated category (optional)
    /// </summary>
    /// <example>Hardware</example>
    public TicketCategory? Category { get; set; }

    /// <summary>
    /// Assign to technician (optional, must be valid user ID)
    /// </summary>
    /// <example>5</example>
    [Range(1, int.MaxValue, ErrorMessage = "AssignedToId must be a valid user ID")]
    public int? AssignedToId { get; set; }

    /// <summary>
    /// Resolution notes (optional, shown when ticket is resolved)
    /// </summary>
    /// <example>Issue resolved by replacing faulty hardware component.</example>
    [StringLength(500, ErrorMessage = "Resolution notes cannot exceed 500 characters")]
    public string? ResolutionNotes { get; set; }
}

public class TicketDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public UserSummaryDto CreatedBy { get; set; } = null!;
    public UserSummaryDto? AssignedTo { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public string? ResolutionNotes { get; set; }
    public int ViewCount { get; set; }
    public int CommentCount { get; set; }
    public bool IsOverdue { get; set; }
}

public class TicketDetailDto : TicketDto
{
    public List<CommentDto> Comments { get; set; } = new();
}

public class UserSummaryDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string? Department { get; set; }
}

public class CommentDto
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public UserSummaryDto Author { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public bool IsInternal { get; set; }
}

/// <summary>
/// DTO for creating a new comment on a ticket
/// </summary>
public class CreateCommentDto
{
    /// <summary>
    /// Comment content (10-2000 characters)
    /// </summary>
    /// <example>Contacted user via phone. Printer driver needs to be reinstalled.</example>
    [Required(ErrorMessage = "Content is required")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "Content must be between 10 and 2000 characters")]
    public required string Content { get; set; }

    /// <summary>
    /// User ID of the comment author
    /// </summary>
    /// <example>5</example>
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "AuthorId must be a valid user ID")]
    public int AuthorId { get; set; }

    /// <summary>
    /// Is this comment internal? (visible only to technicians)
    /// </summary>
    /// <example>false</example>
    public bool IsInternal { get; set; } = false;
}

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}

/// <summary>
/// Query parameters for filtering, sorting, searching and pagination
/// </summary>
public class TicketQueryParameters
{
    /// <summary>
    /// Page number (minimum: 1)
    /// </summary>
    /// <example>1</example>
    [Range(1, int.MaxValue, ErrorMessage = "Page must be at least 1")]
    public int Page { get; set; } = 1;

    /// <summary>
    /// Page size (1-100 items per page)
    /// </summary>
    /// <example>10</example>
    [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100")]
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Search query (searches in title, description, user names)
    /// </summary>
    /// <example>printer</example>
    [StringLength(200, ErrorMessage = "Search query cannot exceed 200 characters")]
    public string? Search { get; set; }

    /// <summary>
    /// Filter by status: New (1), Open (2), InProgress (3), OnHold (4), Resolved (5), Closed (6), Reopened (7)
    /// </summary>
    /// <example>Open</example>
    public TicketStatus? Status { get; set; }

    /// <summary>
    /// Filter by priority: Low (1), Medium (2), High (3), Critical (4)
    /// </summary>
    /// <example>High</example>
    public TicketPriority? Priority { get; set; }

    /// <summary>
    /// Filter by category: Hardware (1), Software (2), Network (3), Account (4), Email (5), Printer (6), Other (7)
    /// </summary>
    /// <example>Hardware</example>
    public TicketCategory? Category { get; set; }

    /// <summary>
    /// Filter by assigned technician ID
    /// </summary>
    /// <example>5</example>
    [Range(1, int.MaxValue, ErrorMessage = "AssignedToId must be a valid user ID")]
    public int? AssignedToId { get; set; }

    /// <summary>
    /// Filter by ticket creator ID
    /// </summary>
    /// <example>1</example>
    [Range(1, int.MaxValue, ErrorMessage = "CreatedById must be a valid user ID")]
    public int? CreatedById { get; set; }

    /// <summary>
    /// Filter only overdue tickets
    /// </summary>
    /// <example>true</example>
    public bool? IsOverdue { get; set; }

    /// <summary>
    /// Sort field: id, title, status, priority, category, createdAt, updatedAt, viewcount
    /// </summary>
    /// <example>createdAt</example>
    [RegularExpression(@"^(id|title|status|priority|category|createdAt|updatedAt|viewcount)$", 
        ErrorMessage = "SortBy must be one of: id, title, status, priority, category, createdAt, updatedAt, viewcount")]
    public string SortBy { get; set; } = "CreatedAt";

    /// <summary>
    /// Sort order: asc or desc
    /// </summary>
    /// <example>desc</example>
    [RegularExpression(@"^(asc|desc)$", ErrorMessage = "SortOrder must be 'asc' or 'desc'")]
    public string SortOrder { get; set; } = "desc";
}

public class DashboardStatsDto
{
    public int TotalTickets { get; set; }
    public int OpenTickets { get; set; }
    public int InProgressTickets { get; set; }
    public int ResolvedTickets { get; set; }
    public int ClosedTickets { get; set; }
    public int CriticalTickets { get; set; }
    public int OverdueTickets { get; set; }
    public double AverageResolutionTimeHours { get; set; }
    public List<CategoryStatsDto> ByCategory { get; set; } = new();
    public List<PriorityStatsDto> ByPriority { get; set; } = new();
}

public class CategoryStatsDto
{
    public string Category { get; set; } = string.Empty;
    public int Count { get; set; }
}

public class PriorityStatsDto
{
    public string Priority { get; set; } = string.Empty;
    public int Count { get; set; }
}
