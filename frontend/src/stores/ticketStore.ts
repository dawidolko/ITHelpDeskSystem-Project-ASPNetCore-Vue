import { defineStore } from "pinia";
import { ref } from "vue";
import type {
  Ticket,
  TicketDetail,
  TicketQueryParams,
  PagedResult,
  CreateTicketRequest,
  UpdateTicketRequest,
  CreateCommentRequest,
  DashboardStats,
} from "../types/ticket.types";
import { ticketService } from "../services/ticketService";

export const useTicketStore = defineStore("ticket", () => {
  const tickets = ref<Ticket[]>([]);
  const currentTicket = ref<TicketDetail | null>(null);
  const pagedResult = ref<PagedResult<Ticket> | null>(null);
  const statistics = ref<DashboardStats | null>(null);
  const loading = ref(false);
  const error = ref<string | null>(null);

  async function fetchTickets(params?: TicketQueryParams) {
    loading.value = true;
    error.value = null;
    try {
      pagedResult.value = await ticketService.getTickets(params);
      tickets.value = pagedResult.value.items;
    } catch (err: any) {
      error.value = err.response?.data?.message || "Failed to fetch tickets";
      console.error("Error fetching tickets:", err);
    } finally {
      loading.value = false;
    }
  }

  async function fetchTicket(id: number) {
    loading.value = true;
    error.value = null;
    try {
      currentTicket.value = await ticketService.getTicket(id);
    } catch (err: any) {
      error.value = err.response?.data?.message || "Failed to fetch ticket";
      console.error("Error fetching ticket:", err);
    } finally {
      loading.value = false;
    }
  }

  async function createTicket(data: CreateTicketRequest) {
    loading.value = true;
    error.value = null;
    try {
      const newTicket = await ticketService.createTicket(data);
      tickets.value.unshift(newTicket);
      return newTicket;
    } catch (err: any) {
      error.value = err.response?.data?.message || "Failed to create ticket";
      console.error("Error creating ticket:", err);
      throw err;
    } finally {
      loading.value = false;
    }
  }

  async function updateTicket(id: number, data: UpdateTicketRequest) {
    loading.value = true;
    error.value = null;
    try {
      const updatedTicket = await ticketService.updateTicket(id, data);

      const index = tickets.value.findIndex((t) => t.id === id);
      if (index !== -1) {
        tickets.value[index] = updatedTicket;
      }

      if (currentTicket.value?.id === id) {
        currentTicket.value = { ...currentTicket.value, ...updatedTicket };
      }

      return updatedTicket;
    } catch (err: any) {
      error.value = err.response?.data?.message || "Failed to update ticket";
      console.error("Error updating ticket:", err);
      throw err;
    } finally {
      loading.value = false;
    }
  }

  async function deleteTicket(id: number) {
    loading.value = true;
    error.value = null;
    try {
      await ticketService.deleteTicket(id);
      tickets.value = tickets.value.filter((t) => t.id !== id);
      if (currentTicket.value?.id === id) {
        currentTicket.value = null;
      }
    } catch (err: any) {
      error.value = err.response?.data?.message || "Failed to delete ticket";
      console.error("Error deleting ticket:", err);
      throw err;
    } finally {
      loading.value = false;
    }
  }

  async function addComment(ticketId: number, data: CreateCommentRequest) {
    loading.value = true;
    error.value = null;
    try {
      const newComment = await ticketService.addComment(ticketId, data);

      if (currentTicket.value?.id === ticketId) {
        currentTicket.value.comments.push(newComment);
        currentTicket.value.commentCount++;
      }

      return newComment;
    } catch (err: any) {
      error.value = err.response?.data?.message || "Failed to add comment";
      console.error("Error adding comment:", err);
      throw err;
    } finally {
      loading.value = false;
    }
  }

  async function fetchStatistics() {
    loading.value = true;
    error.value = null;
    try {
      statistics.value = await ticketService.getStatistics();
    } catch (err: any) {
      error.value = err.response?.data?.message || "Failed to fetch statistics";
      console.error("Error fetching statistics:", err);
    } finally {
      loading.value = false;
    }
  }

  function clearError() {
    error.value = null;
  }

  return {
    tickets,
    currentTicket,
    pagedResult,
    statistics,
    loading,
    error,
    fetchTickets,
    fetchTicket,
    createTicket,
    updateTicket,
    deleteTicket,
    addComment,
    fetchStatistics,
    clearError,
  };
});
