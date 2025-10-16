import DashboardPage from "../pages/Dashboard/dashboard-page.vue";
import { createRouter, createWebHistory } from "vue-router";
import { useSeoMeta } from "@unhead/vue";

const requireAuth = (_to: any, _from: any, next: any) => {
  const token = localStorage.getItem("token");
  if (!token) {
    next("/login");
  } else {
    next();
  }
};

const routes = [
  {
    path: "/login",
    name: "Login",
    component: () => import("../pages/Login/LoginPage.vue"),
    beforeEnter: () => {
      useSeoMeta({
        title: "Login | IT Help Desk",
        description: "Login to IT Help Desk System",
        ogTitle: "Login | IT Help Desk",
      });
    },
  },
  {
    path: "/register",
    name: "Register",
    component: () => import("../pages/Register/RegisterPage.vue"),
    beforeEnter: () => {
      useSeoMeta({
        title: "Register | IT Help Desk",
        description: "Register to IT Help Desk System",
        ogTitle: "Register | IT Help Desk",
      });
    },
  },
  {
    path: "/",
    name: "Dashboard",
    component: DashboardPage,
    beforeEnter: (to: any, from: any, next: any) => {
      requireAuth(to, from, next);
      useSeoMeta({
        title: "IT Help Desk | Dashboard",
        description:
          "IT Help Desk System - Manage and track IT support tickets efficiently with SFWP support.",
        ogTitle: "IT Help Desk | Dashboard",
        ogDescription:
          "Professional ticketing system for managing IT support requests.",
      });
    },
  },
  {
    path: "/dashboard",
    redirect: "/",
  },
  {
    path: "/tickets",
    name: "Tickets",
    component: () => import("../pages/Tickets/tickets-page.vue"),
    beforeEnter: (to: any, from: any, next: any) => {
      requireAuth(to, from, next);
      useSeoMeta({
        title: "Tickets | IT Help Desk",
        description:
          "Browse all IT support tickets with advanced sorting, filtering, searching, and pagination.",
        ogTitle: "Tickets | IT Help Desk",
        ogDescription:
          "Browse all IT support tickets with advanced SFWP features.",
      });
    },
  },
  {
    path: "/tickets/:id",
    name: "TicketDetail",
    component: () => import("../pages/Tickets/ticket-detail-page.vue"),
    beforeEnter: (to: any, from: any, next: any) => {
      requireAuth(to, from, next);
      useSeoMeta({
        title: "Ticket Details | IT Help Desk",
        description:
          "View detailed information about a specific IT support ticket.",
        ogTitle: "Ticket Details | IT Help Desk",
      });
    },
  },
  {
    path: "/create-ticket",
    name: "CreateTicket",
    component: () => import("../pages/Tickets/create-ticket-page.vue"),
    beforeEnter: (to: any, from: any, next: any) => {
      requireAuth(to, from, next);
      useSeoMeta({
        title: "Create Ticket | IT Help Desk",
        description: "Submit a new IT support request or issue.",
        ogTitle: "Create Ticket | IT Help Desk",
      });
    },
  },
  {
    path: "/statistics",
    name: "Statistics",
    component: () => import("../pages/Statistics/statistics-page.vue"),
    beforeEnter: (to: any, from: any, next: any) => {
      requireAuth(to, from, next);
      useSeoMeta({
        title: "Statistics | IT Help Desk",
        description:
          "View comprehensive statistics and analytics for IT support tickets.",
        ogTitle: "Statistics | IT Help Desk",
      });
    },
  },
  {
    path: "/users",
    name: "Users",
    component: () => import("../pages/Users/UsersManagementPage.vue"),
    beforeEnter: (to: any, from: any, next: any) => {
      requireAuth(to, from, next);
      useSeoMeta({
        title: "Users Management | IT Help Desk",
        description: "Manage users and their roles - Admin only",
        ogTitle: "Users Management | IT Help Desk",
      });
    },
  },
  {
    path: "/404",
    component: () => import("../pages/404/404-page.vue"),
    beforeEnter: () => {
      useSeoMeta({
        title: "404 | Page not found",
        description: "Page not found.",
        ogTitle: "404 | Page not found",
      });
    },
  },
  {
    path: "/:pathMatch(.*)",
    component: () => import("../pages/404/404-page.vue"),
    beforeEnter: () => {
      useSeoMeta({
        title: "404 | Page not found",
        description: "Page not found.",
        ogTitle: "404 | Page not found",
      });
    },
  },
];

const Router = createRouter({
  history: createWebHistory(),
  routes,

  scrollBehavior(_1, _2, savedPosition) {
    if (savedPosition) {
      return savedPosition;
    } else {
      return { top: 0 };
    }
  },
});

export default Router;
