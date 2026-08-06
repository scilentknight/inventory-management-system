import Link from "next/link";
import { Boxes, Mail, Phone } from "lucide-react";

export default function Footer() {
  return (
    <footer className="landing-footer">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="grid grid-cols-1 md:grid-cols-3 gap-10">
          <div>
            <h5 className="flex items-center gap-2 text-lg">
              <Boxes
                className="w-5 h-5"
                style={{ color: "var(--c-primary)" }}
              />
              InventoryPro
            </h5>
            <p className="text-sm" style={{ color: "#94a3b8" }}>
              Smart inventory management solution for efficient stock control
              and business growth.
            </p>
          </div>

          <div>
            <h6 className="text-sm uppercase tracking-wide">Quick Links</h6>
            <ul className="footer-links">
              <li>
                <a href="#features">Features</a>
              </li>
              <li>
                <a href="#about">About</a>
              </li>
              <li>
                <Link href="/login">Login</Link>
              </li>
              <li>
                <Link href="/register">Register</Link>
              </li>
            </ul>
          </div>

          <div>
            <h6 className="text-sm uppercase tracking-wide">Contact</h6>
            <ul className="footer-links">
              <li className="flex items-center gap-2">
                <Mail className="w-4 h-4" /> support@inventorymanager.com
              </li>
              <li className="flex items-center gap-2">
                <Phone className="w-4 h-4" /> +977-9861252006
              </li>
            </ul>
          </div>
        </div>

        <hr
          style={{ borderColor: "rgba(255,255,255,0.1)" }}
          className="my-10"
        />

        <p className="text-center text-sm" style={{ color: "#64748b" }}>
          &copy; {new Date().getFullYear()} InventoryPro. All rights reserved.
        </p>
      </div>
    </footer>
  );
}
