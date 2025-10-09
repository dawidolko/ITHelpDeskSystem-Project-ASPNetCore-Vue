namespace HelpDeskAPI.Models;

public enum TicketStatus
{
    New = 1,
    Open = 2,
    InProgress = 3,
    OnHold = 4,
    Resolved = 5,
    Closed = 6,
    Reopened = 7
}

public enum TicketPriority
{
    Low = 1,
    Medium = 2,
    High = 3,
    Critical = 4
}

public enum TicketCategory
{
    Hardware = 1,
    Software = 2,
    Network = 3,
    Account = 4,
    Email = 5,
    Printer = 6,
    Other = 7
}
