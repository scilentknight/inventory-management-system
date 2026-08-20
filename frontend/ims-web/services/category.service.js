// import { apiFetch } from "./api";

// export async function getCategories() {
//   return apiFetch("/categories");
// }

import { apiFetch } from "./api";

export async function getCategories() {
  const response = await apiFetch("/categories");

  return response.data;
}
