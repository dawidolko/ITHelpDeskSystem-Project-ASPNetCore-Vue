using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskAPI.Models;

public class Ticket
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public TicketStatus Status { get; set; } = TicketStatus.New;

    [Required]
    public TicketPriority Priority { get; set; } = TicketPriority.Medium;

    [Required]
    public TicketCategory Category { get; set; } = TicketCategory.Other;

    // Foreign Keys
    [Required]
    public int CreatedById { get; set; }

    public int? AssignedToId { get; set; }

    // Navigation Properties
    [ForeignKey("CreatedById")]
    public User CreatedBy { get; set; } = null!;

    [ForeignKey("AssignedToId")]
    public User? AssignedTo { get; set; }

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    // Timestamps
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ResolvedAt { get; set; }
    public DateTime? ClosedAt { get; set; }

    // Additional fields
    [StringLength(500)]
    public string? ResolutionNotes { get; set; }

    public int ViewCount { get; set; } = 0;

    // Computed properties
    public TimeSpan? TimeToResolve => ResolvedAt.HasValue ? ResolvedAt.Value - CreatedAt : null;
    public bool IsOverdue => Status != TicketStatus.Resolved && Status != TicketStatus.Closed && 
                            CreatedAt.AddHours(GetSLAHours()) < DateTime.UtcNow;

    private int GetSLAHours()
    {
        return Priority switch
        {
            TicketPriority.Critical => 4,
            TicketPriority.High => 24,
            TicketPriority.Medium => 72,
            TicketPriority.Low => 168,
            _ => 72
        };
    }
}
