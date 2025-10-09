<script setup lang="ts">
import { computed, onMounted, ref } from "vue";
import { Component } from "../../../data/component-types";
import ButtonSolid from "../../../components/Buttons/button-solid.vue";

const props = defineProps<{
  category: string;
  item: Component;
}>();

const flip = computed(() => {
  return props.item.id % 2 === 0 ? false : true;
});

let show = ref(false);

onMounted(() => {
  show.value = true;
});
</script>

<template>
  <div
    class="mt-16 flex w-4/5 max-w-6xl flex-col items-center gap-10 md:mt-32 md:grid md:w-11/12 md:grid-cols-2 md:grid-rows-1 md:gap-10 lg:w-4/5 lg:gap-20">
    <div
      class="flex flex-col items-center md:col-span-1 md:row-start-1 md:block lg:pb-6"
      :class="flip === true ? 'md:col-start-1' : 'md:col-start-2'">
      <p
        v-if="props.item.type === 'main'"
        class="md:text-md text-sm font-light uppercase tracking-broad text-black">
        main component
      </p>
      <h1
        class="mt-4 text-center text-4xl font-semibold uppercase text-black md:text-start md:text-5xl lg:text-6xl">
        {{ props.item.name }}
      </h1>
      <h2
        class="mt-2 text-center text-2xl font-medium text-black opacity-80 md:text-start md:text-3xl">
        {{ props.item.model }}
      </h2>
      <p
        class="mb-6 mt-4 text-center text-black opacity-60 md:pr-20 md:text-start">
        {{ props.item.description }}
      </p>

      <div class="mb-6 w-full md:pr-20">
        <h3 class="mb-3 text-lg font-semibold text-black">
          Key specifications:
        </h3>
        <ul class="space-y-1 text-sm text-black opacity-70">
          <li
            v-for="(spec, index) in props.item.specs.slice(0, 4)"
            :key="index">
            • {{ spec }}
          </li>
          <li
            v-if="props.item.specs.length > 4"
            class="text-k-main font-medium">
            + {{ props.item.specs.length - 4 }} more...
          </li>
        </ul>
      </div>

      <div
        class="flex flex-row items-center justify-center gap-4 md:justify-normal">
        <ButtonSolid
          :to="`/${props.category}/${props.item.id}`"
          color="light"
          add="font-bold"
          content="See details" />
      </div>
    </div>

    <div
      class="order-first col-span-1 cursor-pointer overflow-hidden rounded active:translate-y-1 md:-order-none"
      :class="flip === true ? 'col-start-2' : 'col-start-1'">
      <img
        loading="lazy"
        class="relative aspect-square w-full object-cover transition duration-300 grayscale hover:grayscale-0 hover:scale-105"
        :src="props.item.image"
        :alt="props.item.name" />
    </div>
  </div>
</template>
