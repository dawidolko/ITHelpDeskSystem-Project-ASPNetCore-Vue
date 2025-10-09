<script setup lang="ts">
import { onMounted, computed } from "vue";
import { useTicketStore } from "../../stores/ticketStore";
import { useUserStore } from "../../stores/userStore";
import NavigationGlobal from "../../components/navigation-global.vue";
import FooterGlobal from "../../components/footer-global.vue";

const ticketStore = useTicketStore();
const userStore = useUserStore();

const stats = computed(() => ticketStore.statistics);
const loading = computed(() => ticketStore.loading);

onMounted(async () => {
  userStore.loadCurrentUser();
  await ticketStore.fetchStatistics();
});

const getPriorityColor = (priority: string): string => {
  switch (priority) {
    case "Critical":
      return "bg-red-600";
    case "High":
      return "bg-orange-500";
    case "Medium":
      return "bg-yellow-500";
    case "Low":
      return "bg-green-500";
    default:
      return "bg-gray-500";
  }
};

const getCategoryIcon = (category: string): string => {
  switch (category) {
    case "Hardware":
      return "💻";
    case "Software":
      return "💿";
    case "Network":
      return "🌐";
    case "Account":
      return "👤";
    case "Email":
      return "📧";
    case "Printer":
      return "🖨️";
    default:
      return "📁";
  }
};
</script>

