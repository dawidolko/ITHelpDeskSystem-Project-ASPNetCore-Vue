import apiClient from "./api";
import type {
  Ticket,
  TicketDetail,
  CreateTicketRequest,
  UpdateTicketRequest,
  CreateCommentRequest,
  TicketQueryParams,
  PagedResult,
  DashboardStats,
  UserSummary,
  Comment,
} from "../types/ticket.types";

export const ticketService = {
  // Get all tickets with SFWP
  async getTickets(params?: TicketQueryParams): Promise<PagedResult<Ticket>> {
    const response = await apiClient.get<PagedResult<Ticket>>("/tickets", {
      params,
    });
    return response.data;
  },

  // Get single ticket by ID
  async getTicket(id: number): Promise<TicketDetail> {
    const response = await apiClient.get<TicketDetail>(`/tickets/${id}`);
    return response.data;
  },

  // Create new ticket
  async createTicket(data: CreateTicketRequest): Promise<Ticket> {
    const response = await apiClient.post<Ticket>("/tickets", data);
    return response.data;
  },

  // Update ticket
  async updateTicket(id: number, data: UpdateTicketRequest): Promise<Ticket> {
    const response = await apiClient.put<Ticket>(`/tickets/${id}`, data);
    return response.data;
  },

  // Delete ticket
  async deleteTicket(id: number): Promise<void> {
    await apiClient.delete(`/tickets/${id}`);
  },

  // Add comment to ticket
  async addComment(
    ticketId: number,
    data: CreateCommentRequest
  ): Promise<Comment> {
    const response = await apiClient.post<Comment>(
      `/tickets/${ticketId}/comments`,
      data
    );
    return response.data;
  },

  // Get dashboard statistics
  async getStatistics(): Promise<DashboardStats> {
    const response = await apiClient.get<DashboardStats>("/tickets/statistics");
    return response.data;
  },
};

export const userService = {
  // Get all users
  async getUsers(role?: string): Promise<UserSummary[]> {
    const response = await apiClient.get<UserSummary[]>("/users", {
      params: { role },
    });
    return response.data;
  },

  // Get technicians
  async getTechnicians(): Promise<UserSummary[]> {
    const response = await apiClient.get<UserSummary[]>("/users/technicians");
    return response.data;
  },

  // Get user by ID
  async getUser(id: number): Promise<UserSummary> {
    const response = await apiClient.get<UserSummary>(`/users/${id}`);
    return response.data;
  },
};
