import axios from 'axios';

const API_BASE_URL = 'http://localhost:5000/api';

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

export const productService = {
  // Get all products with optional filtering and sorting
  async getProducts(filters = {}) {
    const params = new URLSearchParams();
    
    if (filters.sortBy) params.append('sortBy', filters.sortBy);
    if (filters.sortOrder) params.append('sortOrder', filters.sortOrder);
    if (filters.category) params.append('category', filters.category);
    if (filters.minPrice) params.append('minPrice', filters.minPrice);
    if (filters.maxPrice) params.append('maxPrice', filters.maxPrice);
    if (filters.search) params.append('search', filters.search);
    
    const response = await api.get(`/products?${params.toString()}`);
    return response.data;
  },

  // Get a single product by ID
  async getProduct(id) {
    const response = await api.get(`/products/${id}`);
    return response.data;
  },

  // Create a new product
  async createProduct(product) {
    const response = await api.post('/products', product);
    return response.data;
  },

  // Update an existing product
  async updateProduct(id, product) {
    const response = await api.put(`/products/${id}`, product);
    return response.data;
  },

  // Delete a product
  async deleteProduct(id) {
    const response = await api.delete(`/products/${id}`);
    return response.data;
  },

  // Get all categories
  async getCategories() {
    const response = await api.get('/products/categories');
    return response.data;
  },
};

export default api;
