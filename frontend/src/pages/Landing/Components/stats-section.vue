<script setup lang="ts">
import {
  getTotalComponents,
  getAllCategories,
} from "../../../data/battlestation-utils";

const totalComponents = getTotalComponents();
const totalCategories = getAllCategories().length;
</script>

<template>
  <section
    class="mt-20 flex h-full flex-col items-center justify-center md:mt-32 lg:mt-40">
    <div
      class="flex h-full w-4/5 max-w-6xl flex-col items-center gap-10 md:h-[20rem] md:w-11/12 md:flex-row md:items-center md:justify-between md:gap-32 lg:h-[20rem] lg:w-4/5">
      <div
        class="flex h-full flex-col items-center justify-center text-center md:w-1/2 md:items-start md:text-start">
        <h3 class="text-4xl font-semibold uppercase text-k-black md:text-5xl">
          My <br class="hidden xl:inline" />
          <span class="text-k-main">Battlestation</span> in numbers
        </h3>
        <p class="mt-10 tracking-wide text-black opacity-70 md:mt-6">
          Discover my work and gaming station. From a powerful gaming PC with
          RTX 3060Ti, through MacBook Pro M3, to a professional audio setup with
          the Novox NC1 microphone.
          <br />
          <br />
          Each component has been carefully selected for maximum efficiency in
          work and play.
        </p>
      </div>

      <div class="grid grid-cols-2 gap-8 md:gap-12">
        <div class="flex flex-col items-center text-center">
          <div class="text-5xl font-bold text-k-main md:text-6xl">
            {{ totalComponents }}
          </div>
          <div
            class="mt-2 text-lg font-semibold text-k-black opacity-70 uppercase tracking-wide">
            Components
          </div>
        </div>

        <div class="flex flex-col items-center text-center">
          <div class="text-5xl font-bold text-k-main md:text-6xl">
            {{ totalCategories }}
          </div>
          <div
            class="mt-2 text-lg font-semibold text-k-black opacity-70 uppercase tracking-wide">
            Categories
          </div>
        </div>

        <div class="flex flex-col items-center text-center">
          <div class="text-5xl font-bold text-k-main md:text-6xl">3</div>
          <div
            class="mt-2 text-lg font-semibold text-k-black opacity-70 uppercase tracking-wide">
            Monitors
          </div>
        </div>

        <div class="flex flex-col items-center text-center">
          <div class="text-5xl font-bold text-k-main md:text-6xl">64GB</div>
          <div
            class="mt-2 text-lg font-semibold text-k-black opacity-70 uppercase tracking-wide">
            RAM
          </div>
        </div>
      </div>
    </div>
  </section>
</template>
