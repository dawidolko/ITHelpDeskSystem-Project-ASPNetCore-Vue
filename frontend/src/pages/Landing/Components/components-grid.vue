<script setup lang="ts">
import ButtonEmpty from "../../../components/Buttons/button-empty.vue";
import ButtonSolid from "../../../components/Buttons/button-solid.vue";
import pcImage from "/products/workspace/station1.webp";
import monitorsImage from "/products/monitors/screen1.webp";
import peripheralsImage from "/products/peripherals/keyboard1.jpg";
</script>

<template>
  <section
    class="mt-20 flex h-full w-full flex-col items-center md:-mb-10 md:h-[70rem]">
    <div
      class="md:grid-rows-7 flex h-full w-4/5 max-w-6xl flex-col items-center gap-4 md:grid md:w-11/12 md:grid-cols-6 md:gap-8 lg:w-4/5">
      <div
        id="pcBox"
        class="flex flex-col items-center overflow-hidden rounded-md bg-k-main text-black md:col-span-full md:row-span-3 md:h-[31rem]">
        <div
          class="group order-1 flex h-full w-full flex-col items-center justify-center md:order-none md:grid md:grid-cols-7">
          <router-link
            to="/pc"
            href=""
            class="relative flex h-full w-full flex-col items-center justify-center overflow-hidden md:col-span-4 md:px-10">
            <img
              :src="pcImage"
              alt="High-end gaming PC workstation"
              class="z-20 my-5 aspect-auto translate-x-3 translate-y-3 scale-90 grayscale hover:grayscale-0 transition-all duration-700 md:my-0 md:translate-x-0 md:translate-y-0 md:scale-100 md:group-hover:translate-x-6 md:group-hover:translate-y-6" />
          </router-link>
          <div
            class="relative z-10 col-span-3 flex h-full w-full flex-col justify-center bg-k-main md:pl-4 md:pr-20 lg:pl-6 lg:pr-24">
            <h2
              class="text-center text-5xl font-semibold uppercase md:text-start lg:text-6xl">
              Gaming <br />
              PC Setup
            </h2>
            <p
              class="mb-8 mt-4 text-center tracking-wide md:mb-10 md:text-start">
              Powerful gaming rig with premium components for ultimate
              performance and style.
            </p>
            <ButtonSolid
              to="/pc"
              content="VIEW SPECS"
              add="font-semibold hover:rotate-x-3"
              class="mb-10 self-center md:mb-0 md:self-start" />
          </div>
        </div>
      </div>
      <div
        class="order-3 flex w-full flex-col items-center justify-center gap-6 rounded-md bg-k-grey p-6 text-black md:order-none md:col-span-2 md:row-span-2 md:h-full md:items-start md:bg-white md:px-6">
        <h2 class="text-3xl font-semibold uppercase">Monitors</h2>
        <ButtonEmpty to="/monitors" content="view setup" />
      </div>
      <router-link
        to="/monitors"
        class="order-2 flex h-64 flex-col items-center justify-center overflow-hidden rounded-md bg-k-grey text-black md:order-none md:col-span-4 md:row-span-2 md:block md:h-full">
        <img
          class="scale-125 object-cover grayscale hover:grayscale-0 transition-all duration-500"
          :src="monitorsImage"
          alt="Triple monitor gaming setup"
          loading="lazy" />
      </router-link>
      <div
        class="order-4 col-span-3 row-span-2 overflow-hidden rounded-md bg-k-grey text-black md:order-none md:h-full">
        <img
          class="scale-125 object-cover grayscale hover:grayscale-0 transition-all duration-500"
          :src="peripheralsImage"
          alt="Gaming peripherals and keycaps"
          loading="lazy" />
      </div>
      <div
        class="order-5 col-span-3 row-span-2 flex w-full flex-col items-center justify-center gap-6 rounded-md bg-k-grey p-6 text-black md:order-none md:h-full md:items-start md:px-16">
        <h2 class="text-3xl font-semibold uppercase">Peripherals</h2>
        <ButtonEmpty to="/peripherals" content="see all" />
      </div>
    </div>
  </section>
</template>
