// "use client";

// import { useMemo, useState } from "react";
// import Link from "next/link";
// import {
//   Search,
//   CalendarDays,
//   MoonStar,
//   Bell,
//   ChevronDown,
//   User,
//   KeyRound,
//   Settings,
//   LogOut,
// } from "lucide-react";

// export default function Header() {
//   // Demo Data (Replace with API/User Session)
//   const user = {
//     fullName: "John Doe",
//     role: "Administrator",
//     email: "john@example.com",
//     avatarUrl: "",
//     unreadNotifications: 3,
//   };

//   const [showNotifications, setShowNotifications] = useState(false);
//   const [showProfile, setShowProfile] = useState(false);

//   const currentDate = useMemo(() => {
//     return new Date().toLocaleDateString("en-US", {
//       weekday: "long",
//       month: "long",
//       day: "2-digit",
//       year: "numeric",
//     });
//   }, []);

//   return (
//     <header className="sticky top-0 z-40 h-16 bg-white border-b border-slate-200 flex items-center justify-between px-6">
//       {/* Left */}
//       <div className="flex items-center gap-6">
//         {/* Search */}
//         <div className="hidden md:flex relative w-72">
//           <Search
//             size={18}
//             className="absolute left-4 top-1/2 -translate-y-1/2 text-slate-400"
//           />

//           <input
//             id="globalSearch"
//             type="text"
//             placeholder="Search..."
//             className="w-full rounded-full bg-slate-100 border border-transparent pl-11 pr-4 py-2.5 text-sm outline-none focus:border-blue-500 focus:bg-white transition"
//           />
//         </div>
//       </div>

//       {/* Right */}
//       <div className="flex items-center gap-5">
//         {/* Current Date */}
//         <div className="hidden lg:flex items-center gap-2 text-sm text-slate-600">
//           <CalendarDays size={18} />

//           <span>{currentDate}</span>
//         </div>

//         {/* Dark Mode */}
//         <button
//           title="Toggle dark mode"
//           className="h-10 w-10 rounded-full hover:bg-slate-100 flex items-center justify-center transition"
//         >
//           <MoonStar size={20} />
//         </button>

//         {/* Notifications */}
//         <div className="relative">
//           <button
//             onClick={() => {
//               setShowNotifications(!showNotifications);
//               setShowProfile(false);
//             }}
//             className="relative h-10 w-10 rounded-full hover:bg-slate-100 flex items-center justify-center transition"
//           >
//             <Bell size={20} />

//             {user.unreadNotifications > 0 && (
//               <span className="absolute top-1 right-1 min-w-[18px] h-[18px] rounded-full bg-red-500 text-white text-[11px] flex items-center justify-center font-semibold">
//                 {user.unreadNotifications}
//               </span>
//             )}
//           </button>

//           {showNotifications && (
//             <div className="absolute right-0 mt-3 w-80 rounded-xl border border-slate-200 bg-white shadow-xl overflow-hidden">
//               <div className="flex items-center justify-between px-4 py-3 border-b">
//                 <h6 className="font-semibold">Notifications</h6>

//                 <Link
//                   href="/notifications"
//                   className="text-sm text-blue-600 hover:underline"
//                 >
//                   View All
//                 </Link>
//               </div>

//               <div className="py-8 text-center text-sm text-slate-500">
//                 No new notifications
//               </div>
//             </div>
//           )}
//         </div>

//         {/* User Dropdown */}
//         <div className="relative">
//           <button
//             onClick={() => {
//               setShowProfile(!showProfile);
//               setShowNotifications(false);
//             }}
//             className="flex items-center gap-3 rounded-lg px-2 py-1 hover:bg-slate-100 transition"
//           >
//             <div className="w-10 h-10 rounded-full bg-blue-600 text-white flex items-center justify-center font-semibold overflow-hidden">
//               {user.avatarUrl ? (
//                 <img
//                   src={user.avatarUrl}
//                   alt="Avatar"
//                   className="w-full h-full object-cover"
//                 />
//               ) : (
//                 user.fullName.charAt(0)
//               )}
//             </div>

//             <div className="hidden md:block text-left">
//               <span className="block text-sm font-semibold text-slate-800">
//                 {user.fullName}
//               </span>

//               <span className="block text-xs text-slate-500">
//                 {user.role}
//               </span>
//             </div>

//             <ChevronDown
//               size={18}
//               className="hidden md:block text-slate-500"
//             />
//           </button>

//           {showProfile && (
//             <div className="absolute right-0 mt-3 w-64 rounded-xl border border-slate-200 bg-white shadow-xl overflow-hidden">
//               <div className="px-4 py-3 border-b">
//                 <h6 className="font-semibold">{user.fullName}</h6>

//                 <p className="text-sm text-slate-500">
//                   {user.email}
//                 </p>
//               </div>

//               <div className="py-2">
//                 <Link
//                   href="/profile"
//                   className="flex items-center gap-3 px-4 py-2 hover:bg-slate-100"
//                 >
//                   <User size={18} />
//                   Profile
//                 </Link>

//                 <Link
//                   href="/change-password"
//                   className="flex items-center gap-3 px-4 py-2 hover:bg-slate-100"
//                 >
//                   <KeyRound size={18} />
//                   Change Password
//                 </Link>

//                 <Link
//                   href="/settings"
//                   className="flex items-center gap-3 px-4 py-2 hover:bg-slate-100"
//                 >
//                   <Settings size={18} />
//                   Settings
//                 </Link>

//                 <hr className="my-2" />

//                 <button className="w-full flex items-center gap-3 px-4 py-2 text-red-600 hover:bg-red-50">
//                   <LogOut size={18} />
//                   Logout
//                 </button>
//               </div>
//             </div>
//           )}
//         </div>
//       </div>
//     </header>
//   );
// }

"use client";

import { Bell, User } from "lucide-react";

export default function AdminNavbar() {
  return (
    <header className="sticky top-0 z-40 flex h-16 items-center justify-between border-b bg-white px-6">
      <div>
        <h2 className="text-lg font-semibold text-gray-800">
          Admin Panel
        </h2>
      </div>

      <div className="flex items-center gap-4">
        <button className="rounded-full p-2 hover:bg-gray-100">
          <Bell size={20} />
        </button>

        <button className="flex items-center gap-2">
          <div className="flex h-9 w-9 items-center justify-center rounded-full bg-gray-200">
            <User size={18} />
          </div>

          <span className="text-sm font-medium">
            Admin
          </span>
        </button>
      </div>
    </header>
  );
}