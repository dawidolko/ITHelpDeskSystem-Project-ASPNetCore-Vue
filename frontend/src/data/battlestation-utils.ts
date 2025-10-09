import { Component } from "./component-types";
import { battlestationData } from "./battlestation-data";

export function getComponentsByCategory(category: string): Component[] {
  if (category in battlestationData) {
    return battlestationData[category];
  }
  return [];
}

export function getComponent(category: string, id: number): Component | null {
  const components = getComponentsByCategory(category);
  return components.find((component) => component.id === id) || null;
}

export function getAllCategories(): string[] {
  return Object.keys(battlestationData);
}

export function getMainComponents(): Component[] {
  const mainComponents: Component[] = [];

  Object.values(battlestationData).forEach((categoryComponents) => {
    categoryComponents.forEach((component) => {
      if (component.type === "main") {
        mainComponents.push(component);
      }
    });
  });

  return mainComponents;
}

export function getComponentsCount(category: string): number {
  return getComponentsByCategory(category).length;
}

export function getFeaturedComponents(limit: number = 3): Component[] {
  const featured: Component[] = [];
  const categories = ["pc", "laptop", "monitors", "peripherals"];

  categories.forEach((category) => {
    const components = getComponentsByCategory(category);
    const mainComponent = components.find((c) => c.type === "main");
    if (mainComponent && featured.length < limit) {
      featured.push(mainComponent);
    }
  });

  return featured;
}

export function getRandomComponents(
  currentCategory: string,
  currentId: number,
  limit: number = 3
): Component[] {
  const allComponents: Component[] = [];

  Object.entries(battlestationData).forEach(([, components]) => {
    components.forEach((component) => {
      if (
        !(component.category === currentCategory && component.id === currentId)
      ) {
        allComponents.push(component);
      }
    });
  });

  allComponents.sort(() => Math.random() - 0.5);
  return allComponents.slice(0, limit);
}

export function getTotalComponents(): number {
  let total = 0;
  Object.values(battlestationData).forEach((components) => {
    total += components.length;
  });
  return total;
}

export function getCategoryDisplayName(category: string): string {
  const displayNames: { [key: string]: string } = {
    pc: "Gaming PC",
    laptop: "Laptop",
    monitors: "Monitors",
    peripherals: "Peripherals",
    audio: "Audio Equipment",
    workspace: "Workspace",
  };

  return displayNames[category] || category;
}
