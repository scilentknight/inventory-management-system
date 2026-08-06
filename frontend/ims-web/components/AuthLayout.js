import Link from "next/link";
import { Boxes } from "lucide-react";

export default function AuthLayout({ title, subtitle, children }) {
  return (
    <div className="bg-linear-to-br from-slate-50 via-white to-blue-50 h-screen overflow-hidden">
      <div className="max-w-2xl mx-auto h-full flex flex-col justify-center px-4 pt-24 pb-6">
        {/* Logo */}
        <Link
          href="/"
          className="flex items-center justify-center gap-2 mb-6 text-primary-token"
        >
          <Boxes className="w-8 h-8" />
          <span className="text-3xl font-bold">InventoryPro</span>
        </Link>

        {/* Card */}
        <div className="bg-white rounded-2xl shadow-xl shadow-slate-200/60 border border-slate-100 p-6 max-h-[calc(100vh-140px)] overflow-y-auto">
          <div className="text-center mb-6">
            <h1 className="text-2xl font-bold text-slate-900">{title}</h1>

            {subtitle && (
              <p className="text-sm text-slate-500 mt-2">{subtitle}</p>
            )}
          </div>

          {children}
        </div>
      </div>
    </div>
  );
}
