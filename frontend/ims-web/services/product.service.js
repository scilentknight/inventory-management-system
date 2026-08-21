import { apiFetch } from "./api";

export async function getProducts() {
  const response = await apiFetch("/products");

  return response.data;
}
