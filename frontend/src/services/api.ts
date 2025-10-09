import axios from "axios";

const API_URL = import.meta.env.VITE_API_URL || "http://localhost:5000/api";

// Create axios instance with default config
const apiClient = axios.create({
  baseURL: API_URL,
  headers: {
    "Content-Type": "application/json",
  },
  // Allow self-signed certificates in development
  ...(import.meta.env.DEV && { httpsAgent: { rejectUnauthorized: false } }),
});

export default apiClient;
