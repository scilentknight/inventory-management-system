// "use client";

// import Link from "next/link";
// import { usePathname } from "next/navigation";
// import { useState } from "react";
// import {
//   LayoutDashboard,
//   Users,
//   User,
//   Shield,
//   BookOpen,
//   Grid2x2,
//   Coffee,
//   LayoutPanelTop,
//   Receipt,
//   Flame,
//   Wallet,
//   CalendarCheck,
//   UserRound,
//   Package,
//   Truck,
//   BadgeCheck,
//   Clock3,
//   WalletCards,
//   TrendingUp,
//   FileBarChart,
//   BarChart3,
//   Bell,
//   MessageSquare,
//   Ticket,
//   Settings,
//   ClipboardList,
//   ChevronDown,
//   LogOut,
// } from "lucide-react";

// export default function Sidebar({ onLogout }) {
//   const pathname = usePathname();

//   const menuItems = [
//     {
//       title: "Dashboard",
//       icon: LayoutDashboard,
//       url: "/admin/dashboard",
//     },
//     {
//       title: "User Management",
//       icon: Users,
//       children: [
//         {
//           title: "Users",
//           icon: User,
//           url: "/admin/users",
//         },
//         {
//           title: "Roles",
//           icon: Shield,
//           url: "/admin/roles",
//         },
//       ],
//     },
//     {
//       title: "Menu Management",
//       icon: BookOpen,
//       children: [
//         {
//           title: "Categories",
//           icon: Grid2x2,
//           url: "/admin/categories",
//         },
//         {
//           title: "Products",
//           icon: Coffee,
//           url: "/admin/products",
//         },
//       ],
//     },
//     {
//       title: "Table Management",
//       icon: LayoutPanelTop,
//       url: "/admin/tables",
//     },
//     {
//       title: "Orders",
//       icon: Receipt,
//       url: "/admin/orders",
//     },
//     {
//       title: "Kitchen",
//       icon: Flame,
//       url: "/admin/kitchen",
//     },
//     {
//       title: "POS",
//       icon: Wallet,
//       url: "/admin/pos",
//     },
//     {
//       title: "Reservations",
//       icon: CalendarCheck,
//       url: "/admin/reservations",
//     },
//     {
//       title: "Customers",
//       icon: UserRound,
//       url: "/admin/customers",
//     },
//     {
//       title: "Inventory",
//       icon: Package,
//       url: "/admin/inventory",
//     },
//     {
//       title: "Suppliers",
//       icon: Truck,
//       url: "/admin/suppliers",
//     },
//     {
//       title: "Employees",
//       icon: BadgeCheck,
//       url: "/admin/employees",
//     },
//     {
//       title: "Attendance",
//       icon: Clock3,
//       url: "/admin/attendance",
//     },
//     {
//       title: "Expenses",
//       icon: WalletCards,
//       url: "/admin/expenses",
//     },
//     {
//       title: "Sales",
//       icon: TrendingUp,
//       url: "/admin/sales",
//     },
//     {
//       title: "Reports",
//       icon: FileBarChart,
//       url: "/admin/reports",
//     },
//     {
//       title: "Analytics",
//       icon: BarChart3,
//       url: "/admin/analytics",
//     },
//     {
//       title: "Notifications",
//       icon: Bell,
//       url: "/admin/notifications",
//     },
//     {
//       title: "Feedback",
//       icon: MessageSquare,
//       url: "/admin/feedback",
//     },
//     {
//       title: "Coupons",
//       icon: Ticket,
//       url: "/admin/coupons",
//     },
//     {
//       title: "Settings",
//       icon: Settings,
//       url: "/admin/settings",
//     },
//     {
//       title: "Audit Logs",
//       icon: ClipboardList,
//       url: "/admin/audit-logs",
//     },
//   ];

//   const isChildActive = (item) => item.url && pathname.startsWith(item.url);

//   const isMenuActive = (item) => {
//     if (item.url && pathname.startsWith(item.url)) return true;

//     if (item.children) return item.children.some(isChildActive);

//     return false;
//   };

//   const [openMenus, setOpenMenus] = useState(() => {
//     const state = {};

//     menuItems.forEach((item) => {
//       state[item.title] = isMenuActive(item);
//     });

//     return state;
//   });

//   const toggleMenu = (title) => {
//     setOpenMenus((prev) => ({
//       ...prev,
//       [title]: !prev[title],
//     }));
//   };