<template>
  <div class="min-h-screen w-screen bg-black text-white">
    <NavigationGlobal color="black" />

    <section class="flex w-full items-center justify-center py-16">
      <div class="w-4/5 max-w-6xl md:w-11/12 lg:w-4/5">
        <!-- Header -->
        <div class="mb-12 text-center">
          <h1 class="mb-4 text-5xl font-extrabold tracking-tight md:text-6xl">
            Ticket <span class="text-k-main">Statistics</span>
          </h1>
          <p class="text-xl text-zinc-400">
            Comprehensive analytics and metrics for IT support tickets
          </p>
        </div>

        <!-- Loading State -->
        <div v-if="loading" class="py-20 text-center">
          <div
            class="inline-block h-12 w-12 animate-spin rounded-full border-4 border-solid border-k-main border-r-transparent"></div>
          <p class="mt-4 text-lg text-zinc-400">Loading statistics...</p>
        </div>

        <!-- Statistics Content -->
        <div v-else-if="stats" class="space-y-12">
          <!-- Overview Cards -->
          <div>
            <h2 class="mb-6 text-3xl font-bold">
              Overview <span class="text-k-main">Metrics</span>
            </h2>
            <div class="grid gap-6 md:grid-cols-2 lg:grid-cols-4">
              <div
                class="group rounded-lg border border-zinc-800 bg-zinc-900 p-6 transition duration-300 hover:border-k-main hover:shadow-lg">
                <div class="mb-2 text-4xl">📊</div>
                <h3
                  class="mb-2 text-sm font-bold uppercase tracking-wider text-zinc-500">
                  Total Tickets
                </h3>
                <p class="text-4xl font-extrabold text-white">
                  {{ stats.totalTickets }}
                </p>
              </div>

              <div
                class="group rounded-lg border border-zinc-800 bg-zinc-900 p-6 transition duration-300 hover:border-blue-500 hover:shadow-lg">
                <div class="mb-2 text-4xl">📋</div>
                <h3
                  class="mb-2 text-sm font-bold uppercase tracking-wider text-zinc-500">
                  Open Tickets
                </h3>
                <p class="text-4xl font-extrabold text-blue-500">
                  {{ stats.openTickets }}
                </p>
              </div>

              <div
                class="group rounded-lg border border-zinc-800 bg-zinc-900 p-6 transition duration-300 hover:border-yellow-500 hover:shadow-lg">
                <div class="mb-2 text-4xl">⚙️</div>
                <h3
                  class="mb-2 text-sm font-bold uppercase tracking-wider text-zinc-500">
                  In Progress
                </h3>
                <p class="text-4xl font-extrabold text-yellow-500">
                  {{ stats.inProgressTickets }}
                </p>
              </div>

              <div
                class="group rounded-lg border border-zinc-800 bg-zinc-900 p-6 transition duration-300 hover:border-green-500 hover:shadow-lg">
                <div class="mb-2 text-4xl">✅</div>
                <h3
                  class="mb-2 text-sm font-bold uppercase tracking-wider text-zinc-500">
                  Resolved
                </h3>
                <p class="text-4xl font-extrabold text-green-500">
                  {{ stats.resolvedTickets }}
                </p>
              </div>
            </div>
          </div>

          <!-- Critical Metrics -->
          <div>
            <h2 class="mb-6 text-3xl font-bold">
              Critical <span class="text-k-main">Indicators</span>
            </h2>
            <div class="grid gap-6 md:grid-cols-3">
              <div
                class="rounded-lg border border-red-800 bg-red-950 bg-opacity-20 p-8">
                <div class="mb-4 text-5xl">🚨</div>
                <h3
                  class="mb-2 text-sm font-bold uppercase tracking-wider text-red-400">
                  Critical Priority
                </h3>
                <p class="text-5xl font-extrabold text-red-500">
                  {{ stats.criticalTickets }}
                </p>
                <p class="mt-2 text-sm text-zinc-400">
                  Requires immediate attention
                </p>
              </div>

              <div
                class="rounded-lg border border-orange-800 bg-orange-950 bg-opacity-20 p-8">
                <div class="mb-4 text-5xl">⏰</div>
                <h3
                  class="mb-2 text-sm font-bold uppercase tracking-wider text-orange-400">
                  Overdue Tickets
                </h3>
                <p class="text-5xl font-extrabold text-orange-500">
                  {{ stats.overdueTickets }}
                </p>
                <p class="mt-2 text-sm text-zinc-400">Past SLA deadline</p>
              </div>

              <div class="rounded-lg border border-k-main bg-zinc-900 p-8">
                <div class="mb-4 text-5xl">⚡</div>
                <h3
                  class="mb-2 text-sm font-bold uppercase tracking-wider text-k-main">
                  Avg Resolution Time
                </h3>
                <p class="text-5xl font-extrabold text-k-main">
                  {{ Math.round(stats.averageResolutionTimeHours) }}
                  <span class="text-2xl">h</span>
                </p>
                <p class="mt-2 text-sm text-zinc-400">
                  Average time to resolve
                </p>
              </div>
            </div>
          </div>

          <!-- By Priority -->
          <div>
            <h2 class="mb-6 text-3xl font-bold">
              By <span class="text-k-main">Priority</span>
            </h2>
            <div class="rounded-lg border border-zinc-800 bg-zinc-900 p-8">
              <div class="space-y-6">
                <div
                  v-for="item in stats.byPriority"
                  :key="item.priority"
                  class="group">
                  <div class="mb-2 flex items-center justify-between">
                    <span class="text-lg font-bold text-white">{{
                      item.priority
                    }}</span>
                    <span class="text-2xl font-extrabold text-k-main">{{
                      item.count
                    }}</span>
                  </div>
                  <div class="h-4 overflow-hidden rounded-full bg-zinc-800">
                    <div
                      :class="[
                        getPriorityColor(item.priority),
                        'h-full transition-all duration-500',
                      ]"
                      :style="{
                        width: `${(item.count / stats.totalTickets) * 100}%`,
                      }"></div>
                  </div>
                  <p class="mt-1 text-sm text-zinc-500">
                    {{ ((item.count / stats.totalTickets) * 100).toFixed(1) }}%
                    of total tickets
                  </p>
                </div>
              </div>
            </div>
          </div>

          <!-- By Category -->
          <div>
            <h2 class="mb-6 text-3xl font-bold">
              By <span class="text-k-main">Category</span>
            </h2>
            <div class="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
              <div
                v-for="item in stats.byCategory"
                :key="item.category"
                class="group rounded-lg border border-zinc-800 bg-zinc-900 p-6 transition duration-300 hover:border-k-main">
                <div class="mb-4 text-5xl">
                  {{ getCategoryIcon(item.category) }}
                </div>
                <h3 class="mb-2 text-xl font-bold text-white">
                  {{ item.category }}
                </h3>
                <p class="text-4xl font-extrabold text-k-main">
                  {{ item.count }}
                </p>
                <p class="mt-2 text-sm text-zinc-400">
                  {{ ((item.count / stats.totalTickets) * 100).toFixed(1) }}% of
                  total
                </p>
                <div class="mt-4 h-2 overflow-hidden rounded-full bg-zinc-800">
                  <div
                    class="h-full bg-k-main transition-all duration-500"
                    :style="{
                      width: `${(item.count / stats.totalTickets) * 100}%`,
                    }"></div>
                </div>
              </div>
            </div>
          </div>

          <!-- Status Breakdown -->
          <div>
            <h2 class="mb-6 text-3xl font-bold">
              Status <span class="text-k-main">Breakdown</span>
            </h2>
            <div class="grid gap-6 md:grid-cols-2">
              <div class="rounded-lg border border-zinc-800 bg-zinc-900 p-8">
                <h3 class="mb-6 text-xl font-bold">Active Tickets</h3>
                <div class="space-y-4">
                  <div class="flex items-center justify-between">
                    <span class="text-zinc-400">Open</span>
                    <span class="text-2xl font-bold text-blue-500">{{
                      stats.openTickets
                    }}</span>
                  </div>
                  <div class="flex items-center justify-between">
                    <span class="text-zinc-400">In Progress</span>
                    <span class="text-2xl font-bold text-yellow-500">{{
                      stats.inProgressTickets
                    }}</span>
                  </div>
                </div>
              </div>

              <div class="rounded-lg border border-zinc-800 bg-zinc-900 p-8">
                <h3 class="mb-6 text-xl font-bold">Closed Tickets</h3>
                <div class="space-y-4">
                  <div class="flex items-center justify-between">
                    <span class="text-zinc-400">Resolved</span>
                    <span class="text-2xl font-bold text-green-500">{{
                      stats.resolvedTickets
                    }}</span>
                  </div>
                  <div class="flex items-center justify-between">
                    <span class="text-zinc-400">Closed</span>
                    <span class="text-2xl font-bold text-gray-500">{{
                      stats.closedTickets
                    }}</span>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- Performance Summary -->
          <div class="rounded-lg border border-k-main bg-zinc-900 p-8">
            <h2 class="mb-6 text-3xl font-bold text-k-main">
              📈 Performance Summary
            </h2>
            <div class="grid gap-6 md:grid-cols-3">
              <div>
                <p class="mb-2 text-sm text-zinc-500">Resolution Rate</p>
                <p class="text-3xl font-bold text-white">
                  {{
                    stats.totalTickets > 0
                      ? (
                          (stats.resolvedTickets / stats.totalTickets) *
                          100
                        ).toFixed(1)
                      : 0
                  }}%
                </p>
              </div>
              <div>
                <p class="mb-2 text-sm text-zinc-500">Active Load</p>
                <p class="text-3xl font-bold text-white">
                  {{ stats.openTickets + stats.inProgressTickets }}
                </p>
              </div>
              <div>
                <p class="mb-2 text-sm text-zinc-500">Efficiency Score</p>
                <p class="text-3xl font-bold text-white">
                  {{
                    stats.averageResolutionTimeHours < 48
                      ? "Excellent"
                      : stats.averageResolutionTimeHours < 96
                      ? "Good"
                      : "Needs Improvement"
                  }}
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>

    <FooterGlobal />
  </div>
</template>
