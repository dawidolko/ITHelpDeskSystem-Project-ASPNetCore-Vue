<script setup lang="ts">
import { onMounted, ref, computed, watch } from "vue";
import { useRouter, useRoute } from "vue-router";
import { useTicketStore } from "../../stores/ticketStore";
import { useUserStore } from "../../stores/userStore";
import NavigationGlobal from "../../components/navigation-global.vue";
import FooterGlobal from "../../components/footer-global.vue";
import StatusBadge from "../../components/StatusBadge.vue";
import SearchInput from "../../components/SearchInput.vue";
import Pagination from "../../components/Pagination.vue";
import {
  TicketStatus,
  TicketPriority,
  TicketCategory,
} from "../../types/ticket.types";

const router = useRouter();
const route = useRoute();
const ticketStore = useTicketStore();
const userStore = useUserStore();

const searchQuery = ref("");
const selectedStatus = ref<TicketStatus | "">("");
const selectedPriority = ref<TicketPriority | "">("");
const selectedCategory = ref<TicketCategory | "">("");
const selectedAssignee = ref<number | "">("");
const showOverdueOnly = ref(false);
const sortBy = ref("createdAt");
const sortOrder = ref<"asc" | "desc">("desc");
const currentPage = ref(1);
const pageSize = ref(10);

const tickets = computed(() => ticketStore.tickets);
const pagedResult = computed(() => ticketStore.pagedResult);
const loading = computed(() => ticketStore.loading);
const technicians = computed(() => userStore.technicians);

onMounted(async () => {
  userStore.loadCurrentUser();
  await userStore.fetchTechnicians();

  if (route.query.status)
    selectedStatus.value = route.query.status as TicketStatus;
  if (route.query.priority)
    selectedPriority.value = route.query.priority as TicketPriority;
  if (route.query.category)
    selectedCategory.value = route.query.category as TicketCategory;
  if (route.query.search) searchQuery.value = route.query.search as string;

  await fetchTickets();
});

const fetchTickets = async () => {
  const params: any = {
    page: currentPage.value,
    pageSize: pageSize.value,
    sortBy: sortBy.value,
    sortOrder: sortOrder.value,
  };

  if (searchQuery.value) params.search = searchQuery.value;
  if (selectedStatus.value) params.status = selectedStatus.value;
  if (selectedPriority.value) params.priority = selectedPriority.value;
  if (selectedCategory.value) params.category = selectedCategory.value;
  if (selectedAssignee.value) params.assignedToId = selectedAssignee.value;
  if (showOverdueOnly.value) params.isOverdue = true;

  await ticketStore.fetchTickets(params);
};

const handleSearch = (value: string) => {
  searchQuery.value = value;
  currentPage.value = 1;
  fetchTickets();
};

const handleFilterChange = () => {
  currentPage.value = 1;
  fetchTickets();
};

const handleSort = (field: string) => {
  if (sortBy.value === field) {
    sortOrder.value = sortOrder.value === "asc" ? "desc" : "asc";
  } else {
    sortBy.value = field;
    sortOrder.value = "desc";
  }
  fetchTickets();
};

const handlePageChange = (page: number) => {
  currentPage.value = page;
  fetchTickets();
  window.scrollTo({ top: 0, behavior: "smooth" });
};

const clearFilters = () => {
  searchQuery.value = "";
  selectedStatus.value = "";
  selectedPriority.value = "";
  selectedCategory.value = "";
  selectedAssignee.value = "";
  showOverdueOnly.value = false;
  currentPage.value = 1;
  fetchTickets();
};

const goToTicket = (id: number) => {
  router.push(`/tickets/${id}`);
};

const formatDate = (date: string): string => {
  return new Date(date).toLocaleDateString("pl-PL", {
    year: "numeric",
    month: "short",
    day: "numeric",
    hour: "2-digit",
    minute: "2-digit",
  });
};

const getSortIcon = (field: string): string => {
  if (sortBy.value !== field) return "⇅";
  return sortOrder.value === "asc" ? "↑" : "↓";
};
</script>

