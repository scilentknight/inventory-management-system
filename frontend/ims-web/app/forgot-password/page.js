"use client";

import { useState } from "react";
import Link from "next/link";
import { Mail, ArrowLeft } from "lucide-react";
import AuthLayout from "@/components/AuthLayout";

export default function ForgotPasswordPage() {
  const [email, setEmail] = useState("");
  const [successMessage, setSuccessMessage] = useState("");

  async function handleSubmit(e) {
    e.preventDefault();
    // TODO: call your auth API route, e.g. POST /api/auth/forgot-password
    setSuccessMessage("If an account exists for that email, a reset link is on its way.");
  }

  return (
    <AuthLayout title="Forgot Password">
      {successMessage && (
        <div className="mb-5 flex items-start justify-between gap-3 rounded-lg bg-green-50 border border-green-200 px-4 py-3 text-sm text-green-700">
          <span>{successMessage}</span>
          <button
            type="button"
            onClick={() => setSuccessMessage("")}
            className="text-green-500 hover:text-green-700"
            aria-label="Dismiss"
          >
            ×
          </button>
        </div>
      )}

      <form onSubmit={handleSubmit} className="space-y-5">
        <div>
          <label htmlFor="email" className="block text-sm font-semibold text-slate-700 mb-1.5">
            Email Address
          </label>
          <div className="flex items-stretch rounded-xl border border-slate-200 bg-white overflow-hidden focus-within:ring-2 focus-within:ring-[var(--c-primary)]">
            <span className="flex items-center px-3 text-slate-400 bg-slate-50 border-r border-slate-200">
              <Mail className="w-4 h-4" />
            </span>
            <input
              id="email"
              name="email"
              type="email"
              required
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              placeholder="name@example.com"
              className="flex-1 px-4 py-3 text-slate-900 placeholder:text-slate-400 focus:outline-none"
            />
          </div>
        </div>

        <button
          type="submit"
          className="w-full py-3 rounded-xl font-semibold text-white btn-primary-solid"
        >
          Send Reset Link
        </button>

        <div className="text-center">
          <Link
            href="/login"
            className="inline-flex items-center gap-1.5 text-sm font-medium text-primary-token hover:underline"
          >
            <ArrowLeft className="w-4 h-4" /> Back to Login
          </Link>
        </div>
      </form>
    </AuthLayout>
  );
}
