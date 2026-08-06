// export default function Footer() {
//   return (
//     <div className="w-full h-full flex items-center justify-center text-sm text-slate-500">
//       © {new Date().getFullYear()} InventoryPro. All rights reserved. v1.0.0
//     </div>
//   );
// }
export default function Footer() {
  const year = new Date().getFullYear();

  return (
    <footer
      className="admin-footer bg-white border-top d-flex align-items-center justify-content-between px-4"
      style={{
        height: "60px",
        flexShrink: 0,
      }}
    >
      <span className="text-muted">
        © {year} CaféManager. All rights reserved.
      </span>

      <span className="text-muted">v1.0.0</span>
    </footer>
  );
}
