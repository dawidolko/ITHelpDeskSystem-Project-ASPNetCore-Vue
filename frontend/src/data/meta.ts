import bannerImage from "/icons/battlestation-icon.png";
import keyboardsImage from "/products/workspace/station1.webp";
import keycapsImage from "/products/peripherals/keyboard1.jpg";
import deskmatsImage from "/products/monitors/screen1.webp";
import { meta, metaContainer } from "./meta-types.ts";

export const landingPageMeta: meta = {
  title: "WorkplayHub Mechanical Keyboard Store",
  description:
    "We sell custom mechanical keyboards, keycaps and deskmats for your gaming and office setup, right in the heart of Berlin.",
  image: bannerImage,
};

export const categoryPageMeta: metaContainer = {
  keyboards: {
    title: "All Mechanical Keyboards | WorkplayHub",
    description:
      "Explore our selection of high quality mechanical keyboards that will elevate your typing experience.",
    image: keyboardsImage,
  },
  keycaps: {
    title: "All Custom Keycaps | WorkplayHub",
    description:
      "Enjoy typing with style and upgrade your custom mechanical keyboard with our elegant keycaps",
    image: keycapsImage,
  },
  deskmats: {
    title: "All Deskmats | WorkplayHub",
    description:
      "Make yourself comfortable in your work or gaming setup with one of our WorkplayHub deskmats. Soft and stylish!",
    image: deskmatsImage,
  },
};

export const checkoutPageMeta: meta = {
  title: "Checkout",
  description: "Complete your purchase.",
  image: bannerImage,
};

export const fofPageMeta: meta = {
  title: "404: Page not found.",
  description: "404: Page not found.",
  image: bannerImage,
};
