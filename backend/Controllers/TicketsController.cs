using HelpDeskAPI.Data;
using HelpDeskAPI.DTOs;
using HelpDeskAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace HelpDeskAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TicketsController : ControllerBase
{
    private readonly HelpDeskContext _context;
    private readonly ILogger<TicketsController> _logger;

    public TicketsController(HelpDeskContext context, ILogger<TicketsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Get all tickets with full SFWP support (Sort, Filter, Search, Pagination)
    /// </summary>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all tickets with SFWP",
        Description = "Retrieve tickets with support for Sorting, Filtering, Searching (Wyszukiwanie), and Pagination")]
    [SwaggerResponse(200, "Success", typeof(PagedResult<TicketDto>))]
    public async Task<ActionResult<PagedResult<TicketDto>>> GetTickets([FromQuery] TicketQueryParameters parameters)
    {
        try
        {
            var query = _context.Tickets
                .Include(t => t.CreatedBy)
                .Include(t => t.AssignedTo)
                .Include(t => t.Comments)
                .AsQueryable();

            // FILTERING (F)
            if (parameters.Status.HasValue)
            {
                query = query.Where(t => t.Status == parameters.Status.Value);
            }

            if (parameters.Priority.HasValue)
            {
                query = query.Where(t => t.Priority == parameters.Priority.Value);
            }

            if (parameters.Category.HasValue)
            {
                query = query.Where(t => t.Category == parameters.Category.Value);
            }

            if (parameters.AssignedToId.HasValue)
            {
                query = query.Where(t => t.AssignedToId == parameters.AssignedToId.Value);
            }

            if (parameters.CreatedById.HasValue)
            {
                query = query.Where(t => t.CreatedById == parameters.CreatedById.Value);
            }

            if (parameters.IsOverdue.HasValue && parameters.IsOverdue.Value)
            {
                var now = DateTime.UtcNow;
                query = query.Where(t => 
                    t.Status != TicketStatus.Resolved && 
                    t.Status != TicketStatus.Closed &&
                    (
                        (t.Priority == TicketPriority.Critical && t.CreatedAt.AddHours(4) < now) ||
                        (t.Priority == TicketPriority.High && t.CreatedAt.AddHours(24) < now) ||
                        (t.Priority == TicketPriority.Medium && t.CreatedAt.AddHours(72) < now) ||
                        (t.Priority == TicketPriority.Low && t.CreatedAt.AddHours(168) < now)
                    )
                );
            }

            // SEARCHING / WYSZUKIWANIE (W)
            if (!string.IsNullOrWhiteSpace(parameters.Search))
            {
                var searchLower = parameters.Search.ToLower();
                query = query.Where(t =>
                    t.Title.ToLower().Contains(searchLower) ||
                    t.Description.ToLower().Contains(searchLower) ||
                    t.CreatedBy.FirstName.ToLower().Contains(searchLower) ||
                    t.CreatedBy.LastName.ToLower().Contains(searchLower) ||
                    t.CreatedBy.Email.ToLower().Contains(searchLower) ||
                    (t.AssignedTo != null && (
                        t.AssignedTo.FirstName.ToLower().Contains(searchLower) ||
                        t.AssignedTo.LastName.ToLower().Contains(searchLower)
                    ))
                );
            }

            // Get total count before pagination
            var totalCount = await query.CountAsync();

            // SORTING (S)
            query = ApplySorting(query, parameters.SortBy, parameters.SortOrder);

            // PAGINATION (P)
            var pageSize = Math.Max(1, Math.Min(100, parameters.PageSize)); // Limit to 1-100
            var pageNumber = Math.Max(1, parameters.Page);
            
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(t => MapToTicketDto(t))
                .ToListAsync();

            var result = new PagedResult<TicketDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving tickets");
            return StatusCode(500, "An error occurred while retrieving tickets");
        }
    }

    /// <summary>
    /// Get a specific ticket by ID
    /// </summary>
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get ticket by ID", Description = "Retrieve detailed information about a specific ticket including comments")]
    [SwaggerResponse(200, "Success", typeof(TicketDetailDto))]
    [SwaggerResponse(404, "Ticket not found")]
    public async Task<ActionResult<TicketDetailDto>> GetTicket(int id)
    {
        var ticket = await _context.Tickets
            .Include(t => t.CreatedBy)
            .Include(t => t.AssignedTo)
            .Include(t => t.Comments)
                .ThenInclude(c => c.Author)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (ticket == null)
        {
            return NotFound(new { message = $"Ticket with ID {id} not found" });
        }

        // Increment view count
        ticket.ViewCount++;
        await _context.SaveChangesAsync();

        var result = new TicketDetailDto
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Description = ticket.Description,
            Status = ticket.Status.ToString(),
            Priority = ticket.Priority.ToString(),
            Category = ticket.Category.ToString(),
            CreatedBy = MapToUserSummary(ticket.CreatedBy),
            AssignedTo = ticket.AssignedTo != null ? MapToUserSummary(ticket.AssignedTo) : null,
            CreatedAt = ticket.CreatedAt,
            UpdatedAt = ticket.UpdatedAt,
            ResolvedAt = ticket.ResolvedAt,
            ClosedAt = ticket.ClosedAt,
            ResolutionNotes = ticket.ResolutionNotes,
            ViewCount = ticket.ViewCount,
            CommentCount = ticket.Comments.Count,
            IsOverdue = ticket.IsOverdue,
            Comments = ticket.Comments
                .OrderBy(c => c.CreatedAt)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    Content = c.Content,
                    Author = MapToUserSummary(c.Author),
                    CreatedAt = c.CreatedAt,
                    IsInternal = c.IsInternal
                })
                .ToList()
        };

        return Ok(result);
    }

    /// <summary>
    /// Create a new ticket
    /// </summary>
    [HttpPost]
    [SwaggerOperation(Summary = "Create new ticket", Description = "Create a new help desk ticket")]
    [SwaggerResponse(201, "Ticket created", typeof(TicketDto))]
    [SwaggerResponse(400, "Invalid request")]
    public async Task<ActionResult<TicketDto>> CreateTicket([FromBody] CreateTicketDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Verify user exists
        var user = await _context.Users.FindAsync(dto.CreatedById);
        if (user == null)
        {
            return BadRequest(new { message = "Invalid user ID" });
        }

        var ticket = new Ticket
        {
            Title = dto.Title,
            Description = dto.Description,
            Priority = dto.Priority,
            Category = dto.Category,
            CreatedById = dto.CreatedById,
            Status = TicketStatus.New,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        // Load navigation properties
        await _context.Entry(ticket).Reference(t => t.CreatedBy).LoadAsync();

        var result = MapToTicketDto(ticket);

        return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, result);
    }

    /// <summary>
    /// Update an existing ticket
    /// </summary>
    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update ticket", Description = "Update ticket properties including status, priority, assignment, etc.")]
    [SwaggerResponse(200, "Ticket updated", typeof(TicketDto))]
    [SwaggerResponse(404, "Ticket not found")]
    public async Task<ActionResult<TicketDto>> UpdateTicket(int id, [FromBody] UpdateTicketDto dto)
    {
        var ticket = await _context.Tickets
            .Include(t => t.CreatedBy)
            .Include(t => t.AssignedTo)
            .Include(t => t.Comments)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (ticket == null)
        {
            return NotFound(new { message = $"Ticket with ID {id} not found" });
        }

        // Update fields if provided
        if (!string.IsNullOrWhiteSpace(dto.Title))
        {
            ticket.Title = dto.Title;
        }

        if (!string.IsNullOrWhiteSpace(dto.Description))
        {
            ticket.Description = dto.Description;
        }

        if (dto.Status.HasValue)
        {
            ticket.Status = dto.Status.Value;

            // Set timestamps based on status
            if (dto.Status.Value == TicketStatus.Resolved && !ticket.ResolvedAt.HasValue)
            {
                ticket.ResolvedAt = DateTime.UtcNow;
            }
            else if (dto.Status.Value == TicketStatus.Closed && !ticket.ClosedAt.HasValue)
            {
                ticket.ClosedAt = DateTime.UtcNow;
            }
        }

        if (dto.Priority.HasValue)
        {
            ticket.Priority = dto.Priority.Value;
        }

        if (dto.Category.HasValue)
        {
            ticket.Category = dto.Category.Value;
        }

        if (dto.AssignedToId.HasValue)
        {
            // Verify assignee exists
            var assignee = await _context.Users.FindAsync(dto.AssignedToId.Value);
            if (assignee == null)
            {
                return BadRequest(new { message = "Invalid assignee user ID" });
            }
            ticket.AssignedToId = dto.AssignedToId.Value;

            // Load the assignee
            await _context.Entry(ticket).Reference(t => t.AssignedTo).LoadAsync();
        }

        if (!string.IsNullOrWhiteSpace(dto.ResolutionNotes))
        {
            ticket.ResolutionNotes = dto.ResolutionNotes;
        }

        ticket.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        var result = MapToTicketDto(ticket);

        return Ok(result);
    }

    /// <summary>
    /// Delete a ticket
    /// </summary>
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete ticket", Description = "Permanently delete a ticket and all its comments")]
    [SwaggerResponse(204, "Ticket deleted")]
    [SwaggerResponse(404, "Ticket not found")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket == null)
        {
            return NotFound(new { message = $"Ticket with ID {id} not found" });
        }

        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Add a comment to a ticket
    /// </summary>
    [HttpPost("{id}/comments")]
    [SwaggerOperation(Summary = "Add comment", Description = "Add a new comment to a ticket")]
    [SwaggerResponse(201, "Comment added", typeof(CommentDto))]
    [SwaggerResponse(404, "Ticket not found")]
    public async Task<ActionResult<CommentDto>> AddComment(int id, [FromBody] CreateCommentDto dto)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket == null)
        {
            return NotFound(new { message = $"Ticket with ID {id} not found" });
        }

        var author = await _context.Users.FindAsync(dto.AuthorId);
        if (author == null)
        {
            return BadRequest(new { message = "Invalid author user ID" });
        }

        var comment = new Comment
        {
            TicketId = id,
            AuthorId = dto.AuthorId,
            Content = dto.Content,
            IsInternal = dto.IsInternal,
            CreatedAt = DateTime.UtcNow
        };

        _context.Comments.Add(comment);
        ticket.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        // Load author
        await _context.Entry(comment).Reference(c => c.Author).LoadAsync();

        var result = new CommentDto
        {
            Id = comment.Id,
            Content = comment.Content,
            Author = MapToUserSummary(comment.Author),
            CreatedAt = comment.CreatedAt,
            IsInternal = comment.IsInternal
        };

        return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, result);
    }

    /// <summary>
    /// Get dashboard statistics
    /// </summary>
    [HttpGet("statistics")]
    [SwaggerOperation(Summary = "Get statistics", Description = "Get dashboard statistics including ticket counts by status, priority, and category")]
    [SwaggerResponse(200, "Success", typeof(DashboardStatsDto))]
    public async Task<ActionResult<DashboardStatsDto>> GetStatistics()
    {
        var tickets = await _context.Tickets.ToListAsync();

        var stats = new DashboardStatsDto
        {
            TotalTickets = tickets.Count,
            OpenTickets = tickets.Count(t => t.Status == TicketStatus.Open),
            InProgressTickets = tickets.Count(t => t.Status == TicketStatus.InProgress),
            ResolvedTickets = tickets.Count(t => t.Status == TicketStatus.Resolved),
            ClosedTickets = tickets.Count(t => t.Status == TicketStatus.Closed),
            CriticalTickets = tickets.Count(t => t.Priority == TicketPriority.Critical),
            OverdueTickets = tickets.Count(t => t.IsOverdue),
            AverageResolutionTimeHours = tickets
                .Where(t => t.ResolvedAt.HasValue)
                .Select(t => (t.ResolvedAt!.Value - t.CreatedAt).TotalHours)
                .DefaultIfEmpty(0)
                .Average(),
            ByCategory = Enum.GetValues<TicketCategory>()
                .Select(c => new CategoryStatsDto
                {
                    Category = c.ToString(),
                    Count = tickets.Count(t => t.Category == c)
                })
                .Where(s => s.Count > 0)
                .ToList(),
            ByPriority = Enum.GetValues<TicketPriority>()
                .Select(p => new PriorityStatsDto
                {
                    Priority = p.ToString(),
                    Count = tickets.Count(t => t.Priority == p)
                })
                .Where(s => s.Count > 0)
                .ToList()
        };

        return Ok(stats);
    }

    // Helper methods
    private IQueryable<Ticket> ApplySorting(IQueryable<Ticket> query, string sortBy, string sortOrder)
    {
        var isDescending = sortOrder.ToLower() == "desc";

        Expression<Func<Ticket, object>> sortExpression = sortBy.ToLower() switch
        {
            "title" => t => t.Title,
            "status" => t => t.Status,
            "priority" => t => t.Priority,
            "category" => t => t.Category,
            "createdat" => t => t.CreatedAt,
            "updatedat" => t => t.UpdatedAt,
            "viewcount" => t => t.ViewCount,
            _ => t => t.CreatedAt
        };

        return isDescending
            ? query.OrderByDescending(sortExpression)
            : query.OrderBy(sortExpression);
    }

    private static TicketDto MapToTicketDto(Ticket ticket)
    {
        return new TicketDto
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Description = ticket.Description,
            Status = ticket.Status.ToString(),
            Priority = ticket.Priority.ToString(),
            Category = ticket.Category.ToString(),
            CreatedBy = MapToUserSummary(ticket.CreatedBy),
            AssignedTo = ticket.AssignedTo != null ? MapToUserSummary(ticket.AssignedTo) : null,
            CreatedAt = ticket.CreatedAt,
            UpdatedAt = ticket.UpdatedAt,
            ResolvedAt = ticket.ResolvedAt,
            ClosedAt = ticket.ClosedAt,
            ResolutionNotes = ticket.ResolutionNotes,
            ViewCount = ticket.ViewCount,
            CommentCount = ticket.Comments?.Count ?? 0,
            IsOverdue = ticket.IsOverdue
        };
    }

    private static UserSummaryDto MapToUserSummary(User user)
    {
        return new UserSummaryDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role,
            Department = user.Department
        };
    }
}
