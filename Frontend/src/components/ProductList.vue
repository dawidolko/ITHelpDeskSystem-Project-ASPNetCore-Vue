<template>
  <div class="container">
    <h1>📦 Product Management System</h1>

    <!-- Statistics -->
    <div class="stats">
      <div class="stat-card">
        <h3>Total Products</h3>
        <p>{{ products.length }}</p>
      </div>
      <div class="stat-card">
        <h3>Total Value</h3>
        <p>${{ totalValue.toFixed(2) }}</p>
      </div>
      <div class="stat-card">
        <h3>Categories</h3>
        <p>{{ categories.length }}</p>
      </div>
    </div>

    <!-- Error Message -->
    <div v-if="error" class="error">
      {{ error }}
    </div>

    <!-- Controls for filtering and sorting -->
    <div class="controls">
      <div class="control-group">
        <label>Search</label>
        <input 
          type="text" 
          v-model="filters.search" 
          placeholder="Search by name..."
          @input="fetchProducts"
        />
      </div>

      <div class="control-group">
        <label>Category</label>
        <select v-model="filters.category" @change="fetchProducts">
          <option value="">All Categories</option>
          <option v-for="category in categories" :key="category" :value="category">
            {{ category }}
          </option>
        </select>
      </div>

      <div class="control-group">
        <label>Min Price</label>
        <input 
          type="number" 
          v-model.number="filters.minPrice" 
          placeholder="Min"
          @input="fetchProducts"
        />
      </div>

      <div class="control-group">
        <label>Max Price</label>
        <input 
          type="number" 
          v-model.number="filters.maxPrice" 
          placeholder="Max"
          @input="fetchProducts"
        />
      </div>

      <div class="control-group" style="align-self: flex-end;">
        <button @click="resetFilters" class="btn btn-secondary">
          Reset Filters
        </button>
      </div>

      <div class="control-group" style="align-self: flex-end;">
        <button @click="showAddModal = true" class="btn btn-primary">
          + Add Product
        </button>
      </div>
    </div>

    <!-- Loading State -->
    <div v-if="loading" class="loading">
      Loading products...
    </div>

    <!-- Products Table -->
    <div v-else-if="products.length > 0" class="table-container">
      <table>
        <thead>
          <tr>
            <th @click="sortBy('id')" class="sortable" :class="getSortClass('id')">
              ID
            </th>
            <th @click="sortBy('name')" class="sortable" :class="getSortClass('name')">
              Name
            </th>
            <th @click="sortBy('category')" class="sortable" :class="getSortClass('category')">
              Category
            </th>
            <th @click="sortBy('price')" class="sortable" :class="getSortClass('price')">
              Price
            </th>
            <th @click="sortBy('stock')" class="sortable" :class="getSortClass('stock')">
              Stock
            </th>
            <th @click="sortBy('createdDate')" class="sortable" :class="getSortClass('createdDate')">
              Created Date
            </th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="product in products" :key="product.id">
            <td>{{ product.id }}</td>
            <td>{{ product.name }}</td>
            <td>{{ product.category }}</td>
            <td>${{ product.price.toFixed(2) }}</td>
            <td>{{ product.stock }}</td>
            <td>{{ formatDate(product.createdDate) }}</td>
            <td>
              <button @click="deleteProduct(product.id)" class="btn btn-danger">
                Delete
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- No Data State -->
    <div v-else class="no-data">
      No products found. Try adjusting your filters or add a new product.
    </div>

    <!-- Add Product Modal -->
    <div v-if="showAddModal" class="form-modal" @click.self="closeModal">
      <div class="modal-content">
        <h2>Add New Product</h2>
        <form @submit.prevent="addProduct">
          <div class="form-group">
            <label>Name *</label>
            <input 
              type="text" 
              v-model="newProduct.name" 
              required
              placeholder="Product name"
            />
          </div>
          <div class="form-group">
            <label>Category *</label>
            <input 
              type="text" 
              v-model="newProduct.category" 
              required
              placeholder="Product category"
            />
          </div>
          <div class="form-group">
            <label>Price *</label>
            <input 
              type="number" 
              v-model.number="newProduct.price" 
              step="0.01"
              required
              placeholder="0.00"
            />
          </div>
          <div class="form-group">
            <label>Stock *</label>
            <input 
              type="number" 
              v-model.number="newProduct.stock" 
              required
              placeholder="0"
            />
          </div>
          <div class="form-actions">
            <button type="button" @click="closeModal" class="btn btn-secondary">
              Cancel
            </button>
            <button type="submit" class="btn btn-primary">
              Add Product
            </button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import { ref, computed, onMounted } from 'vue';
