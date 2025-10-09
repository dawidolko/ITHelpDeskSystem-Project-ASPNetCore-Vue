<script setup lang="ts">
import { onMounted, computed } from "vue";
import { useRouter } from "vue-router";
import { useTicketStore } from "../../stores/ticketStore";
import { useUserStore } from "../../stores/userStore";
import NavigationGlobal from "../../components/navigation-global.vue";
import FooterGlobal from "../../components/footer-global.vue";
import StatusBadge from "../../components/StatusBadge.vue";

const router = useRouter();
const ticketStore = useTicketStore();
const userStore = useUserStore();

onMounted(async () => {
  userStore.loadCurrentUser();
  await ticketStore.fetchStatistics();
  await ticketStore.fetchTickets({
    page: 1,
    pageSize: 5,
    sortBy: "createdAt",
    sortOrder: "desc",
  });
});

const stats = computed(() => ticketStore.statistics);
const recentTickets = computed(() => ticketStore.tickets.slice(0, 5));
const currentUser = computed(() => userStore.currentUser);

const goToTickets = () => {
  router.push("/tickets");
};

const goToTicket = (id: number) => {
  router.push(`/tickets/${id}`);
};

const goToCreateTicket = () => {
  router.push("/create-ticket");
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
</script>

<template>
  <div class="min-h-screen w-screen bg-black text-white">
    <NavigationGlobal color="black" />

    <!-- Hero Section -->
    <section class="flex w-full items-center justify-center py-20">
      <div class="w-4/5 max-w-6xl md:w-11/12 lg:w-4/5">
        <div class="text-center">
          <h1
            class="mb-6 text-5xl font-extrabold tracking-tight md:text-6xl lg:text-7xl">
            IT <span class="text-k-main">Help Desk</span>
          </h1>
          <p class="mb-8 text-xl text-zinc-400 md:text-2xl">
            Manage and track IT support tickets efficiently
          </p>
          <div class="flex flex-wrap justify-center gap-4">
            <button
              @click="goToCreateTicket"
              class="rounded-lg bg-k-main px-8 py-4 text-lg font-bold uppercase tracking-wide text-white transition duration-300 hover:scale-105 active:translate-y-0.5">
              Create New Ticket
            </button>
            <button
              @click="goToTickets"
              class="rounded-lg border-2 border-k-main px-8 py-4 text-lg font-bold uppercase tracking-wide text-k-main transition duration-300 hover:bg-k-main hover:text-white active:translate-y-0.5">
              View All Tickets
            </button>
          </div>
        </div>
      </div>
    </section>

    <!-- Statistics Section -->
    <section
      v-if="stats"
      class="flex w-full items-center justify-center bg-zinc-900 py-16">
      <div class="w-4/5 max-w-6xl md:w-11/12 lg:w-4/5">
        <h2 class="mb-10 text-3xl font-bold tracking-tight md:text-4xl">
          Dashboard <span class="text-k-main">Statistics</span>
        </h2>

        <!-- Main Stats Grid -->
        <div class="mb-10 grid gap-6 md:grid-cols-2 lg:grid-cols-4">
          <div
            class="rounded-lg border border-zinc-800 bg-black p-6 transition duration-300 hover:border-k-main">
            <h3 class="mb-2 text-sm uppercase tracking-wider text-zinc-500">
              Total Tickets
            </h3>
            <p class="text-4xl font-bold text-white">
              {{ stats.totalTickets }}
            </p>
          </div>
          <div
            class="rounded-lg border border-zinc-800 bg-black p-6 transition duration-300 hover:border-blue-500">
            <h3 class="mb-2 text-sm uppercase tracking-wider text-zinc-500">
              Open Tickets
            </h3>
            <p class="text-4xl font-bold text-blue-500">
              {{ stats.openTickets }}
            </p>
          </div>
          <div
            class="rounded-lg border border-zinc-800 bg-black p-6 transition duration-300 hover:border-yellow-500">
            <h3 class="mb-2 text-sm uppercase tracking-wider text-zinc-500">
              In Progress
            </h3>
            <p class="text-4xl font-bold text-yellow-500">
              {{ stats.inProgressTickets }}
            </p>
          </div>
          <div
            class="rounded-lg border border-zinc-800 bg-black p-6 transition duration-300 hover:border-green-500">
            <h3 class="mb-2 text-sm uppercase tracking-wider text-zinc-500">
              Resolved
            </h3>
            <p class="text-4xl font-bold text-green-500">
              {{ stats.resolvedTickets }}
            </p>
          </div>
        </div>

        <!-- Additional Stats -->
        <div class="grid gap-6 md:grid-cols-3">
          <div
            class="rounded-lg border border-zinc-800 bg-black p-6 transition duration-300 hover:border-red-500">
            <h3 class="mb-2 text-sm uppercase tracking-wider text-zinc-500">
              Critical Tickets
            </h3>
            <p class="text-3xl font-bold text-red-500">
              {{ stats.criticalTickets }}
            </p>
          </div>
          <div
            class="rounded-lg border border-zinc-800 bg-black p-6 transition duration-300 hover:border-orange-500">
            <h3 class="mb-2 text-sm uppercase tracking-wider text-zinc-500">
              Overdue Tickets
            </h3>
            <p class="text-3xl font-bold text-orange-500">
              {{ stats.overdueTickets }}
            </p>
          </div>
          <div
            class="rounded-lg border border-zinc-800 bg-black p-6 transition duration-300 hover:border-k-main">
            <h3 class="mb-2 text-sm uppercase tracking-wider text-zinc-500">
              Avg Resolution Time
            </h3>
            <p class="text-3xl font-bold text-k-main">
              {{ Math.round(stats.averageResolutionTimeHours) }}h
            </p>
          </div>
        </div>
      </div>
    </section>

    <!-- Recent Tickets Section -->
    <section class="flex w-full items-center justify-center py-16">
      <div class="w-4/5 max-w-6xl md:w-11/12 lg:w-4/5">
        <div class="mb-8 flex items-center justify-between">
          <h2 class="text-3xl font-bold tracking-tight md:text-4xl">
            Recent <span class="text-k-main">Tickets</span>
          </h2>
          <button
            @click="goToTickets"
            class="font-semibold uppercase tracking-wide text-k-main transition duration-300 hover:underline">
            View All →
          </button>
        </div>

        <div
          v-if="recentTickets.length === 0"
          class="text-center text-zinc-500">
          <p class="text-lg">No tickets found. Create your first ticket!</p>
        </div>

        <div v-else class="space-y-4">
          <div
            v-for="ticket in recentTickets"
            :key="ticket.id"
            @click="goToTicket(ticket.id)"
            class="cursor-pointer rounded-lg border border-zinc-800 bg-zinc-900 p-6 transition duration-300 hover:border-k-main hover:shadow-lg">
            <div class="mb-4 flex flex-wrap items-start justify-between gap-4">
              <div class="flex-1">
                <h3 class="mb-2 text-xl font-bold text-white">
                  #{{ ticket.id }} - {{ ticket.title }}
                </h3>
                <p class="line-clamp-2 text-zinc-400">
                  {{ ticket.description }}
                </p>
              </div>
              <div class="flex gap-2">
                <StatusBadge type="status" :value="ticket.status" small />
                <StatusBadge type="priority" :value="ticket.priority" small />
              </div>
            </div>
            <div
              class="flex flex-wrap items-center gap-4 text-sm text-zinc-500">
              <span>📁 {{ ticket.category }}</span>
              <span>👤 {{ ticket.createdBy.fullName }}</span>
              <span>🕒 {{ formatDate(ticket.createdAt) }}</span>
              <span>💬 {{ ticket.commentCount }} comments</span>
              <span v-if="ticket.isOverdue" class="font-bold text-red-500"
                >⚠️ OVERDUE</span
              >
            </div>
          </div>
        </div>
      </div>
    </section>

    <!-- Quick Actions -->
    <section class="flex w-full items-center justify-center bg-zinc-900 py-16">
      <div class="w-4/5 max-w-6xl md:w-11/12 lg:w-4/5">
        <h2 class="mb-10 text-3xl font-bold tracking-tight md:text-4xl">
          Quick <span class="text-k-main">Actions</span>
        </h2>
        <div class="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
          <button
            @click="goToCreateTicket"
            class="group rounded-lg border border-zinc-800 bg-black p-8 text-left transition duration-300 hover:border-k-main hover:bg-zinc-900">
            <div
              class="mb-4 flex h-12 w-12 items-center justify-center rounded-full bg-k-main text-2xl">
              ➕
            </div>
            <h3 class="mb-2 text-xl font-bold">Create New Ticket</h3>
            <p class="text-zinc-400">
              Submit a new IT support request or issue
            </p>
          </button>
          <button
            @click="() => router.push('/tickets?status=Open')"
            class="group rounded-lg border border-zinc-800 bg-black p-8 text-left transition duration-300 hover:border-blue-500 hover:bg-zinc-900">
            <div
              class="mb-4 flex h-12 w-12 items-center justify-center rounded-full bg-blue-500 text-2xl">
              📋
            </div>
            <h3 class="mb-2 text-xl font-bold">View Open Tickets</h3>
            <p class="text-zinc-400">See all currently open support tickets</p>
          </button>
          <button
            @click="() => router.push('/statistics')"
            class="group rounded-lg border border-zinc-800 bg-black p-8 text-left transition duration-300 hover:border-green-500 hover:bg-zinc-900">
            <div
              class="mb-4 flex h-12 w-12 items-center justify-center rounded-full bg-green-500 text-2xl">
              📊
            </div>
            <h3 class="mb-2 text-xl font-bold">View Statistics</h3>
            <p class="text-zinc-400">
              Analyze ticket trends and performance metrics
            </p>
          </button>
        </div>
      </div>
    </section>

    <FooterGlobal />
  </div>
</template>
