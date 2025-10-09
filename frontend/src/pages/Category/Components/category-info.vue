<script setup lang="ts">
import { getCategoryDisplayName } from "../../../data/battlestation-utils";

const props = defineProps<{
  category: string;
}>();

const categoryDescriptions: { [key: string]: string } = {
  pc: "My main computer is a powerful machine for gaming and work. Ryzen 7 5800X combined with RTX 3060Ti and 64GB RAM ensures smooth performance in every task.",
  laptop:
    "MacBook Pro M3 is my choice for mobile work. Apple M3 chip and 18GB RAM guarantee performance in professional applications.",
  monitors:
    "Three-monitor setup ensures maximum productivity. Two IIYAMA for work and HP 144Hz for gaming.",
  peripherals:
    "Razer peripherals provide precision and comfort during long gaming and work sessions.",
  audio:
    "Professional audio setup with EDIFIER speakers and Novox NC1 microphone for streaming and calls.",
  workspace:
    "Ergonomic workstation with custom desk and height adjustment for maximum comfort.",
};

const categoryName = getCategoryDisplayName(props.category);
const description = categoryDescriptions[props.category] || "";
</script>

<template>
  <section
    class="mt-20 flex h-full flex-col items-center justify-center md:mt-32 lg:mt-40 bg-k-light-grey py-20">
    <div
      class="flex h-full w-4/5 max-w-6xl flex-col items-center gap-12 md:w-11/12 lg:w-4/5">
      <div class="flex h-full flex-col items-center justify-center text-center">
        <h3
          class="text-4xl font-bold uppercase text-k-black md:text-5xl lg:text-6xl">
          About <br class="hidden xl:inline" />
          <span class="text-k-main drop-shadow-sm">{{ categoryName }}</span>
        </h3>
        <p
          class="mt-8 max-w-4xl text-lg leading-relaxed text-k-black opacity-80 md:mt-10 md:text-xl">
          {{ description }}
        </p>
      </div>

      <div
        class="grid grid-cols-2 gap-4 md:grid-cols-3 lg:grid-cols-6 mt-12 w-full max-w-4xl">
        <router-link
          to="/pc"
          class="group relative rounded-xl bg-white border-2 border-k-grey p-3 md:p-4 lg:p-6 text-center transition-all duration-300 hover:border-k-main hover:shadow-xl hover:-translate-y-1 cursor-pointer"
          :class="{
            'bg-k-main border-k-main shadow-lg': category === 'pc',
            'hover:bg-k-main': category !== 'pc',
          }">
          <span
            class="font-bold text-k-black text-sm md:text-base lg:text-lg group-hover:text-k-black transition-colors duration-300"
            :class="{ 'text-k-black': category === 'pc' }">
            PC
          </span>
          <div
            v-if="category === 'pc'"
            class="absolute -top-2 -right-2 w-4 h-4 bg-k-black rounded-full"></div>
        </router-link>

        <router-link
          to="/laptop"
          class="group relative rounded-xl bg-white border-2 border-k-grey p-3 md:p-4 lg:p-6 text-center transition-all duration-300 hover:border-k-main hover:shadow-xl hover:-translate-y-1 cursor-pointer"
          :class="{
            'bg-k-main border-k-main shadow-lg': category === 'laptop',
            'hover:bg-k-main': category !== 'laptop',
          }">
          <span
            class="font-bold text-k-black text-sm md:text-base lg:text-lg group-hover:text-k-black transition-colors duration-300"
            :class="{ 'text-k-black': category === 'laptop' }">
            Laptop
          </span>
          <div
            v-if="category === 'laptop'"
            class="absolute -top-2 -right-2 w-4 h-4 bg-k-black rounded-full"></div>
        </router-link>

        <router-link
          to="/monitors"
          class="group relative rounded-xl bg-white border-2 border-k-grey p-3 md:p-4 lg:p-6 text-center transition-all duration-300 hover:border-k-main hover:shadow-xl hover:-translate-y-1 cursor-pointer"
          :class="{
            'bg-k-main border-k-main shadow-lg': category === 'monitors',
            'hover:bg-k-main': category !== 'monitors',
          }">
          <span
            class="font-bold text-k-black text-sm md:text-base lg:text-lg group-hover:text-k-black transition-colors duration-300"
            :class="{ 'text-k-black': category === 'monitors' }">
            Monitor
          </span>
          <div
            v-if="category === 'monitors'"
            class="absolute -top-2 -right-2 w-4 h-4 bg-k-black rounded-full"></div>
        </router-link>

        <router-link
          to="/peripherals"
          class="group relative rounded-xl bg-white border-2 border-k-grey p-3 md:p-4 lg:p-6 text-center transition-all duration-300 hover:border-k-main hover:shadow-xl hover:-translate-y-1 cursor-pointer"
          :class="{
            'bg-k-main border-k-main shadow-lg': category === 'peripherals',
            'hover:bg-k-main': category !== 'peripherals',
          }">
          <span
            class="font-bold text-k-black text-sm md:text-base lg:text-lg group-hover:text-k-black transition-colors duration-300"
            :class="{ 'text-k-black': category === 'peripherals' }">
            Perip
          </span>
          <div
            v-if="category === 'peripherals'"
            class="absolute -top-2 -right-2 w-4 h-4 bg-k-black rounded-full"></div>
        </router-link>

        <router-link
          to="/audio"
          class="group relative rounded-xl bg-white border-2 border-k-grey p-3 md:p-4 lg:p-6 text-center transition-all duration-300 hover:border-k-main hover:shadow-xl hover:-translate-y-1 cursor-pointer"
          :class="{
            'bg-k-main border-k-main shadow-lg': category === 'audio',
            'hover:bg-k-main': category !== 'audio',
          }">
          <span
            class="font-bold text-k-black text-sm md:text-base lg:text-lg group-hover:text-k-black transition-colors duration-300"
            :class="{ 'text-k-black': category === 'audio' }">
            Audio
          </span>
          <div
            v-if="category === 'audio'"
            class="absolute -top-2 -right-2 w-4 h-4 bg-k-black rounded-full"></div>
        </router-link>

        <router-link
          to="/workspace"
          class="group relative rounded-xl bg-white border-2 border-k-grey p-3 md:p-4 lg:p-6 text-center transition-all duration-300 hover:border-k-main hover:shadow-xl hover:-translate-y-1 cursor-pointer"
          :class="{
            'bg-k-main border-k-main shadow-lg': category === 'workspace',
            'hover:bg-k-main': category !== 'workspace',
          }">
          <span
            class="font-bold text-k-black text-sm md:text-base lg:text-lg group-hover:text-k-black transition-colors duration-300"
            :class="{ 'text-k-black': category === 'workspace' }">
            Space
          </span>
          <div
            v-if="category === 'workspace'"
            class="absolute -top-2 -right-2 w-4 h-4 bg-k-black rounded-full"></div>
        </router-link>
      </div>
    </div>
  </section>
</template>
