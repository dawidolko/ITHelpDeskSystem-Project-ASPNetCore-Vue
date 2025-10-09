using HelpDeskAPI.Models;

namespace HelpDeskAPI.DTOs;

// Request DTOs
public class CreateTicketDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public TicketPriority Priority { get; set; } = TicketPriority.Medium;
    public TicketCategory Category { get; set; }
    public int CreatedById { get; set; }
}

public class UpdateTicketDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public TicketStatus? Status { get; set; }
    public TicketPriority? Priority { get; set; }
    public TicketCategory? Category { get; set; }
    public int? AssignedToId { get; set; }
    public string? ResolutionNotes { get; set; }
}

// Response DTOs
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

public class CreateCommentDto
{
    public required string Content { get; set; }
    public int AuthorId { get; set; }
    public bool IsInternal { get; set; } = false;
}

// Pagination
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

// Query Parameters
public class TicketQueryParameters
{
    // Pagination
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    // Search
    public string? Search { get; set; }

    // Filtering
    public TicketStatus? Status { get; set; }
    public TicketPriority? Priority { get; set; }
    public TicketCategory? Category { get; set; }
    public int? AssignedToId { get; set; }
    public int? CreatedById { get; set; }
    public bool? IsOverdue { get; set; }

    // Sorting
    public string SortBy { get; set; } = "CreatedAt";
    public string SortOrder { get; set; } = "desc"; // asc or desc
}

// Statistics DTOs
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
