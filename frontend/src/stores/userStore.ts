import { defineStore } from "pinia";
import { ref, computed } from "vue";
import type { UserSummary } from "../types/ticket.types";
import { userService } from "../services/ticketService";

export const useUserStore = defineStore("user", () => {
  const currentUser = ref<UserSummary | null>(null);
  const users = ref<UserSummary[]>([]);
  const technicians = ref<UserSummary[]>([]);
  const loading = ref(false);
  const error = ref<string | null>(null);

  const isLoggedIn = computed(() => currentUser.value !== null);
  const isAdmin = computed(() => currentUser.value?.role === "Admin");
  const isTechnician = computed(
    () =>
      currentUser.value?.role === "Technician" ||
      currentUser.value?.role === "Admin"
  );

  async function fetchUsers() {
    loading.value = true;
    error.value = null;
    try {
      users.value = await userService.getUsers();
    } catch (err) {
      error.value = "Failed to fetch users";
      console.error("Error fetching users:", err);
    } finally {
      loading.value = false;
    }
  }

  async function fetchTechnicians() {
    loading.value = true;
    error.value = null;
    try {
      technicians.value = await userService.getTechnicians();
    } catch (err) {
      error.value = "Failed to fetch technicians";
      console.error("Error fetching technicians:", err);
    } finally {
      loading.value = false;
    }
  }

  function setCurrentUser(user: UserSummary) {
    currentUser.value = user;
    localStorage.setItem("currentUser", JSON.stringify(user));
  }

  function loadCurrentUser() {
    const stored = localStorage.getItem("currentUser");
    if (stored) {
      currentUser.value = JSON.parse(stored);
    } else {
      currentUser.value = {
        id: 1,
        fullName: "Jan Kowalski",
        email: "jan.kowalski@company.com",
        role: "Admin",
        department: "IT Administration",
      };
      localStorage.setItem("currentUser", JSON.stringify(currentUser.value));
    }
  }

  function logout() {
    currentUser.value = null;
    localStorage.removeItem("currentUser");
  }

  return {
    currentUser,
    users,
    technicians,
    loading,
    error,
    isLoggedIn,
    isAdmin,
    isTechnician,
    fetchUsers,
    fetchTechnicians,
    setCurrentUser,
    loadCurrentUser,
    logout,
  };
});
