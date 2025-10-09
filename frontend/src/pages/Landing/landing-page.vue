<script setup lang="ts">
import Navigation from "../../components/navigation-global.vue";
import BattlestationHero from "./Components/battlestation-hero.vue";
import ComponentsGrid from "./Components/components-grid.vue";
import StatsSection from "./Components/stats-section.vue";
import AboutSection from "./Components/about-section.vue";
import Footer from "../../components/footer-global.vue";
</script>

<template>
  <div
    class="main-container flex h-full w-screen flex-col items-center bg-white">
    <Navigation color="k-black" />
    <main class="h-full w-screen">
      <BattlestationHero />
      <ComponentsGrid />
      <StatsSection />
      <AboutSection />
    </main>
    <Footer />
  </div>
</template>
