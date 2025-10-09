<script setup lang="ts">
import CategoryBox from "./category-box.vue";

import pcImage from "/products/categories/category-pc.webp";
import laptopImage from "/products/categories/category-laptop.webp";
import monitorsImage from "/products/categories/category-monitors.webp";
import peripheralsImage from "/products/categories/category-peripherals.webp";
import audioImage from "/products/categories/category-audio.webp";
import workspaceImage from "/products/categories/category-workspace.webp";

const categories = [
  {
    src: pcImage,
    category: "pc",
    imgClass: [""],
  },
  {
    src: laptopImage,
    category: "laptop",
    imgClass: ["scale-90"],
  },
  {
    src: monitorsImage,
    category: "monitors",
    imgClass: ["-mb-6", "md:-mb-5", "md:scale-110"],
  },
  {
    src: peripheralsImage,
    category: "peripherals",
    imgClass: [""],
  },
  {
    src: audioImage,
    category: "audio",
    imgClass: ["scale-95"],
  },
  {
    src: workspaceImage,
    category: "workspace",
    imgClass: [""],
  },
];
</script>

<template>
  <div class="mt-16 flex h-full w-full flex-col items-center bg-white lg:mt-32">
    <div
      class="grid w-4/5 grid-cols-2 items-center gap-4 md:h-[17rem] md:w-11/12 md:max-w-6xl md:grid-cols-3 md:gap-6 lg:w-4/5 lg:grid-cols-6">
      <CategoryBox
        v-for="(item, index) in categories"
        :key="index"
        :src="item.src"
        :category="item.category"
        :imgClass="item.imgClass" />
    </div>
  </div>
</template>
