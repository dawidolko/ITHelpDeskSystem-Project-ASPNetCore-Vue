<script setup lang="ts">
import Navigation from "../../components/navigation-global.vue";
import ComponentShowcase from "../../pages/Category/Components/component-showcase.vue";
import CategoryInfo from "../../pages/Category/Components/category-info.vue";
import Footer from "../../components/footer-global.vue";
import {
  getComponentsByCategory,
  getCategoryDisplayName,
} from "../../data/battlestation-utils";
import { computed } from "vue";

const props = defineProps<{
  category: string;
}>();

const components = computed(() => getComponentsByCategory(props.category));
const categoryName = computed(() => getCategoryDisplayName(props.category));
</script>

<template>
  <Navigation color="black" />
  <header class="flex w-full flex-col items-center bg-black lg:rounded-b-lg">
    <div
      class="my-10 flex w-4/5 max-w-6xl flex-col items-center justify-center lg:my-20">
      <h1
        class="text-4xl font-semibold uppercase tracking-wider text-white antialiased">
        {{ categoryName }}
      </h1>
    </div>
  </header>
  <main
    class="main-container flex h-full w-screen flex-col items-center bg-white">
    <ComponentShowcase
      v-for="(component, index) in components"
      :item="component"
      :category="props.category"
      :data-test="`component-${component.category}-${component.id}`"
      :key="index" />
    <CategoryInfo :category="props.category" />
    <Footer />
  </main>
</template>