//   return (
//     <aside className="w-72 bg-slate-900 text-white min-h-screen">
//       <ul className="py-4">
//         {menuItems.map((item) => {
//           const hasChildren = item.children && item.children.length > 0;

//           const active = isMenuActive(item);

//           const expanded = openMenus[item.title];

//           return (
//             <li key={item.title}>
//               {hasChildren ? (
//                 <>
//                   <button
//                     onClick={() => toggleMenu(item.title)}
//                     className={`flex w-full items-center justify-between px-5 py-3 hover:bg-slate-800 ${
//                       active ? "bg-slate-800" : ""
//                     }`}
//                   >
//                     <div className="flex items-center gap-3">
//                       <item.icon size={18} />

//                       {item.title}
//                     </div>

//                     <ChevronDown
//                       size={16}
//                       className={`transition ${expanded ? "rotate-180" : ""}`}
//                     />
//                   </button>

//                   {expanded && (
//                     <ul>
//                       {item.children.map((child) => (
//                         <li key={child.title}>
//                           <Link
//                             href={child.url}
//                             className={`flex items-center gap-3 pl-12 pr-5 py-3 hover:bg-slate-800 ${
//                               isChildActive(child)
//                                 ? "bg-slate-800 text-blue-400"
//                                 : ""
//                             }`}
//                           >
//                             <child.icon size={16} />

//                             {child.title}
//                           </Link>
//                         </li>
//                       ))}
//                     </ul>
//                   )}
//                 </>
//               ) : (
//                 <Link
//                   href={item.url}
//                   className={`flex items-center gap-3 px-5 py-3 hover:bg-slate-800 ${
//                     active ? "bg-slate-800 text-blue-400" : ""
//                   }`}
//                 >
//                   <item.icon size={18} />

//                   {item.title}
//                 </Link>
//               )}
//             </li>
//           );
//         })}

//         {/* Logout */}

//         <li className="mt-6">
//           <button
//             onClick={onLogout}
//             className="flex w-full items-center gap-3 px-5 py-3 text-red-400 hover:bg-red-500/10"
//           >
//             <LogOut size={18} />
//             Logout
//           </button>
//         </li>
//       </ul>
//     </aside>
//   );
// }

"use client";

import Link from "next/link";
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

export default function AdminSidebar() {
  return (
    <aside className="fixed left-0 top-0 h-screen w-64 bg-gray-900 text-white">
      <div className="flex h-16 items-center px-6">
        <h1 className="text-xl font-bold">InventoryPro</h1>
      </div>

      <nav className="px-4 py-4">
        <Link
          href="/dashboard"
          className="mb-2 flex items-center gap-3 rounded-lg px-4 py-3 hover:bg-gray-800"
        >
          <LayoutDashboard size={20} />
          Dashboard
        </Link>

        <Link
          href="/categories"
          className="mb-2 flex items-center gap-3 rounded-lg px-4 py-3 hover:bg-gray-800"
        >
          <Tags size={20} />
          Categories
        </Link>

        <Link
          href="/products"
          className="mb-2 flex items-center gap-3 rounded-lg px-4 py-3 hover:bg-gray-800"
        >
          <Package size={20} />
          Products
        </Link>

        <Link
          href="/suppliers"
          className="mb-2 flex items-center gap-3 rounded-lg px-4 py-3 hover:bg-gray-800"
        >
          <Truck size={20} />
          Suppliers
        </Link>

        <Link
          href="/customers"
          className="mb-2 flex items-center gap-3 rounded-lg px-4 py-3 hover:bg-gray-800"
        >
          <Users size={20} />
          Customers
        </Link>

        <Link
          href="/purchases"
          className="mb-2 flex items-center gap-3 rounded-lg px-4 py-3 hover:bg-gray-800"
        >
          <ShoppingCart size={20} />
          Purchases
        </Link>

        <Link
          href="/sales"
          className="mb-2 flex items-center gap-3 rounded-lg px-4 py-3 hover:bg-gray-800"
        >
          <Receipt size={20} />
          Sales
        </Link>

        <Link
          href="/inventory"
          className="mb-2 flex items-center gap-3 rounded-lg px-4 py-3 hover:bg-gray-800"
        >
          <Warehouse size={20} />
          Inventory
        </Link>

        <Link
          href="/reports"
          className="mb-2 flex items-center gap-3 rounded-lg px-4 py-3 hover:bg-gray-800"
        >
          <BarChart3 size={20} />
          Reports
        </Link>
      </nav>
    </aside>
  );
}
