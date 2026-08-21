export default function AdminFooter() {
  const year = new Date().getFullYear();
  return (
    <footer className="h-footer flex items-center justify-center border-t border-border-theme bg-bg-card text-sm text-text-muted">
      <div className="flex w-full items-center justify-between px-6">
        <span>&copy; {year} InventoryPro. All rights reserved.</span>
        <span className="text-text-muted">v1.0.0</span>
      </div>
    </footer>
  );
}
