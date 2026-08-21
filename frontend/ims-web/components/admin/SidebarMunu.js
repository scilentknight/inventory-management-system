import {
  LayoutDashboard,
  Package,
  Tags,
  Users,
  Truck,
  ShoppingCart,
  Receipt,
  Warehouse,
  BarChart3,
} from "lucide-react";

export const sidebarMenu = [
  { title: "Dashboard", url: "/dashboard", icon: LayoutDashboard },
  { title: "Categories", url: "/categories", icon: Tags },
  { title: "Products", url: "/products", icon: Package },
  { title: "Suppliers", url: "/suppliers", icon: Truck },
  { title: "Customers", url: "/customers", icon: Users },
  { title: "Purchases", url: "/purchases", icon: ShoppingCart },
  { title: "Sales", url: "/sales", icon: Receipt },
  { title: "Inventory", url: "/inventory", icon: Warehouse },
  { title: "Reports", url: "/reports", icon: BarChart3 },
];
