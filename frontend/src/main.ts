import App from "./App.vue";
import Router from "./routes/routes";
import { createApp } from "vue";
import { createPinia } from "pinia";
import { createHead } from "@unhead/vue";
import { useAuthStore } from "./stores/authStore";
import "./style.css";

const pinia = createPinia();
const head = createHead();

const app = createApp(App);
app.use(pinia);
app.use(Router);
app.use(head);

const authStore = useAuthStore();
authStore.initAuth();

app.mount("#app");
