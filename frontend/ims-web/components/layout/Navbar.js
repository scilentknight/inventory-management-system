"use client";

import Link from "next/link";
import { useState } from "react";
import { Boxes, Menu, X } from "lucide-react";

export default function Navbar() {
  const [open, setOpen] = useState(false);

  return (
    <nav className="landing-navbar fixed-top">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex items-center justify-between h-16">
          <Link
            href="/"
            className="navbar-brand flex items-center gap-2 text-xl"
          >
            <Boxes className="w-6 h-8 text-primary-token" />
            {/* CaféManager */}
            {/* Inventory Manager */}
            {/* StockFlow */}
            {/* IMS Pro */}
            InventoryPro
          </Link>

          {/* Desktop links */}
          <div className="hidden lg:flex items-center gap-8">
            <a
              href="/"
              className="text-sm font-medium"
              style={{ color: "var(--text-main)" }}
            >
              Home
            </a>
            <a
              href="#features"
              className="text-sm font-medium"
              style={{ color: "var(--text-main)" }}
            >
              Features
            </a>
            <a
              href="#about"
              className="text-sm font-medium"
              style={{ color: "var(--text-main)" }}
            >
              About
            </a>
            <a
              href="#contact"
              className="text-sm font-medium"
              style={{ color: "var(--text-main)" }}
            >
              Contact
            </a>
            <Link href="/login" className="btn-accent btn-sm">
              Login
            </Link>
            <Link href="/register" className="btn-accent btn-sm">
              Register
            </Link>
          </div>

          {/* Mobile toggle */}
          <button
            onClick={() => setOpen(!open)}
            className="lg:hidden p-2 rounded-md"
            style={{ color: "var(--text-main)" }}
            aria-label="Toggle navigation"
          >
            {open ? <X className="w-6 h-6" /> : <Menu className="w-6 h-6" />}
          </button>
        </div>

        {/* Mobile menu */}
        {open && (
          <div className="lg:hidden pb-6 flex flex-col gap-4">
            <a
              href="#features"
              onClick={() => setOpen(false)}
              className="text-sm font-medium"
              style={{ color: "var(--text-main)" }}
            >
              Features
            </a>
            <a
              href="#about"
              onClick={() => setOpen(false)}
              className="text-sm font-medium"
              style={{ color: "var(--text-main)" }}
            >
              About
            </a>
            <a
              href="#contact"
              onClick={() => setOpen(false)}
              className="text-sm font-medium"
              style={{ color: "var(--text-main)" }}
            >
              Contact
            </a>
            <Link href="/login" className="btn-accent btn-sm text-center">
              Login
            </Link>
            <Link href="/register" className="btn-accent btn-sm text-center">
              Register
            </Link>
          </div>
        )}
      </div>
    </nav>
  );
}
