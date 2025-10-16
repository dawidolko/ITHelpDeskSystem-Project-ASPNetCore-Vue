import { defineStore } from "pinia";
import { ref, computed } from "vue";
import api from "@/services/api";

interface User {
  id: number;
  firstName: string;
  lastName: string;
  fullName: string;
  email: string;
  role: string;
  department: string | null;
  isActive: boolean;
}

interface AuthResponse {
  token: string;
  expiresAt: string;
  user: User;
}

export const useAuthStore = defineStore("auth", () => {
  const token = ref<string | null>(localStorage.getItem("token"));
  const user = ref<User | null>(
    JSON.parse(localStorage.getItem("user") || "null")
  );

  const isAuthenticated = computed(() => !!token.value);
  const isAdmin = computed(() => user.value?.role === "Admin");
  const isTechnician = computed(() => user.value?.role === "Technician");
  const isUser = computed(() => user.value?.role === "User");

  function setAuth(authData: AuthResponse) {
    token.value = authData.token;
    user.value = authData.user;
    localStorage.setItem("token", authData.token);
    localStorage.setItem("user", JSON.stringify(authData.user));
    api.defaults.headers.common["Authorization"] = `Bearer ${authData.token}`;
  }

  function clearAuth() {
    token.value = null;
    user.value = null;
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    delete api.defaults.headers.common["Authorization"];
  }

  async function login(email: string, password: string) {
    try {
      const response = await api.post<AuthResponse>("/auth/login", {
        email,
        password,
      });
      setAuth(response.data);
      return { success: true };
    } catch (error: any) {
      return {
        success: false,
        message: error.response?.data?.message || "Login failed",
      };
    }
  }

  async function register(data: {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    phoneNumber?: string;
    department?: string;
  }) {
    try {
      const response = await api.post<AuthResponse>("/auth/register", data);
      setAuth(response.data);
      return { success: true };
    } catch (error: any) {
      return {
        success: false,
        message: error.response?.data?.message || "Registration failed",
      };
    }
  }

  function logout() {
    clearAuth();
  }

  function initAuth() {
    if (token.value) {
      api.defaults.headers.common["Authorization"] = `Bearer ${token.value}`;
    }
  }

  return {
    token,
    user,
    isAuthenticated,
    isAdmin,
    isTechnician,
    isUser,
    login,
    register,
    logout,
    initAuth,
  };
});
