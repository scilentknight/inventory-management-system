import { CheckCircle2, Zap, ShieldCheck, Expand } from "lucide-react";

const checklist = [
  "Multi-role access control",
  "Real-time stock tracking",
  "Advanced reporting & analytics",
  "Inventory & supplier management",
  "Responsive design for any device",
  "Barcode and product management",
];

const metrics = [
  // {
  //   icon: Zap,
  //   cls: "mc-1",
  //   title: "Lightning Fast",
  //   text: "Optimized for speed with sub-second response times",
  // },
  {
    icon: Zap,
    cls: "mc-1",
    title: "Real-Time Tracking",
    text: "Monitor stock levels and inventory movements instantly",
  },
  // {
  //   icon: ShieldCheck,
  //   cls: "mc-2",
  //   title: "Enterprise Security",
  //   text: "Role-based authorization with full audit trails",
  // },
  {
    icon: ShieldCheck,
    cls: "mc-2",
    title: "Secure Management",
    text: "Role-based access control with complete activity tracking",
  },
  // {
  //   icon: Expand,
  //   cls: "mc-3",
  //   title: "Fully Scalable",
  //   text: "Grows with your business from 1 to 100+ locations",
  // },
  {
    icon: Expand,
    cls: "mc-3",
    title: "Business Ready",
    text: "Scales from small stores to large inventory operations",
  },
];

export default function About() {
  return (
    <section id="about" className="about-section">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="grid lg:grid-cols-2 gap-16 items-center">
          {/* Left Content */}
          <div>
            <span className="section-badge">About</span>

            <h2
              className="text-3xl sm:text-4xl font-bold mt-4 mb-5"
              style={{ color: "var(--text-main)" }}
            >
              Built for Businesses That Manage Inventory
            </h2>

            <p className="mb-6" style={{ color: "var(--text-muted)" }}>
              InventoryManager is designed for businesses that need efficient
              stock control and professional inventory tools. From small shops
              to large enterprises, our platform helps you manage products,
              suppliers, and operations with ease.
            </p>

            <div className="about-checklist">
              {checklist.map((item) => (
                <div key={item} className="check-item">
                  <CheckCircle2 className="w-5 h-5" />
                  <span>{item}</span>
                </div>
              ))}
            </div>
          </div>

          {/* Right Cards */}
          <div className="about-metrics">
            {metrics.map(({ icon: Icon, cls, title, text }) => (
              <div key={title} className={`metric-card ${cls}`}>
                <Icon className="w-8 h-8 mb-4" />

                <h3>{title}</h3>

                <p>{text}</p>
              </div>
            ))}
          </div>
        </div>
      </div>
    </section>
  );
}
