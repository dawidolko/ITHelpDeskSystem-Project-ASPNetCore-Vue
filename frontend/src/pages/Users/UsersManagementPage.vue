<script setup lang="ts">
import { onMounted, ref, computed } from "vue";
import { useAuthStore } from "@/stores/authStore";
import { userService, type User } from "@/services/userService";
import NavigationGlobal from "@/components/navigation-global.vue";
import FooterGlobal from "@/components/footer-global.vue";
import { useRouter } from "vue-router";

const authStore = useAuthStore();
const router = useRouter();

const users = ref<User[]>([]);
const loading = ref(false);
const error = ref("");
const filterRole = ref<string>("");
const editingUserId = ref<number | null>(null);
const newRole = ref<"User" | "Technician" | "Admin">("User");

onMounted(async () => {
  if (!authStore.isAdmin) {
    router.push("/");
    return;
  }
  await fetchUsers();
});

const fetchUsers = async () => {
  loading.value = true;
  error.value = "";
  try {
    users.value = await userService.getAllUsers(filterRole.value || undefined);
  } catch (err: any) {
    error.value = "Nie udało się pobrać użytkowników";
  } finally {
    loading.value = false;
  }
};

const filteredUsers = computed(() => {
  if (!filterRole.value) return users.value;
  return users.value.filter((u) => u.role === filterRole.value);
});

const startEditRole = (user: User) => {
  editingUserId.value = user.id;
  newRole.value = user.role;
};

const cancelEdit = () => {
  editingUserId.value = null;
};

const saveRole = async (userId: number) => {
  try {
    await userService.changeUserRole(userId, newRole.value);
    editingUserId.value = null;
    await fetchUsers();
  } catch (err: any) {
    alert(
      "Nie udało się zmienić roli: " +
        (err.response?.data?.message || err.message)
    );
  }
};

const deleteUser = async (userId: number, userName: string) => {
  if (!confirm(`Czy na pewno chcesz usunąć użytkownika ${userName}?`)) return;

  try {
    await userService.deleteUser(userId);
    await fetchUsers();
  } catch (err: any) {
    alert(
      "Nie udało się usunąć użytkownika: " +
        (err.response?.data?.message || err.message)
    );
  }
};

const getRoleBadgeColor = (role: string) => {
  switch (role) {
    case "Admin":
      return "bg-red-500/20 text-red-400 border-red-500";
    case "Technician":
      return "bg-blue-500/20 text-blue-400 border-blue-500";
    case "User":
      return "bg-green-500/20 text-green-400 border-green-500";
    default:
      return "bg-gray-500/20 text-gray-400 border-gray-500";
  }
};
</script>

