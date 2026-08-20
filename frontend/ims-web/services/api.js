const API_URL = process.env.NEXT_PUBLIC_API_BASE_URL;

export async function apiFetch(endpoint, options = {}) {
  const token = localStorage.getItem("token");

  console.log("API URL:", API_URL);
  console.log("TOKEN:", token);

  const headers = {
    "Content-Type": "application/json",
    ...options.headers,
  };

  if (token) {
    headers.Authorization = `Bearer ${token}`;
  }

  console.log("REQUEST HEADERS:", headers);

  const response = await fetch(`${API_URL}${endpoint}`, {
    ...options,
    headers,
  });

  console.log("STATUS:", response.status);

  if (!response.ok) {
    const errorText = await response.text();

    console.error("API ERROR:", errorText);

    throw new Error(`API request failed: ${response.status}`);
  }

  return response.json();
}