<template>
  <div class="min-h-screen w-screen bg-black text-white">
    <NavigationGlobal color="black" />

    <section class="flex w-full items-center justify-center py-16">
      <div class="w-4/5 max-w-7xl md:w-11/12 lg:w-4/5">
        <!-- Header -->
        <div class="mb-8">
          <h1 class="mb-4 text-4xl font-extrabold tracking-tight md:text-5xl">
            All <span class="text-k-main">Tickets</span>
          </h1>
          <p class="text-lg text-zinc-400">
            Browse, search, filter, and sort all IT support tickets
          </p>
        </div>

        <!-- Search Bar -->
        <div class="mb-6">
          <SearchInput
            v-model="searchQuery"
            @search="handleSearch"
            placeholder="Search tickets by title, description, or user..." />
        </div>

        <!-- Filters -->
        <div class="mb-8 rounded-lg border border-zinc-800 bg-zinc-900 p-6">
          <h2 class="mb-4 text-lg font-bold uppercase tracking-wide">
            Filters & Sorting
          </h2>
          <div class="grid gap-4 md:grid-cols-2 lg:grid-cols-5">
            <!-- Status Filter -->
            <div>
              <label class="mb-2 block text-sm font-semibold text-zinc-400"
                >Status</label
              >
              <select
                v-model="selectedStatus"
                @change="handleFilterChange"
                class="w-full rounded-lg border border-zinc-700 bg-black px-4 py-2 text-white transition duration-300 focus:border-k-main focus:outline-none">
                <option value="">All Statuses</option>
                <option
                  v-for="status in Object.values(TicketStatus)"
                  :key="status"
                  :value="status">
                  {{ status }}
                </option>
              </select>
            </div>

            <!-- Priority Filter -->
            <div>
              <label class="mb-2 block text-sm font-semibold text-zinc-400"
                >Priority</label
              >
              <select
                v-model="selectedPriority"
                @change="handleFilterChange"
                class="w-full rounded-lg border border-zinc-700 bg-black px-4 py-2 text-white transition duration-300 focus:border-k-main focus:outline-none">
                <option value="">All Priorities</option>
                <option
                  v-for="priority in Object.values(TicketPriority)"
                  :key="priority"
                  :value="priority">
                  {{ priority }}
                </option>
              </select>
            </div>

            <!-- Category Filter -->
            <div>
              <label class="mb-2 block text-sm font-semibold text-zinc-400"
                >Category</label
              >
              <select
                v-model="selectedCategory"
                @change="handleFilterChange"
                class="w-full rounded-lg border border-zinc-700 bg-black px-4 py-2 text-white transition duration-300 focus:border-k-main focus:outline-none">
                <option value="">All Categories</option>
                <option
                  v-for="category in Object.values(TicketCategory)"
                  :key="category"
                  :value="category">
                  {{ category }}
                </option>
              </select>
            </div>

            <!-- Assignee Filter -->
            <div>
              <label class="mb-2 block text-sm font-semibold text-zinc-400"
                >Assigned To</label
              >
              <select
                v-model="selectedAssignee"
                @change="handleFilterChange"
                class="w-full rounded-lg border border-zinc-700 bg-black px-4 py-2 text-white transition duration-300 focus:border-k-main focus:outline-none">
                <option value="">All Technicians</option>
                <option
                  v-for="tech in technicians"
                  :key="tech.id"
                  :value="tech.id">
                  {{ tech.fullName }}
                </option>
              </select>
            </div>

            <!-- Overdue Filter -->
            <div>
              <label class="mb-2 block text-sm font-semibold text-zinc-400"
                >Show Only</label
              >
              <label class="flex items-center gap-2">
                <input
                  type="checkbox"
                  v-model="showOverdueOnly"
                  @change="handleFilterChange"
                  class="h-5 w-5 rounded border-zinc-700 bg-black text-k-main focus:ring-k-main" />
                <span class="text-white">Overdue Tickets</span>
              </label>
            </div>
          </div>

          <div class="mt-4 flex items-center justify-between">
            <button
              @click="clearFilters"
              class="text-sm font-semibold uppercase tracking-wide text-zinc-400 transition duration-300 hover:text-k-main">
              Clear All Filters
            </button>
            <p class="text-sm text-zinc-400">
              Showing
              <span class="font-bold text-white">{{
                pagedResult?.items.length || 0
              }}</span>
              of
              <span class="font-bold text-white">{{
                pagedResult?.totalCount || 0
              }}</span>
              tickets
            </p>
          </div>
        </div>

        <!-- Loading State -->
        <div v-if="loading" class="py-20 text-center">
          <div
            class="inline-block h-12 w-12 animate-spin rounded-full border-4 border-solid border-k-main border-r-transparent"></div>
          <p class="mt-4 text-lg text-zinc-400">Loading tickets...</p>
        </div>

        <!-- Tickets List -->
        <div v-else-if="tickets.length === 0" class="py-20 text-center">
          <p class="text-xl text-zinc-400">
            No tickets found with current filters.
          </p>
          <button
            @click="clearFilters"
            class="mt-4 font-semibold uppercase tracking-wide text-k-main transition duration-300 hover:underline">
            Clear Filters
          </button>
        </div>

        <!-- Desktop Table View -->
        <div
          v-else
          class="hidden overflow-hidden rounded-lg border border-zinc-800 lg:block">
          <table class="w-full">
            <thead class="bg-zinc-900">
              <tr>
                <th
                  @click="handleSort('id')"
                  class="cursor-pointer px-6 py-4 text-left text-sm font-bold uppercase tracking-wide text-zinc-400 transition duration-300 hover:text-k-main">
                  ID {{ getSortIcon("id") }}
                </th>
                <th
                  @click="handleSort('title')"
                  class="cursor-pointer px-6 py-4 text-left text-sm font-bold uppercase tracking-wide text-zinc-400 transition duration-300 hover:text-k-main">
                  Title {{ getSortIcon("title") }}
                </th>
                <th
                  @click="handleSort('status')"
                  class="cursor-pointer px-6 py-4 text-left text-sm font-bold uppercase tracking-wide text-zinc-400 transition duration-300 hover:text-k-main">
                  Status {{ getSortIcon("status") }}
                </th>
                <th
                  @click="handleSort('priority')"
                  class="cursor-pointer px-6 py-4 text-left text-sm font-bold uppercase tracking-wide text-zinc-400 transition duration-300 hover:text-k-main">
                  Priority {{ getSortIcon("priority") }}
                </th>
                <th
                  class="px-6 py-4 text-left text-sm font-bold uppercase tracking-wide text-zinc-400">
                  Category
                </th>
                <th
                  @click="handleSort('createdAt')"
                  class="cursor-pointer px-6 py-4 text-left text-sm font-bold uppercase tracking-wide text-zinc-400 transition duration-300 hover:text-k-main">
                  Created {{ getSortIcon("createdAt") }}
                </th>
                <th
                  class="px-6 py-4 text-left text-sm font-bold uppercase tracking-wide text-zinc-400">
                  Actions
                </th>
              </tr>
            </thead>
            <tbody>
              <tr
                v-for="ticket in tickets"
                :key="ticket.id"
                class="border-t border-zinc-800 transition duration-300 hover:bg-zinc-900">
                <td class="px-6 py-4 font-mono text-sm font-bold text-k-main">
                  #{{ ticket.id }}
                </td>
                <td class="px-6 py-4">
                  <div>
                    <p class="font-semibold text-white">{{ ticket.title }}</p>
                    <p class="text-sm text-zinc-500">
                      by {{ ticket.createdBy.fullName }}
                    </p>
                  </div>
                </td>
                <td class="px-6 py-4">
                  <StatusBadge type="status" :value="ticket.status" small />
                </td>
                <td class="px-6 py-4">
                  <StatusBadge type="priority" :value="ticket.priority" small />
                </td>
                <td class="px-6 py-4 text-sm text-zinc-400">
                  {{ ticket.category }}
                </td>
                <td class="px-6 py-4 text-sm text-zinc-400">
                  {{ formatDate(ticket.createdAt) }}
                </td>
                <td class="px-6 py-4">
                  <button
                    @click="goToTicket(ticket.id)"
                    class="rounded-lg bg-k-main px-4 py-2 text-sm font-bold uppercase tracking-wide text-white transition duration-300 hover:scale-105 active:translate-y-0.5">
                    View
                  </button>
                </td>
              </tr>
            </tbody>
          </table>
        </div>

        <!-- Mobile Card View -->
        <div v-if="!loading && tickets.length > 0" class="space-y-4 lg:hidden">
          <div
            v-for="ticket in tickets"
            :key="ticket.id"
            @click="goToTicket(ticket.id)"
            class="cursor-pointer rounded-lg border border-zinc-800 bg-zinc-900 p-6 transition duration-300 hover:border-k-main">
            <div class="mb-4 flex items-start justify-between">
              <h3 class="text-xl font-bold text-white">
                #{{ ticket.id }} - {{ ticket.title }}
              </h3>
              <div class="flex gap-2">
                <StatusBadge type="status" :value="ticket.status" small />
                <StatusBadge type="priority" :value="ticket.priority" small />
              </div>
            </div>
            <div class="space-y-2 text-sm text-zinc-400">
              <p>📁 {{ ticket.category }}</p>
              <p>👤 {{ ticket.createdBy.fullName }}</p>
              <p>🕒 {{ formatDate(ticket.createdAt) }}</p>
              <p v-if="ticket.assignedTo">
                👨‍💻 Assigned: {{ ticket.assignedTo.fullName }}
              </p>
            </div>
          </div>
        </div>

        <!-- Pagination -->
        <Pagination
          v-if="pagedResult"
          :current-page="pagedResult.pageNumber"
          :total-pages="pagedResult.totalPages"
          :has-next="pagedResult.hasNextPage"
          :has-previous="pagedResult.hasPreviousPage"
          @page-changed="handlePageChange" />
      </div>
    </section>

    <FooterGlobal />
  </div>
</template>
