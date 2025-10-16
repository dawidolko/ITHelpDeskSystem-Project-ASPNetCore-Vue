<script setup lang="ts">
import { onMounted, ref, computed } from "vue";
import { useRouter, useRoute } from "vue-router";
import { useTicketStore } from "../../stores/ticketStore";
import { useUserStore } from "../../stores/userStore";
import { useAuthStore } from "../../stores/authStore";
import NavigationGlobal from "../../components/navigation-global.vue";
import FooterGlobal from "../../components/footer-global.vue";
import StatusBadge from "../../components/StatusBadge.vue";
import {
  TicketStatus,
  TicketPriority,
  TicketCategory,
  type UpdateTicketRequest,
} from "../../types/ticket.types";

const router = useRouter();
const route = useRoute();
const ticketStore = useTicketStore();
const userStore = useUserStore();
const authStore = useAuthStore();

const ticketId = ref(parseInt(route.params.id as string));
const ticket = computed(() => ticketStore.currentTicket);
const loading = computed(() => ticketStore.loading);
const currentUser = computed(() => userStore.currentUser);
const technicians = computed(() => userStore.technicians);

const isEditing = ref(false);
const newComment = ref("");
const isInternalComment = ref(false);
const editForm = ref<UpdateTicketRequest>({});

onMounted(async () => {
  userStore.loadCurrentUser();
  await userStore.fetchTechnicians();
  await ticketStore.fetchTicket(ticketId.value);
});

const formatDate = (date: string): string => {
  return new Date(date).toLocaleDateString("pl-PL", {
    year: "numeric",
    month: "long",
    day: "numeric",
    hour: "2-digit",
    minute: "2-digit",
  });
};

const startEdit = () => {
  if (!ticket.value) return;
  editForm.value = {
    title: ticket.value.title,
    description: ticket.value.description,
    status: ticket.value.status as TicketStatus,
    priority: ticket.value.priority as TicketPriority,
    category: ticket.value.category as TicketCategory,
    assignedToId: ticket.value.assignedTo?.id ?? null,
  };
  isEditing.value = true;
};

const cancelEdit = () => {
  isEditing.value = false;
  editForm.value = {};
};

const saveChanges = async () => {
  if (!ticket.value) return;
  try {
    await ticketStore.updateTicket(ticketId.value, editForm.value);
    isEditing.value = false;
    alert("Ticket updated successfully!");
  } catch (error) {
    alert("Failed to update ticket");
  }
};

const addComment = async () => {
  if (!newComment.value.trim() || !currentUser.value) return;
  try {
    await ticketStore.addComment(ticketId.value, {
      content: newComment.value,
      authorId: currentUser.value.id,
      isInternal: isInternalComment.value,
    });
    newComment.value = "";
    isInternalComment.value = false;
  } catch (error) {
    alert("Failed to add comment");
  }
};

const deleteTicket = async () => {
  if (!confirm("Are you sure you want to delete this ticket?")) return;
  try {
    await ticketStore.deleteTicket(ticketId.value);
    router.push("/tickets");
  } catch (error) {
    alert("Failed to delete ticket");
  }
};

const goBack = () => {
  router.push("/tickets");
};
</script>

