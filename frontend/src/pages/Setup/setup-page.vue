<script setup lang="ts">
import Navigation from "../../components/navigation-global.vue";
import Footer from "../../components/footer-global.vue";
import setupMainImage from "/products/workspace/station3.webp";
import setupSideImage from "/products/workspace/station1.webp";
import setupTopImage from "/products/workspace/station2.webp";
import setupCloseImage from "/products/workspace/station3.webp";
</script>

<template>
  <Navigation color="black" />
  <header class="flex w-full flex-col items-center bg-black lg:rounded-b-lg">
    <div
      class="my-10 flex w-4/5 max-w-6xl flex-col items-center justify-center lg:my-20">
      <h1
        class="text-4xl font-semibold uppercase tracking-wider text-white antialiased">
        Setup Overview
      </h1>
    </div>
  </header>
  <main
    class="main-container flex h-full w-screen flex-col items-center bg-white">
    <section class="mt-16 flex w-4/5 max-w-6xl flex-col md:mt-32">
      <div class="grid gap-8 md:grid-cols-2">
        <div>
          <h2 class="text-3xl font-bold text-k-black mb-4">My BattleStation</h2>
          <p class="text-black opacity-70 mb-6">
            Welcome to my complete gaming and work station. This is where
            passion for technology meets functionality. Every element has been
            carefully selected to ensure maximum performance and comfort during
            long work and gaming sessions.
          </p>
          <p class="text-black opacity-70">
            The setup consists of a powerful PC with Ryzen 7 5800X and RTX
            3060Ti, MacBook Pro M3 for mobile work, three monitors for maximum
            productivity, and professional audio equipment from EDIFIER and
            Novox.
          </p>
        </div>
        <div class="rounded-lg overflow-hidden cursor-pointer">
          <img
            :src="setupMainImage"
            alt="Main setup view"
            class="w-full h-full object-cover grayscale hover:grayscale-0 transition-all duration-500 transform hover:scale-105 shadow-lg hover:shadow-xl" />
        </div>
      </div>
    </section>

    <section class="mt-16 flex w-4/5 max-w-6xl flex-col md:mt-32">
      <h2 class="text-3xl font-bold text-k-black mb-8 text-center">
        Different Perspectives
      </h2>
      <div class="grid gap-8 md:grid-cols-3">
        <div class="rounded-lg overflow-hidden cursor-pointer group">
          <img
            :src="setupSideImage"
            alt="Side view"
            class="w-full h-64 object-cover grayscale hover:grayscale-0 transition-all duration-500 transform hover:scale-105 shadow-lg hover:shadow-xl" />
          <div
            class="p-4 bg-k-grey group-hover:bg-k-main transition-colors duration-300">
            <h3 class="font-semibold text-k-black">Side View</h3>
            <p class="text-sm text-k-black opacity-70">
              Side perspective showing monitor layout
            </p>
          </div>
        </div>
        <div class="rounded-lg overflow-hidden cursor-pointer group">
          <img
            :src="setupTopImage"
            alt="Top view"
            class="w-full h-64 object-cover grayscale hover:grayscale-0 transition-all duration-500 transform hover:scale-105 shadow-lg hover:shadow-xl" />
          <div
            class="p-4 bg-k-grey group-hover:bg-k-main transition-colors duration-300">
            <h3 class="font-semibold text-k-black">Top View</h3>
            <p class="text-sm text-k-black opacity-70">
              Desk space organization
            </p>
          </div>
        </div>
        <div class="rounded-lg overflow-hidden cursor-pointer group">
          <img
            :src="setupCloseImage"
            alt="Close up"
            class="w-full h-64 object-cover grayscale hover:grayscale-0 transition-all duration-500 transform hover:scale-105 shadow-lg hover:shadow-xl" />
          <div
            class="p-4 bg-k-grey group-hover:bg-k-main transition-colors duration-300">
            <h3 class="font-semibold text-k-black">Details</h3>
            <p class="text-sm text-k-black opacity-70">
              Peripherals and setup details
            </p>
          </div>
        </div>
      </div>
    </section>

    <section class="mt-16 flex w-4/5 max-w-6xl flex-col md:mt-32 mb-16">
      <h2 class="text-3xl font-bold text-k-black mb-8 text-center">
        Key Components
      </h2>
      <div class="grid gap-6 md:grid-cols-2">
        <div
          class="p-6 rounded-lg bg-k-grey hover:bg-k-main transition-colors duration-300 cursor-pointer">
          <h3 class="text-xl font-semibold text-k-black mb-3">Gaming PC</h3>
          <p class="text-k-black opacity-70">
            Heart of the setup - Ryzen 7 5800X, RTX 3060Ti, 64GB RAM in Phanteks
            NV7 case
          </p>
        </div>
        <div
          class="p-6 rounded-lg bg-k-grey hover:bg-k-main transition-colors duration-300 cursor-pointer">
          <h3 class="text-xl font-semibold text-k-black mb-3">
            MacBook Pro M3
          </h3>
          <p class="text-k-black opacity-70">
            16" laptop with M3 chip and 18GB RAM for mobile work and development
          </p>
        </div>
        <div
          class="p-6 rounded-lg bg-k-grey hover:bg-k-main transition-colors duration-300 cursor-pointer">
          <h3 class="text-xl font-semibold text-k-black mb-3">
            Triple Monitor
          </h3>
          <p class="text-k-black opacity-70">
            2x IIYAMA 75Hz + HP 144Hz for maximum productivity
          </p>
        </div>
        <div
          class="p-6 rounded-lg bg-k-grey hover:bg-k-main transition-colors duration-300 cursor-pointer">
          <h3 class="text-xl font-semibold text-k-black mb-3">Ergonomics</h3>
          <p class="text-k-black opacity-70">
            Custom desk with height adjustment and ergonomic chair
          </p>
        </div>
      </div>
    </section>

    <Footer />
  </main>
</template>
