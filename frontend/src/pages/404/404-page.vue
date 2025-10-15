<script lang="ts" setup>
import { useRouter } from "vue-router";

const router = useRouter();

const goHome = () => {
  router.push("/");
};
</script>

<template>
  <main
    class="flex h-screen w-screen flex-col items-center justify-center bg-black text-white">
    <div
      class="flex max-h-screen flex-col items-center justify-center gap-6 p-10 text-center lg:w-1/3">
      <h1 class="text-7xl font-black text-k-main">404</h1>
      <p class="text-md tracking-widest opacity-70">Page not found!</p>
      <button
        @click="goHome"
        class="mt-8 rounded-lg bg-k-main px-6 py-3 font-bold text-black transition duration-300 hover:bg-yellow-400">
        Back to Dashboard
      </button>
    </div>
  </main>
</template>
