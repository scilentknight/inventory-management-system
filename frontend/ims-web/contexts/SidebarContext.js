"use client";

import { createContext, useContext, useState, useCallback } from "react";

const SidebarContext = createContext(null);

export function SidebarProvider({ children }) {
  const [collapsed, setCollapsed] = useState(false); // desktop: icon-only rail
  const [mobileOpen, setMobileOpen] = useState(false); // mobile: slide-in drawer

  // Mirrors sidebarToggle.js: on desktop it collapses/expands the rail,
  // on mobile (<992px) it slides the drawer open/closed instead.
  const toggleSidebar = useCallback(() => {
    if (typeof window !== "undefined" && window.innerWidth < 992) {
      setMobileOpen((prev) => !prev);
    } else {
      setCollapsed((prev) => !prev);
    }
  }, []);

  const closeMobile = useCallback(() => setMobileOpen(false), []);

  return (
    <SidebarContext.Provider
      value={{ collapsed, mobileOpen, toggleSidebar, closeMobile }}
    >
      {children}
    </SidebarContext.Provider>
  );
}

export function useSidebar() {
  const ctx = useContext(SidebarContext);
  if (!ctx) throw new Error("useSidebar must be used within SidebarProvider");
  return ctx;
}
