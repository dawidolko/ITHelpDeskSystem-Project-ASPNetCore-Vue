<template>
  <div
    class="min-h-screen bg-zinc-900 flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
    <div
      class="max-w-md w-full space-y-8 bg-black p-8 rounded-xl shadow-2xl border border-gray-800">
      <div>
        <h2 class="text-center text-3xl font-extrabold text-white">
          Rejestracja
        </h2>
        <p class="mt-2 text-center text-sm text-gray-400">
          Masz już konto?
          <router-link
            to="/login"
            class="font-medium text-k-main hover:text-k-main/80 transition-colors">
            Zaloguj się
          </router-link>
        </p>
      </div>

      <div
        v-if="error"
        class="bg-red-900/30 border border-red-500 text-red-300 px-4 py-3 rounded-lg">
        {{ error }}
      </div>

      <form class="mt-8 space-y-6" @submit.prevent="handleRegister">
        <div class="space-y-4">
          <div class="grid grid-cols-2 gap-4">
            <div>
              <label
                for="firstName"
                class="block text-sm font-medium text-gray-300 mb-2"
                >Imię</label
              >
              <input
                id="firstName"
                v-model="formData.firstName"
                type="text"
                required
                class="appearance-none rounded-lg relative block w-full px-4 py-3 bg-gray-900 border border-gray-700 placeholder-gray-400 text-white focus:outline-none focus:ring-2 focus:ring-k-main focus:border-transparent transition-all sm:text-sm" />
            </div>
            <div>
              <label
                for="lastName"
                class="block text-sm font-medium text-gray-300 mb-2"
                >Nazwisko</label
              >
              <input
                id="lastName"
                v-model="formData.lastName"
                type="text"
                required
                class="appearance-none rounded-lg relative block w-full px-4 py-3 bg-gray-900 border border-gray-700 placeholder-gray-400 text-white focus:outline-none focus:ring-2 focus:ring-k-main focus:border-transparent transition-all sm:text-sm" />
            </div>
          </div>

          <div>
            <label
              for="email"
              class="block text-sm font-medium text-gray-300 mb-2"
              >Email</label
            >
            <input
              id="email"
              v-model="formData.email"
              type="email"
              required
              class="appearance-none rounded-lg relative block w-full px-4 py-3 bg-gray-900 border border-gray-700 placeholder-gray-400 text-white focus:outline-none focus:ring-2 focus:ring-k-main focus:border-transparent transition-all sm:text-sm" />
          </div>

          <div>
            <label
              for="password"
              class="block text-sm font-medium text-gray-300 mb-2"
              >Hasło</label
            >
            <input
              id="password"
              v-model="formData.password"
              type="password"
              required
              minlength="8"
              class="appearance-none rounded-lg relative block w-full px-4 py-3 bg-gray-900 border border-gray-700 placeholder-gray-400 text-white focus:outline-none focus:ring-2 focus:ring-k-main focus:border-transparent transition-all sm:text-sm" />
            <p class="mt-1 text-xs text-gray-400">
              Min. 8 znaków, 1 wielka, 1 mała, 1 cyfra, 1 znak specjalny
            </p>
          </div>

          <div>
            <label
              for="phoneNumber"
              class="block text-sm font-medium text-gray-300 mb-2"
              >Telefon (opcjonalne)</label
            >
            <input
              id="phoneNumber"
              v-model="formData.phoneNumber"
              type="tel"
              class="appearance-none rounded-lg relative block w-full px-4 py-3 bg-gray-900 border border-gray-700 placeholder-gray-400 text-white focus:outline-none focus:ring-2 focus:ring-k-main focus:border-transparent transition-all sm:text-sm" />
          </div>

          <div>
            <label
              for="department"
              class="block text-sm font-medium text-gray-300 mb-2"
              >Dział (opcjonalne)</label
            >
            <input
              id="department"
              v-model="formData.department"
              type="text"
              class="appearance-none rounded-lg relative block w-full px-4 py-3 bg-gray-900 border border-gray-700 placeholder-gray-400 text-white focus:outline-none focus:ring-2 focus:ring-k-main focus:border-transparent transition-all sm:text-sm" />
          </div>
        </div>

        <div>
          <button
            type="submit"
            :disabled="loading"
            class="group relative w-full flex justify-center py-3 px-4 border border-transparent text-sm font-medium rounded-lg text-black bg-k-main hover:bg-k-main/90 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-k-main disabled:opacity-50 disabled:cursor-not-allowed transition-all shadow-lg hover:shadow-k-main/50">
            <span v-if="!loading">Zarejestruj się</span>
            <span v-else>Rejestracja...</span>
          </button>
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

const formData = ref({
  firstName: "",
  lastName: "",
  email: "",
  password: "",
  phoneNumber: "",
  department: "",
});

const error = ref("");
const loading = ref(false);

async function handleRegister() {
  error.value = "";
  loading.value = true;

  const result = await authStore.register(formData.value);

  loading.value = false;

  if (result.success) {
    router.push("/dashboard");
  } else {
    error.value = result.message || "Rejestracja nie powiodła się";
  }
}
</script>
