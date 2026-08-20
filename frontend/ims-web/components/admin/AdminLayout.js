// "use client";

// import { useState } from "react";

// import Sidebar from "./Sidebar";
// import Header from "./Header";
// import Footer from "./Footer";
// import SidebarOverlay from "@/components/admin/SidebarOverlay";
// import Notification from "@/components/admin/Notification";

// export default function AdminLayout({ children }) {
//   const [sidebarCollapsed, setSidebarCollapsed] = useState(false);
//   const [mobileSidebarOpen, setMobileSidebarOpen] = useState(false);

//   const toggleSidebar = () => {
//     if (window.innerWidth < 1024) {
//       setMobileSidebarOpen((prev) => !prev);
//     } else {
//       setSidebarCollapsed((prev) => !prev);
//     }
//   };

//   return (
//     <div className="min-h-screen bg-slate-100">
//       {/* Sidebar */}
//       <Sidebar
//         collapsed={sidebarCollapsed}
//         mobileOpen={mobileSidebarOpen}
//         onClose={() => setMobileSidebarOpen(false)}
//       />

//       {/* Right Side */}
//       <div
//         className={`
//           flex
//           flex-col
//           min-h-screen
//           transition-all
//           duration-300
//           ${sidebarCollapsed ? "lg:ml-20" : "lg:ml-72"}
//         `}
//       >
//         {/* Header */}
//         <Header onToggleSidebar={toggleSidebar} collapsed={sidebarCollapsed} />

//         {/* Page Content */}
//         <main
//           className="
//             flex-1
//             mt-16
//             bg-slate-100
//             p-6
//             overflow-y-auto
//           "
//         >
//           <Notification />
//           {children}
//         </main>

//         <Footer />
//       </div>
//       <SidebarOverlay />

//       {/* Mobile Overlay */}
//       {mobileSidebarOpen && (
//         <div
//           className="
//             fixed
//             inset-0
//             bg-black/40
//             z-40
//             lg:hidden
//           "
//           onClick={() => setMobileSidebarOpen(false)}
//         />
//       )}
//     </div>
//   );
// }

// "use client";

// import { useState } from "react";

// import Sidebar from "./Sidebar";
// import Header from "./Header";
// import Footer from "./Footer";
// import Notification from "./Notification";

// export default function AdminLayout({ children }) {
//   const [sidebarCollapsed, setSidebarCollapsed] = useState(false);
//   const [mobileSidebarOpen, setMobileSidebarOpen] = useState(false);

//   const toggleSidebar = () => {
//     if (window.innerWidth < 1024) {
//       setMobileSidebarOpen((prev) => !prev);
//     } else {
//       setSidebarCollapsed((prev) => !prev);
//     }
//   };

//   return (
//     <div className="bg-slate-100">
//       {/* Sidebar */}
//       <Sidebar
//         collapsed={sidebarCollapsed}
//         mobileOpen={mobileSidebarOpen}
//         onClose={() => setMobileSidebarOpen(false)}
//       />

//       {/* Right Side */}
//       <div
//         className={`
//           transition-all
//           duration-300
//           ${sidebarCollapsed ? "lg:ml-20" : "lg:ml-72"}
//         `}
//       >
//         {/* Fixed Header */}
//         <Header
//           onToggleSidebar={toggleSidebar}
//           collapsed={sidebarCollapsed}
//         />

//         {/* Content + Footer */}
//         <div className="pt-16 h-screen flex flex-col">
//           {/* Scrollable Content */}
//           <main className="flex-1 overflow-y-auto p-6">
//             <Notification />

//             {children}
//           </main>

//           {/* Fixed Bottom Footer */}
//           <Footer />
//         </div>
//       </div>

//       {/* Mobile Overlay */}
//       {mobileSidebarOpen && (
//         <div
//           onClick={() => setMobileSidebarOpen(false)}
//           className="fixed inset-0 bg-black/40 z-40 lg:hidden"
//         />
//       )}
//     </div>
//   );
// }

import AdminSidebar from "./AdminSidebar";
import AdminNavbar from "./AdminNavbar";
import Footer from "./Footer";

export default function AdminLayout({ children }) {
  return (
    <div className="min-h-screen bg-gray-100">
      <AdminSidebar />

      <div className="ml-64">
        <AdminNavbar />

        <main className="p-6">{children}</main>
        <Footer />
      </div>
    </div>
  );
}
