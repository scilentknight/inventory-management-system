"use client";

import Link from "next/link";
import { usePathname } from "next/navigation";
import { useState } from "react";
import { Coffee, ChevronDown, LogOut } from "lucide-react";
import { useSidebar } from "@/contexts/SidebarContext";
import { sidebarMenu } from "./SidebarMunu";

function isChildActive(pathname, item) {
  return item.url && pathname.startsWith(item.url);
}

function isMenuActive(pathname, item) {
  if (item.url && pathname.startsWith(item.url)) return true;
  if (item.children)
    return item.children.some((c) => isChildActive(pathname, c));
  return false;
}

function SidebarItem({ item, collapsed }) {
  const pathname = usePathname();
  const hasChildren = item.children && item.children.length > 0;
  const active = isMenuActive(pathname, item);
  const [open, setOpen] = useState(
    hasChildren && item.children.some((c) => isChildActive(pathname, c)),
  );
  const Icon = item.icon;

  const linkClasses = (isActive) =>
    [
      "flex items-center rounded-lg px-3 py-2.5 text-[0.95rem] font-medium whitespace-nowrap transition-colors duration-150",
      isActive
        ? "bg-white/10 text-text-sidebar-active"
        : "text-text-sidebar hover:bg-white/10 hover:text-text-sidebar-active",
    ].join(" ");

  if (!hasChildren) {
    return (
      <li className="px-3 mb-1">
        <Link href={item.url} className={linkClasses(active)}>
          <Icon size={20} className="w-8 shrink-0" />
          <span className={collapsed ? "hidden" : "flex-1"}>{item.title}</span>
        </Link>
      </li>
    );
  }

  return (
    <li className="px-3 mb-1 relative group">
      <button
        type="button"
        onClick={() => setOpen((v) => !v)}
        className={linkClasses(active) + " w-full text-left"}
      >
        <Icon size={20} className="w-8 shrink-0" />
        <span className={collapsed ? "hidden" : "flex-1"}>{item.title}</span>
        {!collapsed && (
          <ChevronDown
            size={14}
            className={`transition-transform duration-150 ${open ? "rotate-180" : ""}`}
          />
        )}
      </button>

      {/* Expanded (non-collapsed): accordion submenu */}
      {!collapsed && (
        <ul className={`pl-8 pr-0 py-1 ${open ? "block" : "hidden"}`}>
          {item.children.map((child) => (
            <li key={child.url}>
              <Link
                href={child.url}
                className={[
                  "flex items-center gap-2 rounded-md px-3 py-2 text-sm transition-colors duration-150",
                  isChildActive(pathname, child)
                    ? "text-text-sidebar-active"
                    : "text-text-sidebar hover:text-text-sidebar-active",
                ].join(" ")}
              >
                <child.icon size={16} className="w-6 shrink-0" />
                {child.title}
              </Link>
            </li>
          ))}
        </ul>
      )}

      {/* Collapsed (desktop icon rail): flyout submenu on hover */}
      {collapsed && (
        <ul className="hidden group-hover:block absolute left-[70px] top-0 w-50 bg-bg-sidebar rounded-r-lg shadow-flyout p-2 z-flyout">
          {item.children.map((child) => (
            <li key={child.url}>
              <Link
                href={child.url}
                className="flex items-center gap-2 rounded-md px-3 py-2 text-sm text-text-sidebar hover:text-text-sidebar-active"
              >
                <child.icon size={16} className="w-6 shrink-0" />
                {child.title}
              </Link>
            </li>
          ))}
        </ul>
      )}
    </li>
  );
}

export default function AdminSidebar() {
  const { collapsed, mobileOpen, closeMobile } = useSidebar();

  return (
    <>
      <aside
        className={[
          "fixed top-0 left-0 h-screen bg-bg-sidebar text-text-sidebar z-sidebar flex flex-col shadow-sidebar",
          "transition-[width,transform] duration-300",
          collapsed ? "lg:w-sidebar-collapsed" : "lg:w-sidebar",
          "w-sidebar", // mobile always full width when open
          mobileOpen ? "translate-x-0" : "-translate-x-full lg:translate-x-0",
        ].join(" ")}
      >
        <div className="h-header flex items-center px-6 border-b border-white/5 whitespace-nowrap overflow-hidden">
          <Coffee size={24} className="text-primary mr-3 shrink-0" />
          <span
            className={`text-xl font-bold text-white ${collapsed ? "hidden" : ""}`}
          >
            InventoryPro
          </span>
        </div>

        <nav className="flex-1 overflow-y-auto py-4">
          <ul className="list-none">
            {sidebarMenu.map((item) => (
              <SidebarItem key={item.title} item={item} collapsed={collapsed} />
            ))}
          </ul>
        </nav>

        <div className="mt-auto pt-4 border-t border-white/5 px-3 pb-4">
          <button
            type="button"
            onClick={() => {
              /* wire up to your logout action */
            }}
            className="flex items-center w-full rounded-lg px-3 py-2.5 text-[0.95rem] font-medium text-text-sidebar hover:text-danger hover:bg-danger/10 transition-colors duration-150"
          >
            <LogOut size={20} className="w-8 shrink-0" />
            <span className={collapsed ? "hidden" : ""}>Logout</span>
          </button>
        </div>
      </aside>

      {/* Mobile overlay */}
      <div
        onClick={closeMobile}
        className={[
          "fixed inset-0 bg-black/50 z-overlay transition-opacity duration-300 lg:hidden",
          mobileOpen
            ? "opacity-100 pointer-events-auto"
            : "opacity-0 pointer-events-none",
        ].join(" ")}
      />
    </>
  );
}
