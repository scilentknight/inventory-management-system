// import { getCategories } from "@/services/category.service";

// export default async function CategoriesPage() {
//   const categories = await getCategories();

//   return (
//     <div>
//       <h1>Categories</h1>

//       {categories.map((category) => (
//         <>
//           <p key={category.id}></p>
//           <p>{category.name}</p>
//           <p>{category.description}</p>
//         </>
//       ))}
//     </div>
//   );
// }

"use client";

import { useEffect, useState } from "react";

export default function CategoriesPage() {
  const [categories, setCategories] = useState([]);

  useEffect(() => {
    fetch("http://localhost:5200/api/categories")
      .then((res) => res.json())
      .then((data) => setCategories(data));
  }, []);

  return (
    <div>
      <h1>Categories</h1>

      {categories.map((category) => (
        <div key={category.id}>
          <p>{category.name}</p>
          <p>{category.description}</p>
        </div>
      ))}
    </div>
  );
}
