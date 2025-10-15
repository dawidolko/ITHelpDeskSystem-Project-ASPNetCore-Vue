using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskAPI.Models;

public class Comment
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Content { get; set; } = string.Empty;

    [Required]
    public int TicketId { get; set; }

    [Required]
    public int AuthorId { get; set; }

    [ForeignKey("TicketId")]
    public Ticket Ticket { get; set; } = null!;

    [ForeignKey("AuthorId")]
    public User Author { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsInternal { get; set; } = false;
}
