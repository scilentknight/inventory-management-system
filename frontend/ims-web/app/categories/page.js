import { getCategories } from "@/services/category.service";

export default async function CategoriesPage() {
  const categories = await getCategories();

  return (
    <div>
      <h1>Categories</h1>

      {categories.map((category) => (
        <>
          <p key={category.id}></p>
          <p>{category.name}</p>
          <p>{category.description}</p>
        </>
      ))}
    </div>
  );
}
