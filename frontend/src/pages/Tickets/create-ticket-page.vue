<script setup lang="ts">
import { ref, computed, onMounted } from "vue";
import { useRouter } from "vue-router";
import { useTicketStore } from "../../stores/ticketStore";
import { useUserStore } from "../../stores/userStore";
import NavigationGlobal from "../../components/navigation-global.vue";
import FooterGlobal from "../../components/footer-global.vue";
import {
  TicketPriority,
  TicketCategory,
  type CreateTicketRequest,
} from "../../types/ticket.types";

const router = useRouter();
const ticketStore = useTicketStore();
const userStore = useUserStore();

const currentUser = computed(() => userStore.currentUser);
const loading = computed(() => ticketStore.loading);

const form = ref<CreateTicketRequest>({
  title: "",
  description: "",
  priority: TicketPriority.Medium,
  category: TicketCategory.Other,
  createdById: 0,
});

const errors = ref<Record<string, string>>({});

onMounted(() => {
  userStore.loadCurrentUser();
  if (currentUser.value) {
    form.value.createdById = currentUser.value.id;
  }
});

const validateForm = (): boolean => {
  errors.value = {};

  if (!form.value.title.trim()) {
    errors.value.title = "Title is required";
  } else if (form.value.title.length < 5) {
    errors.value.title = "Title must be at least 5 characters";
  }

  if (!form.value.description.trim()) {
    errors.value.description = "Description is required";
  } else if (form.value.description.length < 20) {
    errors.value.description = "Description must be at least 20 characters";
  }

  if (!form.value.category) {
    errors.value.category = "Category is required";
  }

  if (!form.value.priority) {
    errors.value.priority = "Priority is required";
  }

  return Object.keys(errors.value).length === 0;
};

const submitTicket = async () => {
  if (!validateForm()) {
    return;
  }

  if (!currentUser.value) {
    alert("Please log in to create a ticket");
    return;
  }

  form.value.createdById = currentUser.value.id;

  try {
    const newTicket = await ticketStore.createTicket(form.value);
    alert("Ticket created successfully!");
    router.push(`/tickets/${newTicket.id}`);
  } catch (error) {
    alert("Failed to create ticket. Please try again.");
  }
};

const cancel = () => {
  router.push("/tickets");
};
</script>

