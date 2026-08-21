"use client";

import { useEffect, useRef, useState } from "react";
import {
  Menu,
  Search,
  Calendar,
  Moon,
  Sun,
  Bell,
  ChevronDown,
  User,
  Key,
  Settings,
  LogOut,
} from "lucide-react";
import { useSidebar } from "@/contexts/SidebarContext";

function useClickOutside(ref, onOutside) {
  useEffect(() => {
    function handler(e) {
      if (ref.current && !ref.current.contains(e.target)) onOutside();
    }
    document.addEventListener("mousedown", handler);
    return () => document.removeEventListener("mousedown", handler);
  }, [ref, onOutside]);
}

export default function AdminHeader({ user, unreadNotifications = 0 }) {
  const { toggleSidebar } = useSidebar();
  const [dark, setDark] = useState(false);
  const [today, setToday] = useState("");
  const [notifOpen, setNotifOpen] = useState(false);
  const [userOpen, setUserOpen] = useState(false);

  const notifRef = useRef(null);
  const userRef = useRef(null);
  useClickOutside(notifRef, () => setNotifOpen(false));
  useClickOutside(userRef, () => setUserOpen(false));

  // Dark mode: persisted, same intent as darkmode.js toggling data-bs-theme
  useEffect(() => {
    const stored = localStorage.getItem("theme");
    const isDark = stored === "dark";
    setDark(isDark);
    document.documentElement.classList.toggle("dark", isDark);

    setToday(
      new Date().toLocaleDateString("en-US", {
        weekday: "long",
        year: "numeric",
        month: "long",
        day: "2-digit",
      }),
    );
  }, []);

  const toggleDark = () => {
    const next = !dark;
    setDark(next);
    document.documentElement.classList.toggle("dark", next);
    localStorage.setItem("theme", next ? "dark" : "light");
  };

  const fullName = user?.fullName ?? "Admin";
  const role = user?.role ?? "Administrator";
  const email = user?.email ?? "admin@example.com";

  return (
    <header className="sticky top-0 z-header flex h-header items-center justify-between border-b border-border-theme bg-bg-card px-6">
      <div className="flex items-center gap-4">
        <button
          type="button"
          onClick={toggleSidebar}
          className="p-2 text-text-main rounded hover:bg-bg-main transition-colors duration-150"
        >
          <Menu size={24} />
        </button>

        <div className="hidden md:flex relative w-62.5">
          <Search
            size={16}
            className="absolute left-3 top-1/2 -translate-y-1/2 text-text-muted"
          />
          <input
            type="text"
            placeholder="Search..."
            className="w-full rounded-full border-none bg-bg-main pl-9 pr-3 py-2 text-sm text-text-main placeholder:text-text-muted focus:bg-bg-card focus:ring-1 focus:ring-primary outline-none transition-colors duration-150"
          />
        </div>
      </div>

      <div className="flex items-center gap-4">
        <div className="hidden lg:flex items-center gap-1.5 text-sm text-text-muted">
          <Calendar size={16} />
          <span>{today}</span>
        </div>

        <button
          type="button"
          onClick={toggleDark}
          title="Toggle dark mode"
          className="p-2 rounded-full text-text-main hover:bg-bg-main transition-colors duration-150"
        >
          {dark ? <Sun size={20} /> : <Moon size={20} />}
        </button>

        {/* Notifications */}
        <div className="relative" ref={notifRef}>
          <button
            type="button"
            onClick={() => setNotifOpen((v) => !v)}
            title="Notifications"
            className="relative p-2 rounded-full text-text-main hover:bg-bg-main transition-colors duration-150"
          >
            <Bell size={20} />
            {unreadNotifications > 0 && (
              <span className="absolute top-0.5 right-0.5 rounded-full bg-danger text-white text-[0.65rem] font-bold leading-none px-1.5 py-1">
                {unreadNotifications}
              </span>
            )}
          </button>

          {notifOpen && (
            <div className="absolute right-0 mt-2 w-80 rounded-lg border border-border-theme bg-bg-card shadow-flyout py-2 z-flyout">
              <div className="flex items-center justify-between px-4 py-2">
                <h6 className="font-semibold text-text-main">Notifications</h6>
                <a
                  href="/notifications"
                  className="text-primary text-sm hover:underline"
                >
                  View All
                </a>
              </div>
              <hr className="border-border-theme" />
              <p className="text-center text-text-muted text-sm py-6">
                No new notifications
              </p>
            </div>
          )}
        </div>

        {/* User dropdown */}
        <div className="relative" ref={userRef}>
          <button
            type="button"
            onClick={() => setUserOpen((v) => !v)}
            className="flex items-center gap-3 p-1"
          >
            <div className="flex h-10 w-10 items-center justify-center rounded-full bg-primary text-white font-bold overflow-hidden">
              {user?.avatarUrl ? (
                // eslint-disable-next-line @next/next/no-img-element
                <img
                  src={user.avatarUrl}
                  alt="Avatar"
                  className="h-full w-full object-cover"
                />
              ) : (
                <span>{fullName[0]}</span>
              )}
            </div>
            <div className="hidden md:block text-left leading-tight">
              <span className="block text-sm font-semibold text-text-main">
                {fullName}
              </span>
              <span className="block text-xs text-text-muted">{role}</span>
            </div>
            <ChevronDown
              size={14}
              className="hidden md:inline text-text-muted"
            />
          </button>

          {userOpen && (
            <div className="absolute right-0 mt-2 w-64 rounded-lg border border-border-theme bg-bg-card shadow-flyout py-2 z-flyout">
              <div className="px-4 py-2">
                <strong className="block text-text-main">{fullName}</strong>
                <small className="text-text-muted">{email}</small>
              </div>
              <hr className="border-border-theme my-1" />
              <a
                href="/profile"
                className="flex items-center gap-2 px-4 py-2 text-sm text-text-main hover:bg-bg-main"
              >
                <User size={16} /> Profile
              </a>
              <a
                href="/profile/change-password"
                className="flex items-center gap-2 px-4 py-2 text-sm text-text-main hover:bg-bg-main"
              >
                <Key size={16} /> Change Password
              </a>
              <a
                href="/settings"
                className="flex items-center gap-2 px-4 py-2 text-sm text-text-main hover:bg-bg-main"
              >
                <Settings size={16} /> Settings
              </a>
              <hr className="border-border-theme my-1" />
              <button
                type="button"
                onClick={() => {
                  /* wire up to your logout action */
                }}
                className="flex items-center gap-2 w-full px-4 py-2 text-sm text-danger hover:bg-danger/10"
              >
                <LogOut size={16} /> Logout
              </button>
            </div>
          )}
        </div>
      </div>
    </header>
  );
}
