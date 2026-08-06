// services/api.ts page

// const API_BASE_URL = process.env.NEXT_PUBLIC_API_BASE_URL!;

// export async function apiFetch(
//   endpoint: string,
//   options?: RequestInit
// ) {
//   const response = await fetch(`${API_BASE_URL}${endpoint}`, {
//     ...options,
//     headers: {
//       "Content-Type": "application/json",
//       ...(options?.headers || {})
//     }
//   });

//   if (!response.ok) {
//     throw new Error("API request failed");
//   }

//   return response.json();
// }

const API_BASE_URL = process.env.NEXT_PUBLIC_API_BASE_URL;

export async function apiFetch(endpoint, options = {}) {
  const response = await fetch(`${API_BASE_URL}${endpoint}`, {
    ...options,
    headers: {
      "Content-Type": "application/json",
      ...(options.headers || {}),
    },
  });

  if (!response.ok) {
    throw new Error("API request failed");
  }

  return response.json();
}
