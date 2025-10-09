<script setup lang="ts">
import { Component } from "../data/component-types.ts";
import { getRandomComponents } from "../data/battlestation-utils.ts";
import { onBeforeMount } from "vue";
import ButtonSolid from "../components/Buttons/button-solid.vue";

const props = defineProps<{
  productCategory: string;
  productId: number;
}>();

let items: Component[] = [];

onBeforeMount(() => {
  items = getRandomComponents(props.productCategory, props.productId);
});
</script>

<template>
  <section class="mt-20 flex w-4/5 max-w-6xl flex-col items-center lg:mt-32">
    <h2 class="mb-16 font-Manrope text-3xl font-bold uppercase text-black">
      You may also be interested in
    </h2>
    <div
      class="flex flex-col items-center gap-12 lg:grid lg:grid-cols-3 lg:grid-rows-1 lg:gap-6">
      <div
        class="flex flex-col items-center justify-between gap-8 lg:gap-10"
        v-for="(item, index) in items"
        :key="index">
        <router-link
          :to="`/${item.category}/${item.id}`"
          class="overflow-hidden rounded">
          <img
            class="object-center filter grayscale transition duration-300 hover:grayscale-0 hover:scale-110"
            :src="item.image"
            alt=""
            loading="lazy" />
        </router-link>
        <h3 class="text-center font-Manrope text-2xl font-semibold text-black">
          {{ item.name }} <br class="hidden lg:inline" />
          <span class="capitalize"> {{ item.model }}</span>
        </h3>
        <ButtonSolid
          :to="`/${item.category}/${item.id}`"
          color="light"
          content="see component"
          size="small" />
      </div>
    </div>
  </section>
</template>
