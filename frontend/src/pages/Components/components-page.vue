<script setup lang="ts">
import Navigation from "../../components/navigation-global.vue";
import Footer from "../../components/footer-global.vue";
import {
  getAllCategories,
  getComponentsByCategory,
  getCategoryDisplayName,
} from "../../data/battlestation-utils";
</script>

<template>
  <Navigation color="black" />
  <header class="flex w-full flex-col items-center bg-black lg:rounded-b-lg">
    <div
      class="my-10 flex w-4/5 max-w-6xl flex-col items-center justify-center lg:my-20">
      <h1
        class="text-4xl font-semibold uppercase tracking-wider text-white antialiased">
        All Components
      </h1>
    </div>
  </header>
  <main
    class="main-container flex h-full w-screen flex-col items-center bg-white">
    <section class="mt-16 flex w-4/5 max-w-6xl flex-col md:mt-32">
      <div v-for="category in getAllCategories()" :key="category" class="mb-20">
        <div
          class="mb-10 flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4">
          <h2
            class="text-3xl md:text-4xl font-bold text-k-black group-hover:text-k-main transition-colors duration-300">
            {{ getCategoryDisplayName(category) }}
          </h2>
          <router-link
            :to="`/${category}`"
            class="text-k-main font-semibold hover:text-k-black px-4 py-2 border border-k-main rounded-lg hover:bg-k-main transition-all duration-300 text-center">
            View all components
          </router-link>
        </div>

        <div class="grid gap-8 sm:grid-cols-2 lg:grid-cols-3">
          <div
            v-for="component in getComponentsByCategory(category)"
            :key="component.id"
            class="group rounded-xl overflow-hidden bg-k-grey hover:bg-white border-2 border-transparent hover:border-k-main hover:shadow-2xl transition-all duration-500 cursor-pointer transform hover:-translate-y-2">
            <div class="aspect-square overflow-hidden relative">
              <img
                :src="component.image"
                :alt="component.name"
                class="w-full h-full object-cover grayscale group-hover:grayscale-0 transition-all duration-500 transform group-hover:scale-110" />
              <div
                class="absolute inset-0 bg-black opacity-20 group-hover:opacity-0 transition-opacity duration-500"></div>
            </div>

            <div class="p-6">
              <div class="flex items-center gap-3 mb-4">
                <span
                  v-if="component.type === 'main'"
                  class="px-3 py-1 bg-k-main text-xs font-bold rounded-full group-hover:bg-k-black group-hover:text-k-main transition-colors duration-300">
                  MAIN COMPONENT
                </span>
                <span
                  class="text-xs text-k-black opacity-50 uppercase font-medium tracking-wider group-hover:opacity-70 transition-opacity duration-300">
                  {{ getCategoryDisplayName(component.category) }}
                </span>
              </div>

              <h3
                class="font-bold text-k-black mb-2 group-hover:text-k-main transition-colors duration-300 text-lg">
                {{ component.name }}
              </h3>

              <p
                class="text-sm text-k-black opacity-70 mb-3 font-medium group-hover:opacity-90 transition-opacity duration-300">
                {{ component.model }}
              </p>

              <p
                class="text-sm text-k-black opacity-60 mb-4 leading-relaxed group-hover:opacity-80 transition-opacity duration-300">
                {{ component.description }}
              </p>

              <div
                class="flex justify-between items-center pt-4 border-t border-k-black border-opacity-10 group-hover:border-k-main group-hover:border-opacity-30 transition-all duration-300">
                <div class="flex items-center gap-2">
                  <span
                    class="w-2 h-2 bg-k-main rounded-full group-hover:bg-k-black transition-colors duration-300"></span>
                  <span
                    class="text-sm text-k-black opacity-60 font-medium group-hover:opacity-80 transition-opacity duration-300">
                    {{ component.specs.length }} specifications
                  </span>
                </div>
                <router-link
                  :to="`/${component.category}`"
                  class="text-sm text-k-main font-bold hover:text-k-black px-3 py-1 rounded-md hover:bg-k-main transition-all duration-300">
                  Details →
                </router-link>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>

    <Footer />
  </main>
</template>
