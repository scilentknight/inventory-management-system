import {
  Gauge,
  Banknote,
  Flame,
  Package,
  Users,
  TrendingUp,
} from "lucide-react";

const features = [
  {
    icon: Gauge,
    cls: "fi-blue",
    title: "Real-Time Dashboard",
    text: "Monitor inventory levels, stock movements, and business performance with live updates.",
  },
  {
    icon: Banknote,
    cls: "fi-green",
    title: "Product Management",
    text: "Manage products, categories, pricing, and details efficiently from one place.",
  },
  {
    icon: Flame,
    cls: "fi-purple",
    title: "Stock Tracking",
    text: "Track stock availability, incoming products, and inventory movements in real time.",
  },
  {
    icon: Package,
    cls: "fi-orange",
    title: "Inventory Control",
    text: "Manage stock levels, reorder alerts, warehouses, and suppliers from one platform.",
  },
  {
    icon: Users,
    cls: "fi-pink",
    title: "User & Role Management",
    text: "Manage employees, permissions, and role-based access for secure operations.",
  },
  {
    icon: TrendingUp,
    cls: "fi-teal",
    title: "Reports & Analytics",
    text: "Generate detailed inventory reports, product insights, and business analytics.",
  },
];

export default function Features() {
  return (
    <section id="features" className="features-section">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        {/* Section Header */}
        <div className="text-center max-w-2xl mx-auto mb-16">
          <span className="section-badge">Features</span>

          <h2
            className="text-3xl sm:text-4xl font-bold"
            style={{ color: "var(--text-main)" }}
          >
            Everything You Need to Manage Your Inventory
          </h2>

          <p className="mt-3" style={{ color: "var(--text-muted)" }}>
            Powerful tools designed for modern inventory and stock management
          </p>
        </div>

        {/* Feature Cards */}
        <div className="grid gap-6 sm:grid-cols-2 lg:grid-cols-3">
          {features.map(({ icon: Icon, cls, title, text }) => (
            <div key={title} className="feature-card">
              <div className={`feature-icon ${cls}`}>
                <Icon className="w-7 h-7" />
              </div>

              <h4
                className="text-lg font-semibold mb-2"
                style={{ color: "var(--text-main)" }}
              >
                {title}
              </h4>

              <p
                className="text-sm leading-6"
                style={{ color: "var(--text-muted)" }}
              >
                {text}
              </p>
            </div>
          ))}
        </div>
      </div>
    </section>
  );
}
