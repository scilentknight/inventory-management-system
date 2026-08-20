"use client";

import { useEffect, useState } from "react";
import { getCategories } from "@/services/category.service";

export default function CategoriesPage() {
  const [categories, setCategories] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    async function loadCategories() {
      try {
        setLoading(true);

        const data = await getCategories();

        console.log("Categories data:", data);
        console.log("Is array?", Array.isArray(data));

        setCategories(data);
      } catch (error) {
        console.error(error);
        setError("Failed to load categories.");
      } finally {
        setLoading(false);
      }
    }

    loadCategories();
  }, []);

  if (loading) {
    return <div className="p-6">Loading categories...</div>;
  }

  if (error) {
    return <div className="p-6 text-red-500">{error}</div>;
  }

  return (
    <div className="p-6">
      <div className="mb-6">
        <h1 className="text-2xl font-bold">Categories</h1>
        <p className="text-gray-500">Manage your product categories</p>
      </div>

      <div className="overflow-hidden rounded-lg border bg-white">
        <table className="w-full">
          <thead className="bg-gray-100">
            <tr>
              <th className="px-6 py-3 text-left text-sm font-semibold">ID</th>

              <th className="px-6 py-3 text-left text-sm font-semibold">
                Category Code
              </th>

              <th className="px-6 py-3 text-left text-sm font-semibold">
                Name
              </th>

              <th className="px-6 py-3 text-left text-sm font-semibold">
                Slug
              </th>
            </tr>
          </thead>

          <tbody>
            {categories.length === 0 ? (
              <tr>
                <td colSpan="4" className="px-6 py-8 text-center text-gray-500">
                  No categories found.
                </td>
              </tr>
            ) : (
              categories.map((category) => (
                <tr key={category.id} className="border-t hover:bg-gray-50">
                  <td className="px-6 py-4">{category.id}</td>

                  <td className="px-6 py-4">{category.categoryCode}</td>

                  <td className="px-6 py-4">{category.name}</td>

                  <td className="px-6 py-4">{category.slug}</td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}
