<script setup lang="ts">
import { ref } from "vue";

interface Props {
  modelValue: string;
  placeholder?: string;
  debounce?: number;
}

interface Emits {
  (e: "update:modelValue", value: string): void;
  (e: "search", value: string): void;
}

const props = withDefaults(defineProps<Props>(), {
  placeholder: "Search...",
  debounce: 300,
});

const emit = defineEmits<Emits>();

const localValue = ref(props.modelValue);
let debounceTimeout: ReturnType<typeof setTimeout>;

const handleInput = (event: Event) => {
  const target = event.target as HTMLInputElement;
  localValue.value = target.value;
  emit("update:modelValue", target.value);

  // Debounce search
  clearTimeout(debounceTimeout);
  debounceTimeout = setTimeout(() => {
    emit("search", target.value);
  }, props.debounce);
};

const clearSearch = () => {
  localValue.value = "";
  emit("update:modelValue", "");
  emit("search", "");
};
</script>

<template>
  <div class="relative w-full">
    <input
      type="text"
      :value="localValue"
      @input="handleInput"
      :placeholder="placeholder"
      class="w-full rounded-lg border border-zinc-700 bg-black px-4 py-3 pl-10 text-white placeholder-zinc-500 transition duration-300 focus:border-k-main focus:outline-none focus:ring-2 focus:ring-k-main focus:ring-opacity-50" />
    <svg
      xmlns="http://www.w3.org/2000/svg"
      fill="none"
      viewBox="0 0 24 24"
      stroke-width="2"
      stroke="currentColor"
      class="absolute left-3 top-1/2 h-5 w-5 -translate-y-1/2 text-zinc-500">
      <path
        stroke-linecap="round"
        stroke-linejoin="round"
        d="m21 21-5.197-5.197m0 0A7.5 7.5 0 1 0 5.196 5.196a7.5 7.5 0 0 0 10.607 10.607Z" />
    </svg>
    <button
      v-if="localValue"
      @click="clearSearch"
      class="absolute right-3 top-1/2 -translate-y-1/2 text-zinc-500 transition duration-300 hover:text-white">
      <svg
        xmlns="http://www.w3.org/2000/svg"
        fill="none"
        viewBox="0 0 24 24"
        stroke-width="2"
        stroke="currentColor"
        class="h-5 w-5">
        <path
          stroke-linecap="round"
          stroke-linejoin="round"
          d="M6 18 18 6M6 6l12 12" />
      </svg>
    </button>
  </div>
</template>
