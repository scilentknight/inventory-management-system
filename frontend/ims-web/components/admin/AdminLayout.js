"use client";

import { SidebarProvider, useSidebar } from "@/contexts/SidebarContext";
import AdminSidebar from "./AdminSidebar";
import AdminHeader from "./AdminHeader";
import AdminFooter from "./AdminFooter";

function AdminMain({ children }) {
  const { collapsed } = useSidebar();

  return (
    <div
      className={[
        "flex min-h-screen flex-col transition-[margin-left] duration-300",
        collapsed ? "lg:ml-sidebar-collapsed" : "lg:ml-sidebar",
      ].join(" ")}
    >
      <AdminHeader />
      <main className="flex-1 p-6 bg-bg-main">{children}</main>
      <AdminFooter />
    </div>
  );
}

export default function AdminLayout({ children }) {
  return (
    <SidebarProvider>
      <div className="min-h-screen bg-bg-main">
        <AdminSidebar />
        <AdminMain>{children}</AdminMain>
      </div>
    </SidebarProvider>
  );
}
