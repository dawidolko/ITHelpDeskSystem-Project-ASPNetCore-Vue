export interface Component {
  id: number;
  category: string;
  name: string;
  model: string;
  specs: string[];
  description: string;
  image: string;
  detailImages?: string[];
  type: "main" | "secondary";
}

export interface BattlestationData {
  [key: string]: Component[];
}

export interface meta {
  title: string;
  description: string;
  image?: string;
}

export type metaContainer = {
  [key: string]: meta;
};
