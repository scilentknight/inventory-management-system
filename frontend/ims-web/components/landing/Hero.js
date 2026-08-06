// components/landing/Hero.js

import Link from "next/link";
import {
  Sparkles,
  PackageCheck,
  PlayCircle,
  Boxes,
  Package,
  Warehouse,
} from "lucide-react";

const chartHeights = [40, 65, 50, 80, 70, 90, 75];

export default function Hero() {
  return (
    <section className="hero-section">
      <div className="hero-bg-effects">
        <div className="hero-orb hero-orb-1" />
        <div className="hero-orb hero-orb-2" />
        <div className="hero-orb hero-orb-3" />
      </div>

      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="grid lg:grid-cols-2 gap-12 items-center min-h-[calc(100vh-100px)]">
          {/* Left Content */}
          <div className="hero-content">
            <span className="hero-badge">
              <Sparkles className="w-3.5 h-3.5 inline-block mr-1.5 -mt-0.5" />
              Modern Inventory Management
            </span>

            <h1 className="hero-title">
              Manage Your Inventory{" "}
              <span className="gradient-text">Like a Pro</span>
            </h1>

            <p className="hero-subtitle">
              Track inventory in real time, manage products, monitor stock
              levels, and optimize warehouse operations with our powerful
              all-in-one inventory management platform.
            </p>

            <div className="hero-actions">
              <Link href="/register" className="btn-hero-primary">
                <PackageCheck className="w-5 h-5 mr-2" />
                Get Started Free
              </Link>

              <a href="#features" className="btn-hero-outline">
                <PlayCircle className="w-5 h-5 mr-2" />
                Learn More
              </a>
            </div>

            <div className="hero-stats">
              <div>
                <span className="stat-number">10K+</span>
                <span className="stat-label">Products</span>
              </div>

              <div>
                <span className="stat-number">500K+</span>
                <span className="stat-label">Stock Movements</span>
              </div>

              <div>
                <span className="stat-number">99.9%</span>
                <span className="stat-label">System Uptime</span>
              </div>
            </div>
          </div>

          {/* Right Dashboard Preview */}
          <div className="hidden lg:block hero-visual">
            <div className="dashboard-preview">
              <div className="preview-header">
                <div className="preview-dots">
                  <span></span>
                  <span></span>
                  <span></span>
                </div>

                <span className="preview-title">Dashboard</span>
              </div>

              <div className="preview-body">
                <div className="preview-cards">
                  <div className="preview-card pc-blue">
                    <Boxes />
                    <div>
                      <small>Products</small>
                      <strong>12,450</strong>
                    </div>
                  </div>

                  <div className="preview-card pc-green">
                    <Package />
                    <div>
                      <small>Low Stock</small>
                      <strong>42</strong>
                    </div>
                  </div>

                  <div className="preview-card pc-purple">
                    <Warehouse />
                    <div>
                      <small>Warehouses</small>
                      <strong>12</strong>
                    </div>
                  </div>
                </div>

                <div className="chart-bars">
                  {chartHeights.map((height, index) => (
                    <div
                      key={index}
                      className="chart-bar"
                      style={{ height: `${height}%` }}
                    />
                  ))}
                </div>
              </div>
            </div>
          </div>
          {/* End Dashboard */}
        </div>
      </div>
    </section>
  );
}
