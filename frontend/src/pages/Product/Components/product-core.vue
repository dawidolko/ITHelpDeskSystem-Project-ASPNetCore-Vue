<script setup lang="ts">
import ButtonSolid from "../../../components/Buttons/button-solid.vue";
import ButtonGoBack from "../../../components/Buttons/button-go-back.vue";
import { Component } from "../../../data/component-types";

const props = defineProps<{
  item: Component;
}>();
</script>

<template>
  <ButtonGoBack />
  <section
    class="flex w-4/5 max-w-6xl flex-col lg:grid lg:grid-cols-2 lg:grid-rows-1">
    <div
      class="flex flex-col items-center pb-6 lg:col-span-1 lg:col-start-2 lg:row-span-full lg:ml-24 lg:items-start lg:justify-center">
      <p
        v-if="props.item.type === 'main'"
        class="lg:text-md text-sm uppercase tracking-broad text-black">
        main component
      </p>
      <h1
        class="mt-4 text-center text-4xl font-semibold uppercase text-black lg:text-start lg:text-6xl">
        {{ props.item.name }}
      </h1>
      <h2
        class="mt-2 text-center text-2xl font-medium text-black opacity-80 lg:text-start lg:text-3xl">
        {{ props.item.model }}
      </h2>
      <p class="mt-6 text-center text-black opacity-60 lg:pr-20 lg:text-start">
        {{ props.item.description }}
      </p>

      <div class="mt-6 mb-6 w-full lg:pr-20">
        <h3 class="mb-3 text-lg font-semibold text-black">Specifications:</h3>
        <ul class="space-y-2 text-sm text-black opacity-70">
          <li
            v-for="(spec, index) in props.item.specs.slice(0, 5)"
            :key="index">
            • {{ spec }}
          </li>
        </ul>
      </div>

      <ButtonSolid
        :to="`/${props.item.category}/${props.item.id}`"
        color="light"
        add="font-bold"
        content="see details" />
    </div>
    <div
      class="order-first overflow-hidden rounded pb-10 lg:order-none lg:col-span-1 lg:col-start-1 lg:row-span-full lg:pb-0">
      <img
        loading="lazy"
        class="relative aspect-square object-cover transition duration-300 hover:scale-105"
        :src="props.item.image"
        :alt="props.item.name" />
    </div>
  </section>
</template>
