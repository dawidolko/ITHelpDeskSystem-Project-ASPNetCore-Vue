<script lang="ts" setup>
import ButtonSolid from "../../../components/Buttons/button-solid.vue";
import setupMainImage from "/products/workspace/station2.webp";
</script>

<template>
  <section
    class="flex w-full flex-col items-center overflow-hidden rounded-b-md bg-k-black">
    <div
      class="relative mt-20 flex max-w-6xl flex-col text-center transition-transform duration-200 sm:w-4/5 md:w-11/12 lg:w-4/5 xl:grid xl:grid-cols-2 xl:gap-10 xl:text-start">
      <div
        class="relative z-10 flex flex-col items-center justify-center pb-10 xl:items-start xl:pb-0">
        <p
          class="md:text-md text-sm font-light uppercase tracking-broad text-white opacity-80">
          new setup
        </p>
        <h1
          class="relative mt-4 text-5xl font-semibold uppercase text-white md:text-6xl lg:text-7xl">
          Ultimate <br />
          Gaming <br />
          Setup
        </h1>
        <p class="mb-10 mt-5 max-w-3xl text-white opacity-90 md:text-lg">
          Experience the perfect combination of performance and style with our
          premium gaming equipment and professional workspace solutions.
        </p>
        <ButtonSolid
          to="/components"
          content="explore setup"
          color="light"
          add="font-bold mb-10" />
      </div>

      <div class="relative w-full pb-10 xl:pb-0">
        <div class="mx-auto overflow-hidden rounded-lg">
          <img
            :src="setupMainImage"
            alt="Ultimate Gaming Setup - Professional workstation with multiple monitors, RGB lighting and premium peripherals"
            class="w-full h-auto object-cover grayscale hover:grayscale-0 transition-all duration-500 transform hover:scale-105 shadow-2xl"
            loading="lazy" />
        </div>

        <div
          class="absolute inset-0 bg-gradient-to-t from-k-black via-transparent to-transparent opacity-40 pointer-events-none rounded-lg"></div>
      </div>
    </div>
  </section>
</template>
