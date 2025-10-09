import { RouteLocationNormalized, NavigationGuardNext } from "vue-router";
import { useSeoMeta } from "@unhead/vue";
import { getComponent } from "../data/battlestation-utils";
import { meta } from "../data/component-types";
import { categoryPageMeta } from "../data/meta";
import { getComponentPageMeta } from "../data/meta-utils";

function parseRouteId(id: string | string[]): number {
  if (Array.isArray(id)) return parseInt(id[0]);
  return parseInt(id);
}

export function handleRouteMeta(metaFunc: () => meta): void {
  const metaData = metaFunc();
  useSeoMeta({
    title: metaData.title,
    description: metaData.description,
    ogTitle: metaData.title,
    ogDescription: metaData.description,
    ogImage: metaData.image,
  });
}

export function categoryRoute(category: string) {
  return {
    path: `/${category}`,
    component: () => import("../pages/Category/category-page.vue"),
    props: { category: category },
    beforeEnter: () => {
      const meta = categoryPageMeta[category];
      if (meta) {
        useSeoMeta({
          title: meta.title,
          description: meta.description,
          ogTitle: meta.title,
          ogDescription: meta.description,
          ogImage: meta.image,
        });
      }
    },
  };
}

export function componentRoute(category: string) {
  return {
    path: `/${category}/:id`,
    name: category,
    component: () => import("../pages/Product/product-page.vue"),
    props: (route: any) => ({
      category: category,
      componentId: parseInt(route.params.id),
    }),
    beforeEnter: (
      to: RouteLocationNormalized,
      _: RouteLocationNormalized,
      next: NavigationGuardNext
    ) => {
      const component = getComponent(category, parseRouteId(to.params.id));
      if (!component) {
        next("/404");
      } else {
        const meta = getComponentPageMeta(component);
        useSeoMeta({
          title: meta.title,
          description: meta.description,
          ogTitle: meta.title,
          ogDescription: meta.description,
          ogImage: meta.image,
        });
        next();
      }
    },
  };
}
