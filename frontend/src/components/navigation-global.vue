<script setup lang="ts">
import { computed, ref } from "vue";
import { useAuthStore } from "@/stores/authStore";
import { useRouter } from "vue-router";

interface Props {
  color?: "black" | "transparent" | "k-black";
}

const props = withDefaults(defineProps<Props>(), {
  color: "transparent",
});

const authStore = useAuthStore();
const router = useRouter();

let style = computed(() => {
  return "bg-" + props.color;
});

const hamburgerState = ref("hide");

function showHamburger(): void {
  hamburgerState.value = "show";
}

function hideHamburger(): void {
  hamburgerState.value = "hide";
}

function handleLogout(): void {
  authStore.logout();
  router.push("/login");
}
</script>

<template>
  <header
    id="navi"
    class="main-container flex h-full w-screen flex-col items-center"
    :class="style"
    data-test="nav-desktop">
    <div
      class="relative flex w-4/5 max-w-6xl flex-row items-center justify-between border-b border-zinc-500 py-6 md:w-11/12 lg:w-4/5">
      <button
        id="hamburger"
        class="select-none lg:hidden"
        @click="showHamburger()"
        data-test="hamburger">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          stroke-width="2"
          stroke="currentColor"
          class="h-8 w-8">
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            d="M3.75 6.75h16.5M3.75 12h16.5m-16.5 5.25h16.5" />
        </svg>
      </button>

      <router-link
        to="/"
        class="text-3xl font-extrabold tracking-tight antialiased transition duration-300 hover:scale-110 hover:text-k-main"
        data-test="nav-logo">
        IT HelpDesk
      </router-link>

      <nav class="hidden tracking-widest lg:flex lg:gap-8">
        <template v-if="authStore.isAuthenticated">
          <router-link
            to="/"
            class="uppercase text-white transition duration-300 hover:text-k-main active:translate-y-0.5"
            data-test="nav-home"
            >Dashboard
          </router-link>
          <router-link
            to="/tickets"
            class="uppercase text-white transition duration-300 hover:text-k-main active:translate-y-0.5"
            data-test="nav-tickets"
            >Tickets
          </router-link>
          <router-link
            to="/create-ticket"
            class="uppercase text-white transition duration-300 hover:text-k-main active:translate-y-0.5"
            data-test="nav-create"
            >New Ticket
          </router-link>
          <router-link
            to="/statistics"
            class="uppercase text-white transition duration-300 hover:text-k-main active:translate-y-0.5"
            data-test="nav-stats"
            >Statistics
          </router-link>
          <router-link
            v-if="authStore.isAdmin"
            to="/users"
            class="uppercase text-white transition duration-300 hover:text-k-main active:translate-y-0.5"
            data-test="nav-users"
            >Users
          </router-link>
        </template>
      </nav>

      <div
        v-if="authStore.isAuthenticated"
        class="hidden lg:flex lg:items-center lg:gap-4">
        <span class="text-sm text-white">
          {{ authStore.user?.fullName }} ({{ authStore.user?.role }})
        </span>
        <button
          @click="handleLogout"
          class="uppercase text-white transition duration-300 hover:text-k-main active:translate-y-0.5">
          Wyloguj
        </button>
      </div>

      <div v-else class="hidden lg:flex lg:gap-4">
        <router-link
          to="/login"
          class="uppercase text-white transition duration-300 hover:text-k-main active:translate-y-0.5">
          Zaloguj
        </router-link>
        <router-link
          to="/register"
          class="uppercase text-white transition duration-300 hover:text-k-main active:translate-y-0.5">
          Rejestruj
        </router-link>
      </div>
    </div>

    <transition>
      <nav
        class="absolute flex w-screen flex-row justify-around gap-2 bg-black p-9 text-xs font-semibold tracking-widest"
        v-if="hamburgerState === 'show'"
        :class="$route.path === '/' ? 'bg-k-black' : 'bg-black'"
        data-test="nav-mobile">
        <button
          class="lg:text-md uppercase text-white transition duration-300 hover:text-k-main active:translate-y-0.5 md:text-lg"
          @click="hideHamburger()"
          data-test="close-hamburger">
          Close
        </button>

        <template v-if="authStore.isAuthenticated">
          <router-link
            to="/"
            class="lg:text-md uppercase text-white transition duration-300 hover:text-k-main active:translate-y-0.5 md:text-lg"
            :class="$route.path === '/' && 'hidden'"
            @click="hideHamburger()"
            data-test="mobile-nav-home"
            >Dashboard
          </router-link>
          <router-link
            to="/tickets"
            class="lg:text-md uppercase text-white transition duration-300 hover:text-k-main active:translate-y-0.5 md:text-lg"
            :class="$route.path === '/tickets' && 'hidden'"
            @click="hideHamburger()"
            data-test="mobile-nav-tickets"
            >Tickets
          </router-link>
          <router-link
            to="/create-ticket"
            class="lg:text-md uppercase text-white transition duration-300 hover:text-k-main active:translate-y-0.5 md:text-lg"
            :class="$route.path === '/create-ticket' && 'hidden'"
            @click="hideHamburger()"
            data-test="mobile-nav-create"
            >New Ticket
          </router-link>
          <router-link
            to="/statistics"
            class="lg:text-md uppercase text-white transition duration-300 hover:text-k-main active:translate-y-0.5 md:text-lg"
            :class="$route.path === '/statistics' && 'hidden'"
            @click="hideHamburger()"
            data-test="mobile-nav-stats"
            >Statistics
          </router-link>
          <router-link
            v-if="authStore.isAdmin"
            to="/users"
            class="lg:text-md uppercase text-white transition duration-300 hover:text-k-main active:translate-y-0.5 md:text-lg"
            :class="$route.path === '/users' && 'hidden'"
            @click="hideHamburger()"
            data-test="mobile-nav-users"
            >Users
          </router-link>
          <button
            class="lg:text-md uppercase text-white transition duration-300 hover:text-k-main active:translate-y-0.5 md:text-lg"
            @click="
              handleLogout();
              hideHamburger();
            ">
            Wyloguj
          </button>
        </template>

        <template v-else>
          <router-link
            to="/login"
            class="lg:text-md uppercase text-white transition duration-300 hover:text-k-main active:translate-y-0.5 md:text-lg"
            @click="hideHamburger()">
            Zaloguj
          </router-link>
          <router-link
            to="/register"
            class="lg:text-md uppercase text-white transition duration-300 hover:text-k-main active:translate-y-0.5 md:text-lg"
            @click="hideHamburger()">
            Rejestruj
          </router-link>
        </template>
      </nav>
    </transition>
  </header>
</template>
