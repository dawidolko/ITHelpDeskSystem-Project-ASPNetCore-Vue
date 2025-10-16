<template>
  <div
    class="min-h-screen bg-zinc-900 flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
    <div
      class="max-w-md w-full space-y-8 bg-black p-8 rounded-xl shadow-2xl border border-gray-800">
      <div>
        <h2 class="text-center text-3xl font-extrabold text-white">
          Zaloguj się
        </h2>
        <p class="mt-2 text-center text-sm text-gray-400">
          Nie masz konta?
          <router-link
            to="/register"
            class="font-medium text-k-main hover:text-k-main/80 transition-colors">
            Zarejestruj się
          </router-link>
        </p>
      </div>

      <div
        v-if="error"
        class="bg-red-900/30 border border-red-500 text-red-300 px-4 py-3 rounded-lg">
        {{ error }}
      </div>

      <form class="mt-8 space-y-6" @submit.prevent="handleLogin">
        <div class="rounded-md space-y-4">
          <div>
            <label
              for="email"
              class="block text-sm font-medium text-gray-300 mb-2"
              >Email</label
            >
            <input
              id="email"
              v-model="email"
              type="email"
              required
              class="appearance-none rounded-lg relative block w-full px-4 py-3 bg-gray-900 border border-gray-700 placeholder-gray-400 text-white focus:outline-none focus:ring-2 focus:ring-k-main focus:border-transparent transition-all sm:text-sm"
              placeholder="user@firma.pl" />
          </div>
          <div>
            <label
              for="password"
              class="block text-sm font-medium text-gray-300 mb-2"
              >Hasło</label
            >
            <input
              id="password"
              v-model="password"
              type="password"
              required
              class="appearance-none rounded-lg relative block w-full px-4 py-3 bg-gray-900 border border-gray-700 placeholder-gray-400 text-white focus:outline-none focus:ring-2 focus:ring-k-main focus:border-transparent transition-all sm:text-sm"
              placeholder="••••••••" />
          </div>
        </div>

        <div>
          <button
            type="submit"
            :disabled="loading"
            class="group relative w-full flex justify-center py-3 px-4 border border-transparent text-sm font-medium rounded-lg text-black bg-k-main hover:bg-k-main/90 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-k-main disabled:opacity-50 disabled:cursor-not-allowed transition-all shadow-lg hover:shadow-k-main/50">
            <span v-if="!loading">Zaloguj</span>
            <span v-else>Logowanie...</span>
          </button>
        </div>

        <div
          class="text-center text-sm text-gray-400 mt-4 bg-gray-900 p-4 rounded-lg border border-gray-800">
          <p class="font-semibold mb-2 text-white">Testowe konta:</p>
          <p>
            Admin: <span class="text-k-main">admin@firma.pl</span> / Admin123!
          </p>
          <p>
            Technician: <span class="text-k-main">tech@firma.pl</span> /
            Tech123!
          </p>
          <p>User: <span class="text-k-main">user@firma.pl</span> / User123!</p>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";
import { useAuthStore } from "@/stores/authStore";

const router = useRouter();
const authStore = useAuthStore();

const email = ref("");
const password = ref("");
const error = ref("");
const loading = ref(false);

async function handleLogin() {
  error.value = "";
  loading.value = true;

  const result = await authStore.login(email.value, password.value);

  loading.value = false;

  if (result.success) {
    router.push("/dashboard");
  } else {
    error.value = result.message || "Nieprawidłowy email lub hasło";
  }
}
</script>
