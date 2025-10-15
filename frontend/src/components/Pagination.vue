<script setup lang="ts">
import { computed } from "vue";

interface Props {
  currentPage: number;
  totalPages: number;
  hasNext: boolean;
  hasPrevious: boolean;
}

interface Emits {
  (e: "page-changed", page: number): void;
}

const props = defineProps<Props>();
const emit = defineEmits<Emits>();

const goToPage = (page: number) => {
  if (page >= 1 && page <= props.totalPages) {
    emit("page-changed", page);
  }
};

const pageNumbers = computed(() => {
  const pages: number[] = [];
  const maxVisible = 7;
  const halfVisible = Math.floor(maxVisible / 2);

  let start = Math.max(1, props.currentPage - halfVisible);
  let end = Math.min(props.totalPages, props.currentPage + halfVisible);

  if (props.currentPage <= halfVisible) {
    end = Math.min(maxVisible, props.totalPages);
  } else if (props.currentPage >= props.totalPages - halfVisible) {
    start = Math.max(1, props.totalPages - maxVisible + 1);
  }

  for (let i = start; i <= end; i++) {
    pages.push(i);
  }

  return pages;
});
</script>

<template>
  <div
    v-if="totalPages > 1"
    class="flex items-center justify-center gap-2 py-8">
    <!-- Previous Button -->
    <button
      @click="goToPage(currentPage - 1)"
      :disabled="!hasPrevious"
      :class="[
        'rounded-lg border px-4 py-2 font-semibold uppercase tracking-wide transition duration-300',
        hasPrevious
          ? 'border-zinc-700 text-white hover:border-k-main hover:text-k-main active:translate-y-0.5'
          : 'cursor-not-allowed border-zinc-800 text-zinc-600',
      ]">
      Previous
    </button>

    <!-- Page Numbers -->
    <div class="flex gap-2">
      <button
        v-if="pageNumbers[0] > 1"
        @click="goToPage(1)"
        class="rounded-lg border border-zinc-700 px-4 py-2 font-semibold text-white transition duration-300 hover:border-k-main hover:text-k-main active:translate-y-0.5">
        1
      </button>
      <span
        v-if="pageNumbers[0] > 2"
        class="flex items-center px-2 text-zinc-500"
        >...</span
      >

      <button
        v-for="page in pageNumbers"
        :key="page"
        @click="goToPage(page)"
        :class="[
          'rounded-lg border px-4 py-2 font-semibold transition duration-300',
          page === currentPage
            ? 'border-k-main bg-k-main text-white'
            : 'border-zinc-700 text-white hover:border-k-main hover:text-k-main active:translate-y-0.5',
        ]">
        {{ page }}
      </button>

      <span
        v-if="pageNumbers[pageNumbers.length - 1] < totalPages - 1"
        class="flex items-center px-2 text-zinc-500"
        >...</span
      >
      <button
        v-if="pageNumbers[pageNumbers.length - 1] < totalPages"
        @click="goToPage(totalPages)"
        class="rounded-lg border border-zinc-700 px-4 py-2 font-semibold text-white transition duration-300 hover:border-k-main hover:text-k-main active:translate-y-0.5">
        {{ totalPages }}
      </button>
    </div>

    <!-- Next Button -->
    <button
      @click="goToPage(currentPage + 1)"
      :disabled="!hasNext"
      :class="[
        'rounded-lg border px-4 py-2 font-semibold uppercase tracking-wide transition duration-300',
        hasNext
          ? 'border-zinc-700 text-white hover:border-k-main hover:text-k-main active:translate-y-0.5'
          : 'cursor-not-allowed border-zinc-800 text-zinc-600',
      ]">
      Next
    </button>
  </div>
</template>
