"use client";

import { useEffect, useState } from "react";
import { getProducts } from "@/services/product.service";

export default function ProductsPage() {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    async function loadProducts() {
      try {
        setLoading(true);

        const data = await getProducts();

        console.log("Products data:", data);
        console.log("Is array?", Array.isArray(data));

        setProducts(data);
      } catch (error) {
        console.error(error);
        setError("Failed to load products.");
      } finally {
        setLoading(false);
      }
    }

    loadProducts();
  }, []);

  if (loading) {
    return <div className="p-6">Loading products...</div>;
  }

  if (error) {
    return <div className="p-6 text-red-500">{error}</div>;
  }

  return (
    <div className="p-6">
      <div className="mb-6">
        <h1 className="text-2xl font-bold">Products</h1>
        <p className="text-gray-500">Manage your products</p>
      </div>

      <div className="overflow-hidden rounded-lg border bg-white">
        <table className="w-full">
          <thead className="bg-gray-100">
            <tr>
              <th className="px-6 py-3 text-left text-sm font-semibold">ID</th>

              <th className="px-6 py-3 text-left text-sm font-semibold">
                Product Code
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
            {products.length === 0 ? (
              <tr>
                <td colSpan="4" className="px-6 py-8 text-center text-gray-500">
                  No Products found.
                </td>
              </tr>
            ) : (
              products.map((product) => (
                <tr key={product.id} className="border-t hover:bg-gray-50">
                  <td className="px-6 py-4">{product.id}</td>

                  <td className="px-6 py-4">{product.sku}</td>

                  <td className="px-6 py-4">{product.name}</td>

                  <td className="px-6 py-4">{product.slug}</td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}