import { productService } from '../services/api';

export default {
  name: 'ProductList',
  setup() {
    const products = ref([]);
    const categories = ref([]);
    const loading = ref(false);
    const error = ref(null);
    const showAddModal = ref(false);
    
    const filters = ref({
      search: '',
      category: '',
      minPrice: null,
      maxPrice: null,
      sortBy: 'id',
      sortOrder: 'asc'
    });

    const newProduct = ref({
      name: '',
      category: '',
      price: 0,
      stock: 0
    });

    const totalValue = computed(() => {
      return products.value.reduce((sum, product) => sum + (product.price * product.stock), 0);
    });

    const fetchProducts = async () => {
      loading.value = true;
      error.value = null;
      try {
        const data = await productService.getProducts(filters.value);
        products.value = data;
      } catch (err) {
        error.value = 'Failed to load products. Make sure the backend is running on http://localhost:5000';
        console.error('Error fetching products:', err);
      } finally {
        loading.value = false;
      }
    };

    const fetchCategories = async () => {
      try {
        const data = await productService.getCategories();
        categories.value = data;
      } catch (err) {
        console.error('Error fetching categories:', err);
      }
    };

    const sortBy = (field) => {
      if (filters.value.sortBy === field) {
        filters.value.sortOrder = filters.value.sortOrder === 'asc' ? 'desc' : 'asc';
      } else {
        filters.value.sortBy = field;
        filters.value.sortOrder = 'asc';
      }
      fetchProducts();
    };

    const getSortClass = (field) => {
      if (filters.value.sortBy === field) {
        return filters.value.sortOrder === 'asc' ? 'sorted-asc' : 'sorted-desc';
      }
      return '';
    };

    const resetFilters = () => {
      filters.value = {
        search: '',
        category: '',
        minPrice: null,
        maxPrice: null,
        sortBy: 'id',
        sortOrder: 'asc'
      };
      fetchProducts();
    };

    const addProduct = async () => {
      try {
        await productService.createProduct(newProduct.value);
        closeModal();
        resetNewProduct();
        await fetchProducts();
        await fetchCategories();
      } catch (err) {
        error.value = 'Failed to add product';
        console.error('Error adding product:', err);
      }
    };

    const deleteProduct = async (id) => {
      if (!confirm('Are you sure you want to delete this product?')) {
        return;
      }
      try {
        await productService.deleteProduct(id);
        await fetchProducts();
        await fetchCategories();
      } catch (err) {
        error.value = 'Failed to delete product';
        console.error('Error deleting product:', err);
      }
    };

    const closeModal = () => {
      showAddModal.value = false;
      resetNewProduct();
    };

    const resetNewProduct = () => {
      newProduct.value = {
        name: '',
        category: '',
        price: 0,
        stock: 0
      };
    };

    const formatDate = (dateString) => {
      const date = new Date(dateString);
      return date.toLocaleDateString('en-US', {
        year: 'numeric',
        month: 'short',
        day: 'numeric'
      });
    };

    onMounted(() => {
      fetchProducts();
      fetchCategories();
    });

    return {
      products,
      categories,
      loading,
      error,
      filters,
      showAddModal,
      newProduct,
      totalValue,
      fetchProducts,
      sortBy,
      getSortClass,
      resetFilters,
      addProduct,
      deleteProduct,
      closeModal,
      formatDate
    };
  }
};
</script>
