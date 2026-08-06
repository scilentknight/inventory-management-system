import Link from "next/link";
import { PackageCheck } from "lucide-react";

export default function CTA() {
  return (
    <section id="contact" className="cta-section">
      <div className="max-w-3xl mx-auto px-4 sm:px-6 lg:px-8 text-center">
        <h2 className="cta-title">
          Ready to Transform Your Inventory Management?
        </h2>

        <p className="cta-text">
          Join businesses already using InventoryManager to streamline stock
          management, track products, and improve operations.
        </p>

        <Link
          href="/register"
          className="btn-hero-primary inline-flex items-center"
        >
          <PackageCheck className="w-5 h-5 mr-2" />
          Get Started
        </Link>
      </div>
    </section>
  );
}