<template>
  <div class="min-h-screen w-screen bg-black text-white">
    <NavigationGlobal color="black" />

    <section class="flex w-full items-center justify-center py-16">
      <div class="w-4/5 max-w-4xl md:w-11/12 lg:w-4/5">
        <!-- Header -->
        <div class="mb-8">
          <h1 class="mb-4 text-4xl font-extrabold tracking-tight md:text-5xl">
            Create New <span class="text-k-main">Ticket</span>
          </h1>
          <p class="text-lg text-zinc-400">
            Submit a new IT support request or issue
          </p>
        </div>

        <!-- Form -->
        <form @submit.prevent="submitTicket" class="space-y-6">
          <!-- Title -->
          <div class="rounded-lg border border-zinc-800 bg-zinc-900 p-6">
            <label for="title" class="mb-2 block text-lg font-bold text-white">
              Title <span class="text-red-500">*</span>
            </label>
            <input
              id="title"
              v-model="form.title"
              type="text"
              placeholder="Brief description of the issue"
              :class="[
                'w-full rounded-lg border px-4 py-3 text-white transition duration-300 focus:outline-none',
                errors.title
                  ? 'border-red-500 bg-red-950 bg-opacity-20 focus:border-red-500'
                  : 'border-zinc-700 bg-black focus:border-k-main focus:ring-2 focus:ring-k-main focus:ring-opacity-50',
              ]" />
            <p v-if="errors.title" class="mt-2 text-sm text-red-500">
              {{ errors.title }}
            </p>
            <p class="mt-2 text-sm text-zinc-500">
              Example: "Printer not working in conference room B"
            </p>
          </div>

          <!-- Category and Priority -->
          <div class="grid gap-6 md:grid-cols-2">
            <!-- Category -->
            <div class="rounded-lg border border-zinc-800 bg-zinc-900 p-6">
              <label
                for="category"
                class="mb-2 block text-lg font-bold text-white">
                Category <span class="text-red-500">*</span>
              </label>
              <select
                id="category"
                v-model="form.category"
                :class="[
                  'w-full rounded-lg border px-4 py-3 text-white transition duration-300 focus:outline-none',
                  errors.category
                    ? 'border-red-500 bg-red-950 bg-opacity-20'
                    : 'border-zinc-700 bg-black focus:border-k-main',
                ]">
                <option
                  v-for="category in Object.values(TicketCategory)"
                  :key="category"
                  :value="category">
                  {{ category }}
                </option>
              </select>
              <p v-if="errors.category" class="mt-2 text-sm text-red-500">
                {{ errors.category }}
              </p>
            </div>

            <!-- Priority -->
            <div class="rounded-lg border border-zinc-800 bg-zinc-900 p-6">
              <label
                for="priority"
                class="mb-2 block text-lg font-bold text-white">
                Priority <span class="text-red-500">*</span>
              </label>
              <select
                id="priority"
                v-model="form.priority"
                :class="[
                  'w-full rounded-lg border px-4 py-3 text-white transition duration-300 focus:outline-none',
                  errors.priority
                    ? 'border-red-500 bg-red-950 bg-opacity-20'
                    : 'border-zinc-700 bg-black focus:border-k-main',
                ]">
                <option
                  v-for="priority in Object.values(TicketPriority)"
                  :key="priority"
                  :value="priority">
                  {{ priority }}
                </option>
              </select>
              <p v-if="errors.priority" class="mt-2 text-sm text-red-500">
                {{ errors.priority }}
              </p>
              <div class="mt-3 space-y-1 text-sm text-zinc-500">
                <p>🔴 Critical: 4 hours SLA</p>
                <p>🟠 High: 24 hours SLA</p>
                <p>🟡 Medium: 72 hours SLA</p>
                <p>🟢 Low: 7 days SLA</p>
              </div>
            </div>
          </div>

          <!-- Description -->
          <div class="rounded-lg border border-zinc-800 bg-zinc-900 p-6">
            <label
              for="description"
              class="mb-2 block text-lg font-bold text-white">
              Description <span class="text-red-500">*</span>
            </label>
            <textarea
              id="description"
              v-model="form.description"
              rows="8"
              placeholder="Detailed description of the issue. Include steps to reproduce, error messages, and any other relevant information..."
              :class="[
                'w-full rounded-lg border px-4 py-3 text-white transition duration-300 focus:outline-none',
                errors.description
                  ? 'border-red-500 bg-red-950 bg-opacity-20 focus:border-red-500'
                  : 'border-zinc-700 bg-black focus:border-k-main focus:ring-2 focus:ring-k-main focus:ring-opacity-50',
              ]"></textarea>
            <p v-if="errors.description" class="mt-2 text-sm text-red-500">
              {{ errors.description }}
            </p>
            <p class="mt-2 text-sm text-zinc-500">
              Minimum 20 characters. Be as detailed as possible to help us
              resolve your issue quickly.
            </p>
          </div>

          <!-- Guidelines -->
          <div
            class="rounded-lg border border-blue-800 bg-blue-950 bg-opacity-20 p-6">
            <h3 class="mb-3 text-lg font-bold text-blue-400">
              📝 Ticket Guidelines
            </h3>
            <ul class="space-y-2 text-sm text-zinc-300">
              <li>✅ Provide a clear and concise title</li>
              <li>✅ Include detailed steps to reproduce the issue</li>
              <li>✅ Mention any error messages you received</li>
              <li>✅ Specify when the issue started occurring</li>
              <li>✅ Select the appropriate category and priority</li>
              <li>
                ✅ Include screenshots if applicable (add link in description)
              </li>
            </ul>
          </div>

          <!-- Actions -->
          <div class="flex flex-wrap gap-4">
            <button
              type="submit"
              :disabled="loading"
              class="flex-1 rounded-lg bg-k-main px-8 py-4 text-lg font-bold uppercase tracking-wide text-white transition duration-300 hover:scale-105 active:translate-y-0.5 disabled:cursor-not-allowed disabled:opacity-50 md:flex-none">
              {{ loading ? "Creating..." : "Create Ticket" }}
            </button>
            <button
              type="button"
              @click="cancel"
              :disabled="loading"
              class="flex-1 rounded-lg border-2 border-zinc-700 px-8 py-4 text-lg font-bold uppercase tracking-wide text-zinc-400 transition duration-300 hover:border-red-500 hover:text-red-500 active:translate-y-0.5 disabled:cursor-not-allowed disabled:opacity-50 md:flex-none">
              Cancel
            </button>
          </div>
        </form>
      </div>
    </section>

    <FooterGlobal />
  </div>
</template>
