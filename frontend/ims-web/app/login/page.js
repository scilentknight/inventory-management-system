// "use client";

// import { useState } from "react";
// import Link from "next/link";
// import AuthLayout from "@/components/AuthLayout";

// export default function LoginPage() {
//   const [form, setForm] = useState({
//     usernameOrEmail: "",
//     password: "",
//     rememberMe: false,
//   });
//   const [errors, setErrors] = useState({});
//   const [successMessage, setSuccessMessage] = useState("");
//   // Populate from e.g. a redirect query param on the client if desired:
//   // useEffect(() => setSuccessMessage(searchParams.get("registered") ? "Account created, please log in." : ""), []);

//   function handleChange(e) {
//     const { name, value, type, checked } = e.target;
//     setForm((f) => ({ ...f, [name]: type === "checkbox" ? checked : value }));
//   }

//   async function handleSubmit(e) {
//     e.preventDefault();
//     setErrors({});

//     const newErrors = {};
//     if (!form.usernameOrEmail) newErrors.usernameOrEmail = "Username or email is required.";
//     if (!form.password) newErrors.password = "Password is required.";
//     if (Object.keys(newErrors).length) {
//       setErrors(newErrors);
//       return;
//     }

//     // TODO: call your auth API route, e.g. POST /api/auth/login
//   }

//   return (
//     <AuthLayout title="Log In">
//       {successMessage && (
//         <div className="mb-5 flex items-start justify-between gap-3 rounded-lg bg-mint/10 border border-mint/30 px-4 py-3 text-sm text-mint">
//           <span>{successMessage}</span>
//           <button
//             type="button"
//             onClick={() => setSuccessMessage("")}
//             className="text-mint/70 hover:text-mint"
//             aria-label="Dismiss"
//           >
//             ×
//           </button>
//         </div>
//       )}

//       <form onSubmit={handleSubmit} className="space-y-5">
//         <div>
//           <label htmlFor="usernameOrEmail" className="block text-sm font-medium text-espresso-700 mb-1.5">
//             Username or Email
//           </label>
//           <input
//             id="usernameOrEmail"
//             name="usernameOrEmail"
//             type="text"
//             value={form.usernameOrEmail}
//             onChange={handleChange}
//             placeholder="name@example.com"
//             className="w-full px-4 py-3 rounded-xl border border-espresso-100 bg-white text-espresso-800 placeholder:text-espresso-400 focus:outline-none focus:ring-2 focus:ring-caramel-500 focus:border-transparent transition-shadow"
//           />
//           {errors.usernameOrEmail && (
//             <p className="text-xs text-rose-500 mt-1.5">{errors.usernameOrEmail}</p>
//           )}
//         </div>

//         <div>
//           <label htmlFor="password" className="block text-sm font-medium text-espresso-700 mb-1.5">
//             Password
//           </label>
//           <input
//             id="password"
//             name="password"
//             type="password"
//             value={form.password}
//             onChange={handleChange}
//             placeholder="Password"
//             className="w-full px-4 py-3 rounded-xl border border-espresso-100 bg-white text-espresso-800 placeholder:text-espresso-400 focus:outline-none focus:ring-2 focus:ring-caramel-500 focus:border-transparent transition-shadow"
//           />
//           {errors.password && (
//             <p className="text-xs text-rose-500 mt-1.5">{errors.password}</p>
//           )}
//         </div>

//         <div className="flex items-center justify-between">
//           <label className="flex items-center gap-2 text-sm text-espresso-600">
//             <input
//               type="checkbox"
//               name="rememberMe"
//               checked={form.rememberMe}
//               onChange={handleChange}
//               className="rounded border-espresso-200 text-caramel-500 focus:ring-caramel-500"
//             />
//             Remember me
//           </label>
//           <Link href="/forgot-password" className="text-sm font-medium text-caramel-600 hover:underline">
//             Forgot password?
//           </Link>
//         </div>

//         <button
//           type="submit"
//           className="w-full py-3.5 rounded-xl font-semibold text-white bg-gradient-hero shadow-soft hover:opacity-90 transition-opacity"
//         >
//           Sign In
//         </button>
//       </form>

