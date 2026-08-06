"use client";

import Link from "next/link";
import { ShieldOff, ArrowLeft, Gauge } from "lucide-react";

export default function AccessDeniedPage() {
  return (
    <div className="min-h-screen flex items-center justify-center bg-slate-50 px-4 py-12">
      <div className="w-full max-w-md text-center bg-white rounded-2xl shadow-xl shadow-slate-200/60 border border-slate-200 p-10">
        <ShieldOff className="w-16 h-16 text-red-500 mx-auto mb-5" />
        <h2 className="text-2xl font-bold text-slate-900 mb-2">
          Access Denied
        </h2>
        <p className="text-slate-600 mb-8">
          You do not have permission to access this resource or perform this
          operation.
        </p>
        <div className="flex flex-wrap items-center justify-center gap-3">
          <button
            onClick={() => window.history.back()}
            className="inline-flex items-center gap-2 px-5 py-2.5 rounded-xl font-semibold text-slate-700 border border-slate-200 hover:bg-slate-50 transition-colors"
          >
            <ArrowLeft className="w-4 h-4" /> Go Back
          </button>
          <Link
            href="/dashboard"
            className="inline-flex items-center gap-2 px-5 py-2.5 rounded-xl font-semibold text-white btn-primary-solid"
          >
            <Gauge className="w-4 h-4" /> Go to Dashboard
          </Link>
        </div>
      </div>
    </div>
  );
}