<template>
  <div class="min-h-screen w-screen bg-black text-white">
    <NavigationGlobal color="black" />

    <section class="flex w-full items-center justify-center py-16">
      <div class="w-4/5 max-w-5xl md:w-11/12 lg:w-4/5">
        <!-- Back Button -->
        <button
          @click="goBack"
          class="mb-6 flex items-center gap-2 font-semibold uppercase tracking-wide text-zinc-400 transition duration-300 hover:text-k-main">
          ← Back to Tickets
        </button>

        <!-- Loading State -->
        <div v-if="loading && !ticket" class="py-20 text-center">
          <div
            class="inline-block h-12 w-12 animate-spin rounded-full border-4 border-solid border-k-main border-r-transparent"></div>
          <p class="mt-4 text-lg text-zinc-400">Loading ticket...</p>
        </div>

        <!-- Ticket Details -->
        <div v-else-if="ticket" class="space-y-6">
          <!-- Header -->
          <div class="rounded-lg border border-zinc-800 bg-zinc-900 p-8">
            <div class="mb-6 flex flex-wrap items-start justify-between gap-4">
              <div>
                <p class="mb-2 font-mono text-sm text-k-main">
                  TICKET #{{ ticket.id }}
                </p>
                <h1
                  v-if="!isEditing"
                  class="mb-4 text-3xl font-extrabold tracking-tight md:text-4xl">
                  {{ ticket.title }}
                </h1>
                <input
                  v-else
                  v-model="editForm.title"
                  class="mb-4 w-full rounded-lg border border-zinc-700 bg-black px-4 py-2 text-3xl font-extrabold text-white"
                  type="text" />
                <div class="flex flex-wrap gap-3">
                  <StatusBadge
                    v-if="!isEditing"
                    type="status"
                    :value="ticket.status" />
                  <select
                    v-else
                    v-model="editForm.status"
                    class="rounded-lg border border-zinc-700 bg-black px-4 py-2 text-white">
                    <option
                      v-for="status in Object.values(TicketStatus)"
                      :key="status"
                      :value="status">
                      {{ status }}
                    </option>
                  </select>

                  <StatusBadge
                    v-if="!isEditing"
                    type="priority"
                    :value="ticket.priority" />
                  <select
                    v-else
                    v-model="editForm.priority"
                    class="rounded-lg border border-zinc-700 bg-black px-4 py-2 text-white">
                    <option
                      v-for="priority in Object.values(TicketPriority)"
                      :key="priority"
                      :value="priority">
                      {{ priority }}
                    </option>
                  </select>

                  <span
                    v-if="ticket.isOverdue"
                    class="inline-block rounded-full bg-red-600 px-3 py-1 text-sm font-semibold uppercase tracking-wide text-white">
                    ⚠️ OVERDUE
                  </span>
                </div>
              </div>

              <!-- Action Buttons -->
              <div class="flex gap-2">
                <button
                  v-if="!isEditing"
                  @click="startEdit"
                  class="rounded-lg border border-k-main px-6 py-3 font-bold uppercase tracking-wide text-k-main transition duration-300 hover:bg-k-main hover:text-white">
                  Edit
                </button>
                <button
                  v-else
                  @click="saveChanges"
                  class="rounded-lg bg-k-main px-6 py-3 font-bold uppercase tracking-wide text-white transition duration-300 hover:scale-105">
                  Save
                </button>
                <button
                  v-if="isEditing"
                  @click="cancelEdit"
                  class="rounded-lg border border-zinc-700 px-6 py-3 font-bold uppercase tracking-wide text-zinc-400 transition duration-300 hover:border-red-500 hover:text-red-500">
                  Cancel
                </button>
                <button
                  v-if="!isEditing && authStore.isAdmin"
                  @click="deleteTicket"
                  class="rounded-lg border border-red-500 px-6 py-3 font-bold uppercase tracking-wide text-red-500 transition duration-300 hover:bg-red-500 hover:text-white">
                  Delete
                </button>
              </div>
            </div>

            <!-- Metadata Grid -->
            <div
              class="grid gap-6 border-t border-zinc-800 pt-6 md:grid-cols-2">
              <div>
                <h3 class="mb-2 text-sm font-bold uppercase text-zinc-500">
                  Category
                </h3>
                <p v-if="!isEditing" class="text-lg text-white">
                  📁 {{ ticket.category }}
                </p>
                <select
                  v-else
                  v-model="editForm.category"
                  class="w-full rounded-lg border border-zinc-700 bg-black px-4 py-2 text-white">
                  <option
                    v-for="category in Object.values(TicketCategory)"
                    :key="category"
                    :value="category">
                    {{ category }}
                  </option>
                </select>
              </div>

              <div>
                <h3 class="mb-2 text-sm font-bold uppercase text-zinc-500">
                  Created By
                </h3>
                <p class="text-lg text-white">
                  👤 {{ ticket.createdBy.fullName }}
                </p>
                <p class="text-sm text-zinc-400">
                  {{ ticket.createdBy.email }}
                </p>
              </div>

              <div>
                <h3 class="mb-2 text-sm font-bold uppercase text-zinc-500">
                  Assigned To
                </h3>
                <p
                  v-if="!isEditing && ticket.assignedTo"
                  class="text-lg text-white">
                  👨‍💻 {{ ticket.assignedTo.fullName }}
                </p>
                <p
                  v-else-if="!isEditing && !ticket.assignedTo"
                  class="text-lg text-zinc-500">
                  Not assigned
                </p>
                <select
                  v-else
                  v-model="editForm.assignedToId"
                  class="w-full rounded-lg border border-zinc-700 bg-black px-4 py-2 text-white">
                  <option :value="null">Unassigned</option>
                  <option
                    v-for="tech in technicians"
                    :key="tech.id"
                    :value="tech.id">
                    {{ tech.fullName }}
                  </option>
                </select>
              </div>

              <div>
                <h3 class="mb-2 text-sm font-bold uppercase text-zinc-500">
                  Created At
                </h3>
                <p class="text-lg text-white">
                  🕒 {{ formatDate(ticket.createdAt) }}
                </p>
              </div>

              <div>
                <h3 class="mb-2 text-sm font-bold uppercase text-zinc-500">
                  Last Updated
                </h3>
                <p class="text-lg text-white">
                  🔄 {{ formatDate(ticket.updatedAt) }}
                </p>
              </div>

              <div>
                <h3 class="mb-2 text-sm font-bold uppercase text-zinc-500">
                  View Count
                </h3>
                <p class="text-lg text-white">
                  👁️ {{ ticket.viewCount }} views
                </p>
              </div>
            </div>
          </div>

          <!-- Description -->
          <div class="rounded-lg border border-zinc-800 bg-zinc-900 p-8">
            <h2 class="mb-4 text-2xl font-bold">Description</h2>
            <p v-if="!isEditing" class="whitespace-pre-wrap text-zinc-300">
              {{ ticket.description }}
            </p>
            <textarea
              v-else
              v-model="editForm.description"
              rows="6"
              class="w-full rounded-lg border border-zinc-700 bg-black px-4 py-2 text-white"></textarea>
          </div>

          <!-- Resolution Notes -->
          <div
            v-if="ticket.resolutionNotes || isEditing"
            class="rounded-lg border border-green-800 bg-green-950 bg-opacity-20 p-8">
            <h2 class="mb-4 text-2xl font-bold text-green-500">
              Resolution Notes
            </h2>
            <p
              v-if="!isEditing && ticket.resolutionNotes"
              class="whitespace-pre-wrap text-zinc-300">
              {{ ticket.resolutionNotes }}
            </p>
            <textarea
              v-if="isEditing"
              v-model="editForm.resolutionNotes"
              rows="4"
              placeholder="Add resolution notes..."
              class="w-full rounded-lg border border-zinc-700 bg-black px-4 py-2 text-white"></textarea>
          </div>

          <!-- Comments Section -->
          <div class="rounded-lg border border-zinc-800 bg-zinc-900 p-8">
            <h2 class="mb-6 text-2xl font-bold">
              Comments ({{ ticket.comments.length }})
            </h2>

            <!-- Add Comment Form -->
            <div class="mb-8 rounded-lg border border-zinc-700 bg-black p-6">
              <textarea
                v-model="newComment"
                rows="4"
                placeholder="Add a comment..."
                class="w-full rounded-lg border border-zinc-700 bg-zinc-900 px-4 py-3 text-white placeholder-zinc-500 focus:border-k-main focus:outline-none"></textarea>
              <div class="mt-4 flex items-center justify-between">
                <label class="flex items-center gap-2 text-sm text-zinc-400">
                  <input
                    type="checkbox"
                    v-model="isInternalComment"
                    class="h-4 w-4 rounded border-zinc-700 bg-black text-k-main focus:ring-k-main" />
                  Internal comment (visible to technicians only)
                </label>
                <button
                  @click="addComment"
                  :disabled="!newComment.trim()"
                  class="rounded-lg bg-k-main px-6 py-3 font-bold uppercase tracking-wide text-white transition duration-300 hover:scale-105 disabled:cursor-not-allowed disabled:opacity-50">
                  Add Comment
                </button>
              </div>
            </div>

            <!-- Comments List -->
            <div
              v-if="ticket.comments.length === 0"
              class="text-center text-zinc-500">
              <p>No comments yet. Be the first to comment!</p>
            </div>

            <div v-else class="space-y-4">
              <div
                v-for="comment in ticket.comments"
                :key="comment.id"
                :class="[
                  'rounded-lg border p-6',
                  comment.isInternal
                    ? 'border-yellow-800 bg-yellow-950 bg-opacity-20'
                    : 'border-zinc-700 bg-black',
                ]">
                <div class="mb-3 flex items-start justify-between">
                  <div>
                    <p class="font-bold text-white">
                      {{ comment.author.fullName }}
                    </p>
                    <p class="text-sm text-zinc-500">
                      {{ comment.author.role }} •
                      {{ formatDate(comment.createdAt) }}
                    </p>
                  </div>
                  <span
                    v-if="comment.isInternal"
                    class="rounded-full bg-yellow-600 px-3 py-1 text-xs font-semibold uppercase text-white">
                    Internal
                  </span>
                </div>
                <p class="whitespace-pre-wrap text-zinc-300">
                  {{ comment.content }}
                </p>
              </div>
            </div>
          </div>
        </div>

        <!-- Error State -->
        <div v-else class="py-20 text-center">
          <p class="text-xl text-red-500">Ticket not found</p>
          <button
            @click="goBack"
            class="mt-4 font-semibold uppercase tracking-wide text-k-main transition duration-300 hover:underline">
            Back to Tickets
          </button>
        </div>
      </div>
    </section>

    <FooterGlobal />
  </div>
</template>
