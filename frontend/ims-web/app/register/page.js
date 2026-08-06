"use client";

import { useState } from "react";
import Link from "next/link";
import AuthLayout from "@/components/AuthLayout";

const initialForm = {
  firstName: "",
  lastName: "",
  username: "",
  email: "",
  phone: "",
  password: "",
  confirmPassword: "",
};

function Field({
  id,
  label,
  type = "text",
  value,
  onChange,
  error,
  placeholder,
}) {
  return (
    <div>
      <label
        htmlFor={id}
        className="block text-sm font-medium text-slate-700 mb-1.5"
      >
        {label}
      </label>
      <input
        id={id}
        name={id}
        type={type}
        value={value}
        onChange={onChange}
        placeholder={placeholder}
        className="w-full px-4 py-3 rounded-xl border border-slate-200 bg-white text-slate-900 placeholder:text-slate-400 focus:outline-none focus:ring-2 focus:ring-(--c-primary) focus:border-transparent transition-shadow"
      />
      {error && <p className="text-xs text-red-500 mt-1.5">{error}</p>}
    </div>
  );
}

export default function RegisterPage() {
  const [form, setForm] = useState(initialForm);
  const [errors, setErrors] = useState({});

  function handleChange(e) {
    const { name, value } = e.target;
    setForm((f) => ({ ...f, [name]: value }));
  }

  async function handleSubmit(e) {
    e.preventDefault();
    const newErrors = {};
    if (!form.firstName) newErrors.firstName = "First name is required.";
    if (!form.lastName) newErrors.lastName = "Last name is required.";
    if (!form.username) newErrors.username = "Username is required.";
    if (!form.email) newErrors.email = "Email is required.";
    if (!form.password) newErrors.password = "Password is required.";
    if (form.password !== form.confirmPassword) {
      newErrors.confirmPassword = "Passwords do not match.";
    }

    setErrors(newErrors);
    if (Object.keys(newErrors).length) return;

    // TODO: call your auth API route, e.g. POST /api/auth/register
  }

  return (
    <AuthLayout title="Register">
      <form onSubmit={handleSubmit} className="space-y-5">
        <div className="grid grid-cols-2 gap-4">
          <Field
            id="firstName"
            label="First Name"
            value={form.firstName}
            onChange={handleChange}
            error={errors.firstName}
            placeholder="First Name"
          />
          <Field
            id="lastName"
            label="Last Name"
            value={form.lastName}
            onChange={handleChange}
            error={errors.lastName}
            placeholder="Last Name"
          />
        </div>

        <Field
          id="username"
          label="Username"
          value={form.username}
          onChange={handleChange}
          error={errors.username}
          placeholder="Username"
        />

        <Field
          id="email"
          label="Email"
          type="email"
          value={form.email}
          onChange={handleChange}
          error={errors.email}
          placeholder="name@example.com"
        />

        <Field
          id="phone"
          label="Phone (Optional)"
          type="tel"
          value={form.phone}
          onChange={handleChange}
          placeholder="Phone (Optional)"
        />

        <div className="grid grid-cols-2 gap-4">
          <Field
            id="password"
            label="Password"
            type="password"
            value={form.password}
            onChange={handleChange}
            error={errors.password}
            placeholder="Password"
          />
          <Field
            id="confirmPassword"
            label="Confirm Password"
            type="password"
            value={form.confirmPassword}
            onChange={handleChange}
            error={errors.confirmPassword}
            placeholder="Confirm Password"
          />
        </div>

        <button
          type="submit"
          className="w-full py-3.5 rounded-xl font-semibold text-white btn-primary-solid"
        >
          Create Account
        </button>

        <p className="text-center text-xs text-slate-500">
          By registering, you agree to our{" "}
          <a href="#" className="underline hover:text-primary-token">
            Terms of Service
          </a>{" "}
          and{" "}
          <a href="#" className="underline hover:text-primary-token">
            Privacy Policy
          </a>
          .
        </p>
      </form>

      <div className="text-center mt-6 pt-5 border-t border-slate-200">
        <p className="text-sm text-slate-600">
          Already have an account?{" "}
          <Link
            href="/login"
            className="font-semibold text-primary-token hover:underline"
          >
            Sign in
          </Link>
        </p>
      </div>
    </AuthLayout>
  );
}
