// Ticket types
export interface Ticket {
  id: number;
  title: string;
  description: string;
  status: TicketStatus;
  priority: TicketPriority;
  category: TicketCategory;
  createdBy: UserSummary;
  assignedTo?: UserSummary | null;
  createdAt: string;
  updatedAt: string;
  resolvedAt?: string | null;
  closedAt?: string | null;
  resolutionNotes?: string | null;
  viewCount: number;
  commentCount: number;
  isOverdue: boolean;
}

export interface TicketDetail extends Ticket {
  comments: Comment[];
}

export interface UserSummary {
  id: number;
  fullName: string;
  email: string;
  role: string;
  department?: string | null;
}

export interface Comment {
  id: number;
  content: string;
  author: UserSummary;
  createdAt: string;
  isInternal: boolean;
}

// Enums
export enum TicketStatus {
  New = "New",
  Open = "Open",
  InProgress = "InProgress",
  OnHold = "OnHold",
  Resolved = "Resolved",
  Closed = "Closed",
  Reopened = "Reopened",
}

export enum TicketPriority {
  Low = "Low",
  Medium = "Medium",
  High = "High",
  Critical = "Critical",
}

export enum TicketCategory {
  Hardware = "Hardware",
  Software = "Software",
  Network = "Network",
  Account = "Account",
  Email = "Email",
  Printer = "Printer",
  Other = "Other",
}

// Request DTOs
export interface CreateTicketRequest {
  title: string;
  description: string;
  priority: TicketPriority;
  category: TicketCategory;
  createdById: number;
}

export interface UpdateTicketRequest {
  title?: string;
  description?: string;
  status?: TicketStatus;
  priority?: TicketPriority;
  category?: TicketCategory;
  assignedToId?: number | null;
  resolutionNotes?: string;
}

export interface CreateCommentRequest {
  content: string;
  authorId: number;
  isInternal?: boolean;
}

// Query parameters
export interface TicketQueryParams {
  page?: number;
  pageSize?: number;
  search?: string;
  status?: TicketStatus;
  priority?: TicketPriority;
  category?: TicketCategory;
  assignedToId?: number;
  createdById?: number;
  isOverdue?: boolean;
  sortBy?: string;
  sortOrder?: "asc" | "desc";
}

// Pagination
export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

// Statistics
export interface DashboardStats {
  totalTickets: number;
  openTickets: number;
  inProgressTickets: number;
  resolvedTickets: number;
  closedTickets: number;
  criticalTickets: number;
  overdueTickets: number;
  averageResolutionTimeHours: number;
  byCategory: CategoryStats[];
  byPriority: PriorityStats[];
}

export interface CategoryStats {
  category: string;
  count: number;
}

export interface PriorityStats {
  priority: string;
  count: number;
}
