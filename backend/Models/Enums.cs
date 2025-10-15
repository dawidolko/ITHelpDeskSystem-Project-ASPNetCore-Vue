using System.ComponentModel;

namespace HelpDeskAPI.Models;

/// <summary>
/// Ticket status values
/// </summary>
public enum TicketStatus
{
    /// <summary>
    /// Newly created ticket (not yet reviewed)
    /// </summary>
    [Description("Newly created ticket")]
    New = 1,
    
    /// <summary>
    /// Ticket has been reviewed and opened for work
    /// </summary>
    [Description("Ticket opened")]
    Open = 2,
    
    /// <summary>
    /// Work is currently in progress
    /// </summary>
    [Description("Work in progress")]
    InProgress = 3,
    
    /// <summary>
    /// Ticket is temporarily on hold (waiting for user, parts, etc.)
    /// </summary>
    [Description("On hold")]
    OnHold = 4,
    
    /// <summary>
    /// Issue has been resolved
    /// </summary>
    [Description("Resolved")]
    Resolved = 5,
    
    /// <summary>
    /// Ticket is closed (final state)
    /// </summary>
    [Description("Closed")]
    Closed = 6,
    
    /// <summary>
    /// Previously resolved/closed ticket that has been reopened
    /// </summary>
    [Description("Reopened")]
    Reopened = 7
}

/// <summary>
/// Ticket priority levels with SLA times
/// </summary>
public enum TicketPriority
{
    /// <summary>
    /// Low priority (SLA: 168 hours / 7 days)
    /// </summary>
    [Description("Low priority - 7 days SLA")]
    Low = 1,
    
    /// <summary>
    /// Medium priority (SLA: 72 hours / 3 days)
    /// </summary>
    [Description("Medium priority - 3 days SLA")]
    Medium = 2,
    
    /// <summary>
    /// High priority (SLA: 24 hours / 1 day)
    /// </summary>
    [Description("High priority - 24 hours SLA")]
    High = 3,
    
    /// <summary>
    /// Critical priority (SLA: 4 hours)
    /// </summary>
    [Description("Critical priority - 4 hours SLA")]
    Critical = 4
}

/// <summary>
/// Ticket category types
/// </summary>
public enum TicketCategory
{
    /// <summary>
    /// Hardware issues (computers, monitors, keyboards, etc.)
    /// </summary>
    [Description("Hardware issues")]
    Hardware = 1,
    
    /// <summary>
    /// Software issues (applications, OS, licenses, etc.)
    /// </summary>
    [Description("Software issues")]
    Software = 2,
    
    /// <summary>
    /// Network connectivity issues (WiFi, ethernet, VPN, etc.)
    /// </summary>
    [Description("Network issues")]
    Network = 3,
    
    /// <summary>
    /// Account and access issues (passwords, permissions, etc.)
    /// </summary>
    [Description("Account access issues")]
    Account = 4,
    
    /// <summary>
    /// Email related issues (Outlook, webmail, sending/receiving, etc.)
    /// </summary>
    [Description("Email issues")]
    Email = 5,
    
    /// <summary>
    /// Printer and scanner issues
    /// </summary>
    [Description("Printer/Scanner issues")]
    Printer = 6,
    
    /// <summary>
    /// Other issues not fitting above categories
    /// </summary>
    [Description("Other issues")]
    Other = 7
}
