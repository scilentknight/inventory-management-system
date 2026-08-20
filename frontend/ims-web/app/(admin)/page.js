// import { redirect } from "next/navigation";

// export default function Page() {
//
//  redirect("/admin/dashboard");
// }

import DashboardPage from "./dashboard/page";

export default function AdminPage() {
  return <DashboardPage />;
}
