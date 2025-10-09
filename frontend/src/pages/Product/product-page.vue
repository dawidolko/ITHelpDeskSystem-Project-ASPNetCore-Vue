<script setup lang="ts">
import Navigation from "../../components/navigation-global.vue";
import Footer from "../../components/footer-global.vue";
import YmalBoxes from "../../components/ymal-boxes.vue";
import { getComponent } from "../../data/battlestation-utils";
import { computed } from "vue";

const props = defineProps<{
  category: string;
  componentId: number;
}>();

let item = computed(() => {
  return getComponent(props.category, props.componentId);
});
</script>

<template>
  <main class="flex h-full w-screen flex-col items-center bg-white">
    <Navigation color="black" />

    <div v-if="item" class="mt-16 flex w-4/5 max-w-6xl flex-col md:mt-32">
      <div class="grid gap-8 md:grid-cols-2 mb-16">
        <div class="rounded-lg overflow-hidden">
          <img
            :src="item.image"
            :alt="item.name"
            class="w-full h-full object-cover filter grayscale transition duration-300 hover:grayscale-0 hover:scale-110" />
        </div>
        <div class="flex flex-col justify-center">
          <div class="mb-4">
            <span
              v-if="item.type === 'main'"
              class="px-3 py-1 bg-k-main text-sm font-semibold rounded mb-4 inline-block">
              MAIN COMPONENT
            </span>
          </div>
          <h1 class="text-4xl font-bold text-k-black mb-2">{{ item.name }}</h1>
          <h2 class="text-2xl font-medium text-k-black opacity-80 mb-4">
            {{ item.model }}
          </h2>
          <p class="text-k-black opacity-70 mb-6">{{ item.description }}</p>

          <div class="bg-k-light-grey p-6 rounded-lg">
            <h3 class="text-xl font-semibold text-k-black mb-4">Full Specs:</h3>
            <div class="grid gap-3">
              <div
                v-for="(spec, index) in item.specs"
                :key="index"
                class="flex items-center">
                <span
                  class="w-3 h-3 bg-k-main rounded-full mr-4 flex-shrink-0"></span>
                <span class="text-k-black">{{ spec }}</span>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div
        v-if="item.detailImages && item.detailImages.length > 0"
        class="mb-16">
        <h3 class="text-2xl font-bold text-k-black mb-6">Detailed photos</h3>
        <div class="grid gap-4 md:grid-cols-3">
          <div
            v-for="(image, index) in item.detailImages"
            :key="index"
            class="rounded-lg overflow-hidden">
            <img
              :src="image"
              :alt="`${item.name} detail ${index + 1}`"
              class="w-full h-64 object-cover filter grayscale transition duration-300 hover:grayscale-0 hover:scale-110" />
          </div>
        </div>
      </div>
    </div>

    <YmalBoxes
      :productCategory="props.category"
      :productId="props.componentId" />
    <Footer />
  </main>
</template>
