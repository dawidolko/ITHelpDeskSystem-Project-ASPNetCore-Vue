import { Component } from "./component-types";

export function getComponentPageMeta(component: Component) {
  return {
    title: component.name + " " + component.model + " | BattleStation",
    description: component.description,
    image: component.image,
  };
}