<template>
  <div class="min-h-screen w-screen bg-zinc-900 text-white">
    <NavigationGlobal color="black" />

    <section class="flex w-full items-center justify-center py-12">
      <div class="w-4/5 max-w-7xl md:w-11/12 lg:w-4/5">
        <div class="mb-8">
          <h1 class="mb-4 text-4xl font-extrabold tracking-tight">
            Zarządzanie <span class="text-k-main">Użytkownikami</span>
          </h1>
          <p class="text-lg text-zinc-400">
            Panel administratora - zarządzaj rolami i użytkownikami systemu
          </p>
        </div>

        <div class="mb-6 flex items-center gap-4">
          <label class="text-sm font-medium text-zinc-400"
            >Filtruj według roli:</label
          >
          <select
            v-model="filterRole"
            @change="fetchUsers"
            class="rounded-lg border border-zinc-700 bg-black px-4 py-2 text-white focus:border-k-main focus:outline-none">
            <option value="">Wszystkie</option>
            <option value="Admin">Admin</option>
            <option value="Technician">Technician</option>
            <option value="User">User</option>
          </select>
        </div>

        <div
          v-if="error"
          class="mb-6 rounded-lg border border-red-500 bg-red-500/10 px-4 py-3 text-red-400">
          {{ error }}
        </div>

        <div v-if="loading" class="text-center text-zinc-400">
          <p>Ładowanie użytkowników...</p>
        </div>

        <div
          v-else
          class="overflow-x-auto rounded-lg border border-zinc-800 bg-black">
          <table class="w-full">
            <thead>
              <tr class="border-b border-zinc-800 bg-zinc-900">
                <th
                  class="px-6 py-4 text-left text-sm font-bold uppercase text-zinc-400">
                  ID
                </th>
                <th
                  class="px-6 py-4 text-left text-sm font-bold uppercase text-zinc-400">
                  Imię i Nazwisko
                </th>
                <th
                  class="px-6 py-4 text-left text-sm font-bold uppercase text-zinc-400">
                  Email
                </th>
                <th
                  class="px-6 py-4 text-left text-sm font-bold uppercase text-zinc-400">
                  Dział
                </th>
                <th
                  class="px-6 py-4 text-left text-sm font-bold uppercase text-zinc-400">
                  Rola
                </th>
                <th
                  class="px-6 py-4 text-left text-sm font-bold uppercase text-zinc-400">
                  Status
                </th>
                <th
                  class="px-6 py-4 text-center text-sm font-bold uppercase text-zinc-400">
                  Akcje
                </th>
              </tr>
            </thead>
            <tbody>
              <tr
                v-for="user in filteredUsers"
                :key="user.id"
                class="border-b border-zinc-800 transition-colors hover:bg-zinc-900/50">
                <td class="px-6 py-4 text-zinc-300">{{ user.id }}</td>
                <td class="px-6 py-4 font-medium text-white">
                  {{ user.fullName }}
                </td>
                <td class="px-6 py-4 text-zinc-300">{{ user.email }}</td>
                <td class="px-6 py-4 text-zinc-400">
                  {{ user.department || "-" }}
                </td>
                <td class="px-6 py-4">
                  <div
                    v-if="editingUserId === user.id"
                    class="flex items-center gap-2">
                    <select
                      v-model="newRole"
                      class="rounded border border-zinc-700 bg-black px-3 py-1 text-sm text-white focus:border-k-main focus:outline-none">
                      <option value="User">User</option>
                      <option value="Technician">Technician</option>
                      <option value="Admin">Admin</option>
                    </select>
                    <button
                      @click="saveRole(user.id)"
                      class="rounded bg-green-600 px-3 py-1 text-xs font-bold text-white hover:bg-green-700">
                      ✓
                    </button>
                    <button
                      @click="cancelEdit"
                      class="rounded bg-red-600 px-3 py-1 text-xs font-bold text-white hover:bg-red-700">
                      ✕
                    </button>
                  </div>
                  <span
                    v-else
                    :class="getRoleBadgeColor(user.role)"
                    class="inline-block rounded-full border px-3 py-1 text-xs font-bold">
                    {{ user.role }}
                  </span>
                </td>
                <td class="px-6 py-4">
                  <span
                    :class="
                      user.isActive
                        ? 'bg-green-500/20 text-green-400'
                        : 'bg-red-500/20 text-red-400'
                    "
                    class="inline-block rounded-full px-3 py-1 text-xs font-bold">
                    {{ user.isActive ? "Aktywny" : "Nieaktywny" }}
                  </span>
                </td>
                <td class="px-6 py-4">
                  <div class="flex items-center justify-center gap-2">
                    <button
                      v-if="
                        editingUserId !== user.id &&
                        user.id !== authStore.user?.id
                      "
                      @click="startEditRole(user)"
                      class="rounded bg-blue-600 px-3 py-1 text-xs font-bold text-white transition hover:bg-blue-700">
                      Zmień rolę
                    </button>
                    <button
                      v-if="user.id !== authStore.user?.id"
                      @click="deleteUser(user.id, user.fullName)"
                      class="rounded bg-red-600 px-3 py-1 text-xs font-bold text-white transition hover:bg-red-700">
                      Usuń
                    </button>
                  </div>
                </td>
              </tr>
            </tbody>
          </table>

          <div
            v-if="filteredUsers.length === 0"
            class="py-12 text-center text-zinc-400">
            Brak użytkowników do wyświetlenia
          </div>
        </div>
      </div>
    </section>

    <FooterGlobal color="black" />
  </div>
</template>
