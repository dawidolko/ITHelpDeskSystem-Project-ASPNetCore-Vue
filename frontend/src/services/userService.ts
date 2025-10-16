import api from "./api";

export interface User {
  id: number;
  firstName: string;
  lastName: string;
  fullName: string;
  email: string;
  phoneNumber?: string;
  role: "User" | "Technician" | "Admin";
  department?: string;
  isActive: boolean;
  createdAt: string;
}

export interface ChangeRoleRequest {
  userId: number;
  role: "User" | "Technician" | "Admin";
}

export const userService = {
  async getAllUsers(role?: string): Promise<User[]> {
    const params = role ? { role } : {};
    const response = await api.get<User[]>("/users", { params });
    return response.data;
  },

  async changeUserRole(
    userId: number,
    role: "User" | "Technician" | "Admin"
  ): Promise<void> {
    await api.put(`/users/${userId}/role`, { userId, role });
  },

  async deleteUser(userId: number): Promise<void> {
    await api.delete(`/users/${userId}`);
  },
};