//       <div className="text-center mt-6">
//         <p className="text-sm text-espresso-600">
//           Don&apos;t have an account?{" "}
//           <Link href="/register" className="font-semibold text-caramel-600 hover:underline">
//             Sign up
//           </Link>
//         </p>
//       </div>
//     </AuthLayout>
//   );
// }

"use client";

import { useState } from "react";
import Link from "next/link";
import AuthLayout from "@/components/AuthLayout";
import Navbar from "@/components/layout/Navbar";
import Footer from "@/components/layout/Footer";

export default function LoginPage() {
  const [form, setForm] = useState({
    usernameOrEmail: "",
    password: "",
    rememberMe: false,
  });
  const [errors, setErrors] = useState({});
  const [successMessage, setSuccessMessage] = useState("");
  // Populate from e.g. a redirect query param on the client if desired:
  // useEffect(() => setSuccessMessage(searchParams.get("registered") ? "Account created, please log in." : ""), []);

  function handleChange(e) {
    const { name, value, type, checked } = e.target;
    setForm((f) => ({ ...f, [name]: type === "checkbox" ? checked : value }));
  }

  async function handleSubmit(e) {
    e.preventDefault();
    setErrors({});

    const newErrors = {};
    if (!form.usernameOrEmail)
      newErrors.usernameOrEmail = "Username or email is required.";
    if (!form.password) newErrors.password = "Password is required.";
    if (Object.keys(newErrors).length) {
      setErrors(newErrors);
      return;
    }

    // TODO: call your auth API route, e.g. POST /api/auth/login
  }

  return (
    <>
      <Navbar />
      <AuthLayout title="Log In">
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
            <label
              htmlFor="usernameOrEmail"
              className="block text-sm font-medium text-slate-700 mb-1.5"
            >
              Username or Email
            </label>
            <input
              id="usernameOrEmail"
              name="usernameOrEmail"
              type="text"
              value={form.usernameOrEmail}
              onChange={handleChange}
              placeholder="name@example.com"
              className="w-full px-4 py-3 rounded-xl border border-slate-200 bg-white text-slate-900 placeholder:text-slate-400 focus:outline-none focus:ring-2 focus:ring-(--c-primary) focus:border-transparent transition-shadow"
            />
            {errors.usernameOrEmail && (
              <p className="text-xs text-red-500 mt-1.5">
                {errors.usernameOrEmail}
              </p>
            )}
          </div>

          <div>
            <label
              htmlFor="password"
              className="block text-sm font-medium text-slate-700 mb-1.5"
            >
              Password
            </label>
            <input
              id="password"
              name="password"
              type="password"
              value={form.password}
              onChange={handleChange}
              placeholder="Password"
              className="w-full px-4 py-3 rounded-xl border border-slate-200 bg-white text-slate-900 placeholder:text-slate-400 focus:outline-none focus:ring-2 focus:ring-(--c-primary) focus:border-transparent transition-shadow"
            />
            {errors.password && (
              <p className="text-xs text-red-500 mt-1.5">{errors.password}</p>
            )}
          </div>

          <div className="flex items-center justify-between">
            <label className="flex items-center gap-2 text-sm text-slate-600">
              <input
                type="checkbox"
                name="rememberMe"
                checked={form.rememberMe}
                onChange={handleChange}
                className="rounded border-slate-200 focus:ring-(--c-primary)"
                style={{ accentColor: "var(--c-primary)" }}
              />
              Remember me
            </label>
            <Link
              href="/forgot-password"
              className="text-sm font-medium text-primary-token hover:underline"
            >
              Forgot password?
            </Link>
          </div>

          <button
            type="submit"
            className="w-full py-3.5 rounded-xl font-semibold text-white btn-primary-solid"
          >
            Sign In
          </button>
        </form>

        <div className="text-center mt-6">
          <p className="text-sm text-slate-600">
            Don&apos;t have an account?{" "}
            <Link
              href="/register"
              className="font-semibold text-primary-token hover:underline"
            >
              Sign up
            </Link>
          </p>
        </div>
      </AuthLayout>
      <Footer />
    </>
  );
}
