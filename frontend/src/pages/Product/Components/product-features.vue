<script setup lang="ts">
interface Contents {
  count: number;
  content: string;
}

const props = defineProps<{
  features: string;
  inthebox: Contents[];
}>();
</script>

<template>
  <section
    class="mt-16 flex w-4/5 max-w-6xl flex-col lg:mt-32 lg:grid lg:grid-cols-2 lg:grid-rows-1">
    <div class="flex h-full w-full flex-col">
      <h2 class="mb-8 text-2xl font-bold tracking-wide text-black">FUNKCJE</h2>
      <p class="tracking-wide text-black opacity-60">
        {{ props.features }}
      </p>
    </div>
    <div class="mt-16 flex h-full w-full flex-col lg:ml-24 lg:mt-0">
      <h2 class="mb-8 text-2xl font-bold tracking-wide text-black">
        W ZESTAWIE
      </h2>
      <div
        class="flex flex-row items-baseline gap-4"
        v-for="(item, index) of props.inthebox"
        :key="index">
        <p class="font-bold tracking-wide text-k-main">{{ item.count }}x</p>
        <p class="tracking-wide text-black opacity-60">
          {{ item.content }}
        </p>
      </div>
    </div>
  </section>
</template>
